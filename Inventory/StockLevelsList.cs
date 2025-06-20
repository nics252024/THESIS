using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PIERANGELO
{
    public partial class StockLevelsList : Form
    {
        
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        String PoNo;
        String suppliername;
        public StockLevelsList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            PopulateSupplier();
            LoadCategories();
            PurchaseOrder();
            PopulateSuppliers();
            ReceiveOrder();  
        }
        private void LoadCategories()
        {
            try
            {
                cbcategories.Items.Clear(); 
                cbcategories.Items.Add("All"); 

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    string query = @"SELECT DISTINCT c.category 
                             FROM tableProductList p
                             INNER JOIN tableCategory c ON p.cid = c.id";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                cbcategories.Items.Add(dr["category"].ToString());
                            }
                        }
                    }
                }
                cbcategories.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public void ShowAdjustmentStock()
        {
            int i = 0;
            dgvStockAdjustment.Rows.Clear();

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                string query = @"
        SELECT r.id, r.PoNo, p.pcode, p.productname, c.category, p.supplier, r.expirydate,  
               ISNULL(SUM(r.qtyRO), 0) AS totalqty 
        FROM tableProductList AS p 
        INNER JOIN tableCategory AS c ON c.id = p.cid 
        LEFT JOIN tablePODetails AS r ON p.pcode = r.pcode 
        WHERE p.productname LIKE @SearchText 
              AND (r.expirydate IS NULL OR r.expirydate > GETDATE())
              AND (@Category = 'All' OR c.category = @Category)
        GROUP BY r.id, r.PoNo, p.pcode, p.productname, c.category, p.supplier, r.expirydate";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    string searchText = txtsearchstockadjustment.Text;
                    if (string.IsNullOrEmpty(searchText))
                    {
                        searchText = "%";
                    }
                    com.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = "%" + searchText + "%";
                    if (cbcategories.SelectedItem != null)
                    {
                        string selectedCategory = cbcategories.SelectedItem.ToString();
                        com.Parameters.Add("@Category", SqlDbType.VarChar).Value = selectedCategory;
                    }
                    else
                    {
                        com.Parameters.Add("@Category", SqlDbType.VarChar).Value = "All";
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;

                            // Check the expiry date value
                            string expiry = "";
                            if (dr["expirydate"] != DBNull.Value)
                            {
                                if (dr["expirydate"].ToString() == "No Expiry")
                                {
                                    expiry = "No Expiry";
                                }
                                else
                                {
                                    expiry = Convert.ToDateTime(dr["expirydate"]).ToString("yyyy-MM-dd");
                                }
                            }

                            dgvStockAdjustment.Rows.Add(i,
                                dr["id"].ToString(),
                                dr["PoNo"].ToString(),
                                dr["pcode"].ToString(),
                                dr["productname"].ToString(),
                                dr["category"].ToString(),
                                expiry,
                                dr["supplier"].ToString(),
                                dr["totalqty"].ToString());
                        }
                    }
                }
                con.Close();
            }
        }
        private void txtsearchstockadjustment_TextChanged(object sender, EventArgs e)
        {
            ShowAdjustmentStock();
        }

        private void dgvStockAdjustment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string cName = dgvStockAdjustment.Columns[e.ColumnIndex].Name;
            if (cName == "Select")
            {
                AdjustmentStockDetails asd = new AdjustmentStockDetails(this);
                asd.lblID.Text = dgvStockAdjustment[1, e.RowIndex].Value.ToString();
                asd.lblPoNO.Text = dgvStockAdjustment[2, e.RowIndex].Value.ToString();
                asd.lblproductcode.Text = dgvStockAdjustment[3, e.RowIndex].Value.ToString();   
                asd.lblproductname.Text = dgvStockAdjustment[4, e.RowIndex].Value.ToString();
                asd.lblExpirydate.Text = dgvStockAdjustment[6, e.RowIndex].Value.ToString();
                asd.lblSupplier.Text = dgvStockAdjustment[7, e.RowIndex].Value.ToString();
                asd.lblcategory.Text = dgvStockAdjustment[5, e.RowIndex].Value.ToString();
                asd.Show();
            }
        }

        public void PurchaseOrder()
        {
            try
            {
                int i = 0;
                dgvPO.Rows.Clear();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // SQL query with ORDER BY to sort by transdate in descending order
                string query = "SELECT PoNo, transdate, supname, numItems, totalamountPo, delivereddate, totalamountRo, status " +
                               "FROM tablePurchaseOrders WHERE status = 'Delivered'";

                if (!string.IsNullOrEmpty(cbSuppliers.Text))
                {
                    query += " AND supname LIKE @supname";
                }

                query += " ORDER BY transdate DESC";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(cbSuppliers.Text))
                    {
                        com.Parameters.AddWithValue("@supname", "%" + cbSuppliers.Text + "%");
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            decimal totalAmount = 0;
                            decimal totalAmountPO = 0;
                            decimal.TryParse(dr["totalamountPO"].ToString(), out totalAmount);
                            decimal.TryParse(dr["totalamountRo"].ToString(), out totalAmountPO);
                            DateTime transactionDate;
                            string formattedDate = dr["transdate"] != DBNull.Value && DateTime.TryParse(dr["transdate"].ToString(), out transactionDate)
                                ? transactionDate.ToString("yyyy-MM-dd")
                                : string.Empty;

                            string deliveredDate = dr["delivereddate"] != DBNull.Value && DateTime.TryParse(dr["delivereddate"].ToString(), out transactionDate)
                                ? transactionDate.ToString("yyyy-MM-dd")
                                : string.Empty;

                            dgvPO.Rows.Add(
                                i,
                                dr["PoNo"].ToString(),
                                formattedDate,
                                dr["supname"].ToString(),
                                dr["numItems"].ToString(),
                                "₱" + totalAmount.ToString("N2"),
                                deliveredDate,
                                "₱" + totalAmountPO.ToString("N2"),
                                dr["status"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string cname = dgvPO.Columns[e.ColumnIndex].Name;

                if (cname == "POnum")
                {
                    string PO = dgvPO.Rows[e.RowIndex].Cells["POnum"].Value.ToString();
                    string supplier = dgvPO.Rows[e.RowIndex].Cells["supplier"].Value.ToString();
                    string stats = dgvPO.Rows[e.RowIndex].Cells["status"].Value.ToString();
                    ShowRODetails(PO, supplier,  stats);
                }
            }
        }
        private void ShowRODetails(string PO, string supplier, string stats)
        {
            RODetails ro = new RODetails();
            ro.lblPoNo.Text = PO;
            ro.lblsupplier.Text = supplier;

            if (stats == "Delivered")
            {
                ro.dgvRO.ReadOnly = true;
                ro.btnROreversal.Visible = false;
                ro.btnSaveRODetails.Visible = false;
                ro.dptrans.Visible = false;
                ro.label3.Visible = false;
                ro.dgvRO.Columns["partialselect"].Visible = false;
                ro.dgvRO.Columns["allselect"].Visible = false;
            }
            ro.ReceiveOrder();
            ro.ShowDialog();
        }

      
        private void cbcategories_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowAdjustmentStock();
        }

        private void cbcategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAdjustmentStock();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ReceiveOrder()
        {
            try
            {
                int i = 0; // Row counter
                ROdgv.Rows.Clear(); // Clear existing rows

                if (con.State == ConnectionState.Closed)
                {
                    con.Open(); // Open the database connection
                }

                // Base query
                string query = @"
            SELECT PoNo, transdate, supname, numItems, totalamountPo, delivereddate, totalamountRo, status
            FROM tablePurchaseOrders
            WHERE status = 'In Progress'";

                // Check if a supplier filter is applied
                if (!string.IsNullOrEmpty(cbfiltersupplier.Text))
                {
                    query += " AND supname LIKE @supname"; // Add supplier filter
                }

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    // Add parameter for supplier filter if applicable
                    if (!string.IsNullOrEmpty(cbfiltersupplier.Text))
                    {
                        com.Parameters.AddWithValue("@supname", "%" + cbfiltersupplier.Text.Trim() + "%");
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++; // Increment row counter

                            // Parse monetary values
                            decimal.TryParse(dr["totalamountPo"].ToString(), out decimal totalAmountPO);
                            decimal.TryParse(dr["totalamountRo"].ToString(), out decimal totalAmountRO);

                            // Parse and format dates
                            string formattedTransactionDate = dr["transdate"] != DBNull.Value && DateTime.TryParse(dr["transdate"].ToString(), out DateTime transactionDate)
                                ? transactionDate.ToString("yyyy-MM-dd")
                                : string.Empty;

                            string formattedDeliveredDate = dr["delivereddate"] != DBNull.Value && DateTime.TryParse(dr["delivereddate"].ToString(), out DateTime deliveredDate)
                                ? deliveredDate.ToString("yyyy-MM-dd")
                                : string.Empty;

                            // Add row to DataGridView
                            ROdgv.Rows.Add(
                                i,
                                dr["PoNo"].ToString(),
                                formattedTransactionDate,
                                dr["supname"].ToString(),
                                dr["numItems"].ToString(),
                                "₱" + totalAmountPO.ToString("N2"),
                                formattedDeliveredDate,
                                "₱" + totalAmountRO.ToString("N2"),
                                dr["status"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Display error message with context
                MessageBox.Show("Error loading purchase orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close(); // Ensure connection is closed
                }
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            ShowProductList list = new ShowProductList(this);
            list.ShowProduct();
            list.ShowDialog();
        }

        private void RObtn_Click(object sender, EventArgs e)
        {
            if (ROdgv.SelectedRows.Count > 0)
            {
                string poNumber = ROdgv.SelectedRows[0].Cells["PoNumber"].Value.ToString();
                string supplierName = ROdgv.SelectedRows[0].Cells["supname"].Value.ToString();
                RODetails ro = new RODetails();
                ro.lblPoNo.Text = poNumber;
                ro.lblsupplier.Text = supplierName;
                ro.ReceiveOrder();
                ro.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a row to proceed.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void ROdgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (ROdgv.CurrentRow != null)
                {
                    int i = ROdgv.CurrentRow.Index; // Get the index of the currently selected row

                    // Fetch PoNo and suppliername values from the DataGridView
                    PoNo = ROdgv[1, i].Value != null ? ROdgv[1, i].Value.ToString() : string.Empty;
                    suppliername = ROdgv[3, i].Value != null ? ROdgv[3, i].Value.ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbfiltersupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReceiveOrder();
        }

        public void PopulateSupplier()
        {
            try
            {
                // Clear existing items in the ComboBox
                cbfiltersupplier.Items.Clear();

                // Open the database connection
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // SQL query to get distinct supplier names
                string query = "SELECT DISTINCT supname FROM tablePurchaseOrders WHERE supname IS NOT NULL";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Add each supplier name to the ComboBox
                        cbfiltersupplier.Items.Add(dr["supname"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                 }
            }
        }

        private void ROdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (ROdgv.Columns[e.ColumnIndex].Name == "delete" && e.RowIndex >= 0)
            {
                string poNo = ROdgv.Rows[e.RowIndex].Cells["POnumber"].Value.ToString();
                DialogResult confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete the purchase order with PoNo: {poNo}?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmResult == DialogResult.Yes)
                {
                    // Delete the record from the database
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(dbcon.DataConnection()))
                        {
                            connection.Open();
                            string query = "DELETE FROM tablePurchaseOrders WHERE PoNo = @PoNo";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@PoNo", poNo);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    // Refresh the DataGridView to reflect changes
                                    ROdgv.Rows.RemoveAt(e.RowIndex);
                                }
                                else
                                {
                                    MessageBox.Show("No record found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ROdgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >=0)
            {
                string name = ROdgv.Columns[e.ColumnIndex].Name;

                if(name == "POnumber")
                {
                    string PONO = ROdgv.Rows[e.RowIndex].Cells["POnumber"].Value.ToString();
                    string date = dgvPO.Rows[e.RowIndex].Cells["date"].Value.ToString();
                    string suppliername = ROdgv.Rows[e.RowIndex].Cells["supname"].Value.ToString();
                    string status = ROdgv.Rows[e.RowIndex].Cells["stats"].Value.ToString();
                    ShowPO(PONO, date,suppliername, status);
                }
            }

        }
        private void ShowPO(string PONO, string date, string supplier, string stats)
        {
            PODetails details = new PODetails(PONO,date, supplier, stats);
            details.lblpoNo.Text = PONO;
            details.lblsupplier.Text = supplier;
            details.lbltransdate.Text = date;
            details.PurchaseOrderDetails(stats);
            details.ShowDialog();
        }

        private void cbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            PurchaseOrder();
        }
        public void PopulateSuppliers()
        {
            try
            {
                cbSuppliers.Items.Clear();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT DISTINCT supname FROM tablePurchaseOrders WHERE supname IS NOT NULL";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        // Add each supplier name to the ComboBox
                        cbSuppliers.Items.Add(dr["supname"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}

