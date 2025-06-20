using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class ShowProductList : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        StockLevelsList stock;
        public ShowProductList(StockLevelsList stock)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            this.stock = stock;
            PopulateSupplier();
           
        }
        public void ShowProduct()
        {
            int i = 0; // Row counter
            dgvList.Rows.Clear(); // Clear existing rows

            // SQL query to fetch product data, with search condition for product name
            string query = @"
SELECT 
    p.pcode, 
    p.productname, 
    p.supplier,
    ISNULL(SUM(s.qtyRO), 0) AS totalQty, -- Total quantity ordered
    p.reorder -- Reorder level
FROM 
    tableProductList AS p
LEFT JOIN 
    tablePODetails AS s 
    ON p.pcode = s.pcode
LEFT JOIN 
    tablePurchaseOrders AS PO 
    ON s.PoNO = PO.PoNO
WHERE 
    p.supplier = @supname -- Filter by supplier name
    AND (@search IS NULL OR p.productname LIKE @search) -- Search condition
GROUP BY 
    p.pcode, p.productname, p.supplier, p.reorder
HAVING 
    ISNULL(SUM(s.qtyRO), 0) = 0 -- Out of stock condition
    OR ISNULL(SUM(s.qtyRO), 0) <= p.reorder; -- Below reorder level condition";

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection())) // Replace with your connection logic
            {
                try
                {
                    con.Open(); // Open the connection

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // Add parameters for supplier name and search text
                        com.Parameters.AddWithValue("@supname", cbSuppliers.Text);

                        // Add search parameter, using NULL if txtsearch is empty
                        string search = string.IsNullOrWhiteSpace(txtsearchproduct.Text) ? null : "%" + txtsearchproduct.Text.Trim() + "%";
                        com.Parameters.AddWithValue("@search", (object)search ?? DBNull.Value);

                        using (SqlDataReader dr = com.ExecuteReader()) // Execute the query
                        {
                            while (dr.Read()) // Iterate through the result set
                            {
                                i++; // Increment the row counter

                                // Add a row to the DataGridView with the fetched data
                                dgvList.Rows.Add(
                                    i, // Row number
                                    dr["pcode"].ToString(), // Product code
                                    dr["productname"].ToString(), // Product name
                                    dr["totalQty"].ToString(), // Total quantity
                                    dr["reorder"].ToString() // Reorder level
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display any errors that occur during execution
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    con.Close(); // Ensure the connection is closed even if an error occurs
                }
            }
        }




        public void PopulateSupplier()
        {
            cbSuppliers.Items.Clear();

            string query = "SELECT DISTINCT supplier FROM tableProductList"; // Query to get unique supplier names

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection())) // Replace with your connection logic
            {
                try
                {
                    con.Open(); // Open the connection

                    using (SqlCommand com = new SqlCommand(query, con))
                    using (SqlDataReader dr = com.ExecuteReader()) // Execute the query
                    {
                        while (dr.Read()) // Iterate through the result set
                        {
                            cbSuppliers.Items.Add(dr["supplier"].ToString()); // Add each supplier name to the ComboBox
                        }
                    }

                    if (cbSuppliers.Items.Count > 0)
                        cbSuppliers.SelectedIndex = 0; // Optionally select the first item by default
                }
                catch (Exception ex)
                {
                    // Display any errors that occur during execution
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    con.Close(); // Ensure the connection is closed even if an error occurs
                }
            }
        }



        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            ShowProduct();
        }

        private void ShowProductList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private string GeneratePONumber()
        {
            string poNumber;
            string prefix = "PO";
            string datePart = DateTime.Now.ToString("yyyyMMdd"); // e.g., 20240929 for September 29, 2024

            // Query to count the number of POs on the current day
            string sql = "SELECT COUNT(*) FROM tablePurchaseOrders WHERE CONVERT(date, transdate) = CONVERT(date, GETDATE())";

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();  // Ensure the connection is open

                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    int count = (int)com.ExecuteScalar();
                    int sequentialNumber = count + 1; // Increment by 1 to get the next sequential number

                    // Generate the PO number
                    poNumber = $"{prefix}-{datePart}-{sequentialNumber.ToString("D4")}"; // e.g., PO-20240929-0001
                }
            }

            return poNumber;
        }

        private List<string> selectedProducts = new List<string>();
        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvList.Columns[e.ColumnIndex].Name == "Select")
            {
                DataGridViewRow row = dgvList.Rows[e.RowIndex];
                string productName = row.Cells["productname"].Value.ToString();

                if (row.Cells["Select"].Value != null && (bool)row.Cells["Select"].Value)
                {
                    // Deselect the product
                    row.Cells["Select"].Value = false;
                    selectedProducts.Remove(productName);
                }
                else
                {
                    // Select the product
                    row.Cells["Select"].Value = true;
                    if (!selectedProducts.Contains(productName))
                        selectedProducts.Add(productName);
                }
            }
        }

        private void btnPOdetails_Click(object sender, EventArgs e)
        {
            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Please select at least one product before proceeding.", "No Products Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(cbSuppliers.Text))
            {
                MessageBox.Show("Please select a supplier.", "Missing Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (MessageBox.Show("Create PO?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();

                    // Generate Purchase Order Number
                    string poNumber = GeneratePONumber();

                    // Insert into tablePurchaseOrders
                    SqlCommand com = new SqlCommand(@"
                INSERT INTO tablePurchaseOrders 
                (PoNO, transdate, supname, numItems, totalamountPo, delivereddate, totalamountRo, status) 
                VALUES 
                (@PoNO, @transdate, @supname, @numItems, @totalamountPo, @delivereddate, @totalamountRo, @status)", con);

                    com.Parameters.AddWithValue("@PoNO", poNumber);
                    com.Parameters.AddWithValue("@transdate", DateTime.Now.ToString("yyyy-MM-dd"));
                    com.Parameters.AddWithValue("@supname", cbSuppliers.Text);
                    com.Parameters.AddWithValue("@numItems", selectedProducts.Count);
                    com.Parameters.AddWithValue("@totalamountPo", DBNull.Value);
                    com.Parameters.AddWithValue("@delivereddate", DBNull.Value);
                    com.Parameters.AddWithValue("@totalamountRo", DBNull.Value);
                    com.Parameters.AddWithValue("@status", "In Progress");
                    com.ExecuteNonQuery();

                    // Insert into tablePODetails
                    decimal totalAmountPO = 0;

                    foreach (string productName in selectedProducts)
                    {
                        SqlCommand reorderCommand = new SqlCommand(@"
                    SELECT reorder AS reorder, supprice, pcode 
                    FROM tableProductList 
                    WHERE productname = @productname", con);
                        reorderCommand.Parameters.AddWithValue("@productname", productName);

                        int qtyPO = 0;
                        decimal supprice = 0;
                        string pcode = "";

                        using (SqlDataReader reader = reorderCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                qtyPO = Convert.ToInt32(reader["reorder"]);
                                supprice = Convert.ToDecimal(reader["supprice"]);
                                pcode = reader["pcode"].ToString();
                            }
                        }

                        decimal amountPO = qtyPO * supprice;

                        SqlCommand productCommand = new SqlCommand(@"
                    INSERT INTO tablePODetails 
                    (PoNO, productname, pcode, qtyPO, pricePO, amountPO) 
                    VALUES 
                    (@PoNO, @productname, @pcode, @qtyPO, @pricePO, @amountPO)", con);

                        productCommand.Parameters.AddWithValue("@PoNO", poNumber);
                        productCommand.Parameters.AddWithValue("@pcode", pcode);
                        productCommand.Parameters.AddWithValue("@productname", productName);
                        productCommand.Parameters.AddWithValue("@qtyPO", qtyPO);
                        productCommand.Parameters.AddWithValue("@pricePO", supprice);
                        productCommand.Parameters.AddWithValue("@amountPO", amountPO);
                        productCommand.ExecuteNonQuery();

                        totalAmountPO += amountPO;
                    }

                    // Update totalamountPo in tablePurchaseOrders
                    SqlCommand updatePOCommand = new SqlCommand(@"
                UPDATE tablePurchaseOrders 
                SET totalamountPo = @totalamountPo 
                WHERE PoNO = @PoNO", con);

                    updatePOCommand.Parameters.AddWithValue("@totalamountPo", totalAmountPO);
                    updatePOCommand.Parameters.AddWithValue("@PoNO", poNumber);
                    updatePOCommand.ExecuteNonQuery();

                    MessageBox.Show("Purchase Order Created Successfully.");
                   
                    stock.ReceiveOrder();
                    this.Dispose();// Refresh Purchase Orders list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void cbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowProduct();
        }
    }
}
