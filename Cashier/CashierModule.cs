using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tulpep.NotificationWindow;
using ZXing;

namespace PIERANGELO
{
    public partial class CashierModule : Form
    {
        String id;
        String price;
        int qty;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();     
   
        public CashierModule()
        {

            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            lbldate.Text = DateTime.Now.ToLongDateString();
            ShowProduct();
            Category();
            this.KeyPreview = true;
           
        }

        public void GetTransaction()
        {
            try
            {
                string date = DateTime.Now.ToString("yyyyMMdd");
                string transactionNo;
                int count;
                con.Open();
                com = new SqlCommand("SELECT TOP 1 transactionNo from tableSales WHERE transactionNo LIKE '" + date + "%' order by id desc", con);
                dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transactionNo = dr[0].ToString();
                    count = int.Parse(transactionNo.Substring(8, 4));
                    lbltransaction.Text = date + (count + 1);
                }
                else
                {
                    transactionNo = date + "1001";
                    lbltransaction.Text = transactionNo;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //For time and Date
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDateee.Text = DateTime.Now.ToLongDateString();
        }

        //To Show the Sales 
        public void ShowSales()
        {
            try
            {
                Boolean hasRecord = false;
                dgSales.Rows.Clear();
                int i = 0;
                double total = 0;
                double disc = 0;
                var addedProducts = new Dictionary<string, int>(); // Dictionary to track added products

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    string query = @"
           SELECT s.id, r.ID as rID, s.pcode, p.productname, s.price, s.discount, s.qty, s.total
            FROM tableSales AS s
            INNER JOIN tableProductList AS p ON s.pcode = p.pcode
            LEFT JOIN tablePODetails AS r ON s.pcode = r.pcode
           WHERE s.transactionNo LIKE @transactionNo AND s.status LIKE 'PENDING'";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@transactionNo", lbltransaction.Text);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string pcode = dr["pcode"].ToString();

                                if (!addedProducts.ContainsKey(pcode))
                                {
                                    i++;
                                    total += Convert.ToDouble(dr["total"]);
                                    disc += Convert.ToDouble(dr["discount"]);
                                    dgSales.Rows.Add(
                                        i,
                                        dr["rID"].ToString(),
                                        dr["id"].ToString(),
                                        dr["pcode"].ToString(),
                                        dr["productname"].ToString(),
                                        dr["price"].ToString(),
                                        dr["discount"].ToString(),
                                        "+",
                                        dr["qty"].ToString(),
                                        "-",
                                        Convert.ToDouble(dr["total"]).ToString("₱#,##0.00")
                                    );
                                    addedProducts[pcode] = 1; // Add to the dictionary
                                    hasRecord = true;
                                }
                            }
                        }
                    }
                }

                lblsubtotal.Text = total.ToString("#,##0.00");
                lbldiscount.Text = disc.ToString("#,##0.00");
                TotalSales();

                btnPayment.Enabled = hasRecord;
                btnDiscount.Enabled = hasRecord;
                btnCancelSales.Enabled = hasRecord;
                button2.Enabled = hasRecord;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private void dgSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string cname = dgSales.Columns[e.ColumnIndex].Name;

            if (cname == "Add")
            {
                try
                {
                    int availableStock = 0;

                    // Get the product code from dgSales
                    string pcode = dgSales.Rows[e.RowIndex].Cells["pcode"].Value.ToString();

                    // Check available stock in dgvProductList
                    foreach (DataGridViewRow row in dgvproductlist.Rows)
                    {
                        if (row.Cells["productcode"].Value.ToString() == pcode)
                        {
                            availableStock = Convert.ToInt32(row.Cells["totalqty"].Value);
                            break;
                        }
                    }

                    int currentQty = Convert.ToInt32(dgSales.Rows[e.RowIndex].Cells["orderqty"].Value);

                    if (currentQty + 1 <= availableStock) // Validate stock availability
                    {
                        // Update the quantity in the database
                        con.Open();
                        com = new SqlCommand("UPDATE tableSales SET qty = qty + 1 WHERE transactionNo = @transactionNo AND pcode = @pcode", con);
                        com.Parameters.AddWithValue("@transactionNo", lbltransaction.Text);
                        com.Parameters.AddWithValue("@pcode", pcode);
                        int rowsAffected = com.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            ShowSales();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot add more items. Only " + availableStock + " items are in stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (cname == "Minus")
            {
                try
                {
                    int currentQty = Convert.ToInt32(dgSales.Rows[e.RowIndex].Cells["orderqty"].Value);

                    if (currentQty - 1 >= 0) // Validate cart quantity
                    {
                        string pcode = dgSales.Rows[e.RowIndex].Cells["pcode"].Value.ToString();
                        int productListQty = 0;

                        // Check the current quantity in dgvProductList
                        foreach (DataGridViewRow row in dgvproductlist.Rows)
                        {
                            if (row.Cells["productcode"].Value.ToString() == pcode)
                            {
                                productListQty = Convert.ToInt32(row.Cells["totalqty"].Value);
                                break;
                            }
                        }

                        if (currentQty - 1 > productListQty) // Slow validation
                        {
                            MessageBox.Show("You cannot reduce more than the available quantity in stock.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Update the quantity in the database
                        con.Open();
                        com = new SqlCommand("UPDATE tableSales SET qty = qty - 1 WHERE transactionNo = @transactionNo AND pcode = @pcode", con);
                        com.Parameters.AddWithValue("@transactionNo", lbltransaction.Text);
                        com.Parameters.AddWithValue("@pcode", pcode);
                        int rowsAffected = com.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            ShowSales();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Remaining quantity in the cart is only " + currentQty + ". You cannot remove more than that.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (cname == "Delete")
            {
                if (MessageBox.Show("Void the Product?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    VoidV voidForm = new VoidV(); // Renamed for clarity
                    voidForm.Show(); // Use PascalCase for method names
                }
            }
        }
       
        private void BtnDiscount_Click(object sender, EventArgs e)
        {
            Discount discount = new Discount(this);
            discount.lblID.Text = id;               
            discount.txtPrice.Text = price;      
            discount.ShowDialog();

        }
        private void dgSales_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
               
                if (dgSales.CurrentRow != null)
                {
                    int i = dgSales.CurrentRow.Index;
                    id = dgSales[2, i].Value.ToString();  
                    price = dgSales[10, i].Value.ToString();  
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void TotalSales()
        {
            double discount = Double.Parse(lbldiscount.Text);
            double sale = Double.Parse(lblsubtotal.Text);
            double VAT = sale * dbcon.Vat();
            double Vatable = sale - VAT;
            lblVAT.Text = VAT.ToString("₱#,##0.00");
            lblVatable.Text = Vatable.ToString("₱#,##0.00");
            lblAmount.Text = sale.ToString("₱#,##0.00");


        }
        private void btnPayment_Click(object sender, EventArgs e)
        {
            Payment payment = new Payment(this);
            payment.txtSales.Text = lblAmount.Text;
            payment.ShowDialog();

        }
        private void CashierModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnnewtrans_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                BtnDiscount_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnPayment_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnCancelSales_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                txtBarcode.SelectionStart = 0;
                txtBarcode.SelectionLength = txtBarcode.Text.Length;
            }
        }

        private void btnnewtrans_Click(object sender, EventArgs e)
        {
            if (dgSales.Rows.Count > 0)
            {
                return;
            }
            GetTransaction();

        }

        private void btnCancelSales_Click(object sender, EventArgs e)
        {
            VoidVerification vv = new VoidVerification();

          
            if (vv.ShowDialog() == DialogResult.OK)
            {
              
                string user = vv.VerifiedUser; 
                vv.VoidAllRows(user); 
            }
        }
        public void Category()
        {
            cbCategory.Items.Clear();
            con.Open();
            com = new SqlCommand("SELECT category FROM tableCategory", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                cbCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
        public void ShowProduct()
        {
            try
            {
                dgvproductlist.Rows.Clear();
                int i = 0;

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    string query = @"
                SELECT 
                    p.pcode, 
                    p.barcode, 
                    p.productname, 
                    c.category, 
                    COALESCE(p.price, 0) AS PRICE, 
                    COALESCE(SUM(r.qtyRO), 0) AS TotalQty, 
                    p.criticallevel, 
                    p.reorder, 
                    p.safetylevel 
                FROM 
                    tableProductList p
                LEFT JOIN 
                    tablePODetails r ON p.pcode = r.pcode AND r.status IN ('Fulfilled', 'Partial') -- Fix JOIN condition
                LEFT JOIN 
                    tableCategory c ON c.id = p.cid 
                WHERE 
                    (r.expirydate > CAST(GETDATE() AS DATE) OR r.expirydate IS NULL)
                    AND (@Category IS NULL OR c.category = @Category)
                    AND (p.pcode LIKE @searchText 
                         OR p.productname LIKE @searchText 
                         OR p.barcode LIKE @searchText)
                GROUP BY 
                    p.pcode, p.barcode, p.productname, c.category, p.price, p.criticallevel, p.reorder, p.safetylevel 
                HAVING 
                    COALESCE(SUM(r.qtyRO), 0) > 0 
                ORDER BY 
                    CASE
                        WHEN COALESCE(SUM(r.qtyRO), 0) = 0 THEN 1
                        WHEN COALESCE(SUM(r.qtyRO), 0) <= p.criticallevel THEN 2
                        WHEN COALESCE(SUM(r.qtyRO), 0) <= p.reorder THEN 3
                        WHEN COALESCE(SUM(r.qtyRO), 0) < p.safetylevel THEN 4
                        ELSE 5
                    END, 
                    COALESCE(SUM(r.qtyRO), 0) ASC;
            ";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@searchText", "%" + txtsearchitem.Text + "%");
                        if (!string.IsNullOrEmpty(cbCategory.Text))
                        {
                            com.Parameters.AddWithValue("@Category", cbCategory.Text);
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@Category", DBNull.Value);
                        }

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int totalqty = Convert.ToInt32(dr["TotalQty"]);
                                double price = Convert.ToDouble(dr["PRICE"]);
                                i += 1;
                                dgvproductlist.Rows.Add(i, dr["pcode"].ToString(), dr["barcode"].ToString(), dr["productname"].ToString(),
                                    dr["category"].ToString(), price.ToString("₱0.00"), totalqty);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           string cname = dgvproductlist.Columns[e.ColumnIndex].Name;
            if (cname == "Select")
            {
                try
                {
                    Quantity Qf = new Quantity(this);
                    Qf.lblproductname.Text = dgvproductlist[3, e.RowIndex].Value.ToString();

                    string productCode = dgvproductlist.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string priceString = dgvproductlist.Rows[e.RowIndex].Cells[5].Value.ToString().Replace("₱", "").Trim();
                    string quantityString = dgvproductlist.Rows[e.RowIndex].Cells[6].Value.ToString();

                    double price = double.Parse(priceString);
                    int quantity = int.Parse(quantityString);

                    Qf.ProductInfo(productCode, price, lbltransaction.Text, quantity);
                    Qf.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid data format: " + ex.Message);
                }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (dgSales.Rows.Count > 0)
            {
                MessageBox.Show("Unable to logout. Please cancel the transaction.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult message = MessageBox.Show("Do you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (message == DialogResult.Yes)
            {
                string name = lblaccoutname.Text;

                // Log the logout action in tableLogHistory
                Log(name, "The Cashier has successfully logged out.", "Log Out");
                this.Hide();
                Login form = new Login();
                form.ShowDialog();
            }
        }

        private void Log(string user, string activity, string action)
        {
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO tableLogHistory (username, Date, activity ,action) VALUES (@username, @Date, @activity, @action)", con);
            command.Parameters.AddWithValue("@username", user);
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.Parameters.AddWithValue("@activity", activity);
            command.Parameters.AddWithValue("@action", action);
            command.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CashierSetting cs = new CashierSetting();
            cs.lblUser.Text = lblaccoutname.Text;
            cs.lblusername.Text = lblUser.Text;
            cs.ShowDialog();
        }
        private void txtsearchitem_TextChanged(object sender, EventArgs e)
        {
            ShowProduct();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateDiscount dc = new CreateDiscount();
            dc.btnsave.Hide();
            dc.dgvDiscount.Columns["Edit"].Visible = false;
            dc.dgvDiscount.Columns["Delete"].Visible = false;
            dc.ShowDialog();

        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowProduct();
        }

        private void cbCategory_TextChanged(object sender, EventArgs e)
        {
            ShowProduct();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dgSales.CurrentRow != null)
            {
                int i = dgSales.CurrentRow.Index;

                // Trim peso sign and validate lblAmount.Text
                string amountWithoutPesoSign = lblAmount.Text.Replace("₱", "").Trim();
                if (!decimal.TryParse(amountWithoutPesoSign, out decimal salesAmount))
                {
                    MessageBox.Show("Invalid amount format.");
                    return;
                }

                // Retrieve and validate data from the selected row
                string id = dgSales.Rows[i].Cells[1]?.Value?.ToString() ?? string.Empty;
                string salesId = dgSales.Rows[i].Cells[2]?.Value?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(salesId))
                {
                    MessageBox.Show("Invalid data in the selected row.");
                    return;
                }

                if (!int.TryParse(dgSales.Rows[i].Cells[8]?.Value?.ToString(), out int qty))
                {
                    MessageBox.Show("Invalid quantity format in the selected row.");
                    return;
                }

                // Initialize and show the GcashPayment form
                GcashPayment gcash = new GcashPayment(this)
                {
                    txtSales = { Text = salesAmount.ToString() },
                    SalesId = salesId,
                    Quantity = qty,
                    ProductId = id
                };

                gcash.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row in the sales data.");
            }
        }

        public void AddSales(String apcode, double aprice, int aqty)
        {
            String id = "";
            Boolean found = false;
            int salesqty = 0;

            // Open connection
            con.Open();

            // Check if the product has already been added to the current transaction
            com = new SqlCommand("SELECT * FROM tableSales WHERE transactionNo = @transactionNo AND pcode = @pcode", con);
            com.Parameters.AddWithValue("@transactionNo", lbltransaction.Text);
            com.Parameters.AddWithValue("@pcode", apcode);
            dr = com.ExecuteReader();

            if (dr.Read()) // Read once to check if there are rows
            {
                found = true;
                id = dr["id"].ToString();
                salesqty = int.Parse(dr["qty"].ToString());
            }
            dr.Close();
            con.Close();

            if (found)
            {
                // Update the quantity if the product already exists in the sales list
                if (qty < (aqty + salesqty))
                {
                    MessageBox.Show("Remaining Quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                con.Open();
                com = new SqlCommand("UPDATE tableSales SET qty = qty + @qty WHERE id = @id", con);
                com.Parameters.AddWithValue("@qty", aqty); // Increment the quantity
                com.Parameters.AddWithValue("@id", id);
                com.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                // Insert a new sales record if the product is being scanned for the first time
                if (qty < aqty)
                {
                    MessageBox.Show("Remaining Quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                con.Open();
                com = new SqlCommand("INSERT INTO tableSales (transactionNo, pcode, price, qty, transdate, cashier) VALUES (@transactionNo, @pcode, @price, @qty, @transdate, @cashier)", con);
                com.Parameters.AddWithValue("@transactionNo", lbltransaction.Text);
                com.Parameters.AddWithValue("@pcode", apcode);
                com.Parameters.AddWithValue("@price", aprice);
                com.Parameters.AddWithValue("@qty", aqty); // Initial quantity
                com.Parameters.AddWithValue("@transdate", DateTime.Now);
                com.Parameters.AddWithValue("@cashier", lblaccoutname.Text);
                com.ExecuteNonQuery();
                con.Close();
            }

            // Refresh the DataGridView to show updated quantities
            ShowSales();
        }
        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text == String.Empty)
                {
                    return;
                }
                else
                {

                    String apcode;
                    double aprice;
                    int aqty;

                    con.Open();
                    com = new SqlCommand("Select * from tableProductList where barcode like '" + txtBarcode.Text + "'", con);
                    dr = com.ExecuteReader();
                    dr.Read();
                  
                    if (dr.HasRows)
                    {
                        qty = int.Parse(dr["qty"].ToString());
                        apcode = dr["pcode"].ToString();
                        aprice = double.Parse(dr["price"].ToString());
                        aqty = int.Parse(txtqty.Text);

                        
                        dr.Close();
                        con.Close();
                        // Instead of calling AddSales here, pass the barcode to ProcessBarcode
                        ProcessBarcode(txtBarcode.Text);  // Process the barcode and add the sales
                    }
                    else
                    {
                        dr.Close();
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProcessBarcode(string barcode)
        {
            try
            {
                // Fetch product details based on the scanned barcode
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    string query = "SELECT * FROM tableProductList WHERE barcode = @barcode";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@barcode", barcode); // Use the barcode parameter, not txtBarcode.Text
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Extract product details
                                string apcode = dr["pcode"].ToString();
                                double aprice = Convert.ToDouble(dr["price"]);
                                int aqty = 1; // Default quantity for a single scan

                                // Call the method to add the sales record
                                AddSales(apcode, aprice, aqty);
                                ShowSales();
                                
                            }
                            else
                            {
                                MessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    txtBarcode.Clear();
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            ReturnExchange ex = new ReturnExchange();
            ex.datefrom.Enabled = false;
            ex.dateto.Enabled = false;
            ex.lblCashiername.Text = lblaccoutname.Text;
            ex.cashname = lblname.Text;
            ex.ShowSalesRecord();
            ex.ShowDialog();
        }

        private void dgvproductlist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the row index is valid (not header or out of bounds)
                if (e.RowIndex >= 0)
                {
                    // Retrieve the product information from the selected row in the DataGridView
                    DataGridViewRow row = dgvproductlist.Rows[e.RowIndex];

                    // Get the barcode value from the selected row (assuming barcode is one of the columns)
                    string barcode = row.Cells["barcode"].Value.ToString(); // Adjust column name if necessary

                    if (string.IsNullOrEmpty(barcode))
                    {
                        return; // Do nothing if no barcode is available
                    }

                    // Store product details
                    string apcode;
                    double aprice;
                    int aqty;

                    // Open the connection and search for the product in the database using the barcode
                    con.Open();
                    com = new SqlCommand("SELECT * FROM tableProductList WHERE barcode = @barcode", con);
                    com.Parameters.AddWithValue("@barcode", barcode);
                    dr = com.ExecuteReader();
                    dr.Read();

                    if (dr.HasRows) // Check if product is found in the database
                    {
                        qty = int.Parse(dr["qty"].ToString());
                        apcode = dr["pcode"].ToString();
                        aprice = double.Parse(dr["price"].ToString());

                        // Get quantity from the text box (txtqty) or assign a default if needed
                        aqty = int.Parse(txtqty.Text);

                        dr.Close();
                        con.Close();

                        // Call method to process the barcode and add to sales
                        ProcessBarcode(barcode);
                    }
                    else
                    {
                        dr.Close();
                        con.Close();
                        MessageBox.Show("Product not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Ensure the connection is closed and handle the exception
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
