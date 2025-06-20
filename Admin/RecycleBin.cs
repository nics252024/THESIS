using PIERANGELO;
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
    public partial class RecycleBin : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;

        public RecycleBin()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            LoadCategories();
            LoadDeletedProducts();
            LoadDeletedCategory();
            this.KeyPreview = true;
            
            
        }
        private void LoadCategories()
        {
            try
            {
                cbCategory.Items.Clear(); // Clear existing items in the ComboBox
                cbCategory.Items.Add("All"); // Option for selecting all categories

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    string query = @"SELECT DISTINCT [category] FROM [PIERANGELO].[dbo].[tableDeletedItems]";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Add each category to the ComboBox
                                cbCategory.Items.Add(dr["category"].ToString());
                            }
                        }
                    }
                }

                // Set the default selected item as "All"
                cbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void LoadDeletedProducts()
        {
         
            try
            {
                int i = 0;
                dgvDeleteItems.Rows.Clear(); // Clear the existing rows in the DataGridView

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    // SQL query to fetch deleted products with optional search and category filter
                    string query = @"SELECT [pcode], [barcode], [productName], [category], [supPrice], [markupPrice], [price], [supplier], [reorder], [deletedAt]
                             FROM [PIERANGELO].[dbo].[tableDeletedItems]
                             WHERE ([productName] LIKE @search OR [barcode] LIKE @search)";

                    // If category is selected, append the condition
                    if (!string.IsNullOrEmpty(cbCategory.Text) && cbCategory.Text != "All")
                    {
                        query += " AND [category] = @category";
                    }

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // Add parameters to avoid SQL injection
                        com.Parameters.AddWithValue("@search", "%" + txtsearchproduct.Text.Trim() + "%");

                        if (!string.IsNullOrEmpty(cbCategory.Text) && cbCategory.Text != "All")
                        {
                            com.Parameters.AddWithValue("@category", cbCategory.Text);
                        }

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                i++;
                                dgvDeleteItems.Rows.Add(
                                    i,
                                    dr["pcode"].ToString(),
                                    dr["barcode"].ToString(),
                                    dr["productName"].ToString(),
                                    dr["category"].ToString(),
                                    "₱" + decimal.Parse(dr["supPrice"].ToString()).ToString("N2"),
                                    "₱" + decimal.Parse(dr["markupPrice"].ToString()).ToString("N2"),
                                    "₱" + decimal.Parse(dr["price"].ToString()).ToString("N2"),
                                    dr["supplier"].ToString(),
                                    dr["reorder"].ToString(),
                                    dr["deletedAt"].ToString()
                                );
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


        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void btnRestore_Click(object sender, EventArgs e)
        {
           
            if  (dgvDeleteItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to restore.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    foreach (DataGridViewRow row in dgvDeleteItems.Rows)
                    {
                        // Check if the row's checkbox is checked
                        bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value); // Adjust "Select" to match the checkbox column name
                        if (!isChecked)
                            continue;

                        string pcode = row.Cells["pcode"].Value.ToString(); // Ensure this column matches your DataGridView column

                        // Retrieve deleted product details
                        string query = @"
                    SELECT p.pcode, p.barcode, p.productName, p.category, p.supPrice, p.markupPrice, p.price, p.supplier, p.reorder
                    FROM tableDeletedItems p
                    WHERE p.pcode = @pcode";

                        using (SqlCommand retrieveCommand = new SqlCommand(query, con))
                        {
                            retrieveCommand.Parameters.AddWithValue("@pcode", pcode);

                            using (SqlDataReader reader = retrieveCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string pcodeValue = reader["pcode"].ToString();
                                    string barcode = reader["barcode"].ToString();
                                    string productName = reader["productName"].ToString();
                                    string category = reader["category"].ToString();
                                    decimal supPrice = reader.IsDBNull(reader.GetOrdinal("supPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("supPrice"));
                                    decimal markupPrice = reader.IsDBNull(reader.GetOrdinal("markupPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("markupPrice"));
                                    decimal price = reader.IsDBNull(reader.GetOrdinal("price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("price"));
                                    string supplier = reader["supplier"].ToString();
                                    int reorder = reader.IsDBNull(reader.GetOrdinal("reorder")) ? 0 : reader.GetInt32(reader.GetOrdinal("reorder"));

                                    // Close the reader before executing another command
                                    reader.Close();

                                    // Get the category ID (cid) based on the category name
                                    string categoryQuery = "SELECT id FROM tableCategory WHERE category = @category";
                                    using (SqlCommand categoryCommand = new SqlCommand(categoryQuery, con))
                                    {
                                        categoryCommand.Parameters.AddWithValue("@category", category);
                                        object cidObj = categoryCommand.ExecuteScalar();
                                        if (cidObj == null)
                                        {
                                            throw new Exception($"Category '{category}' not found in the database.");
                                        }
                                        int cid = Convert.ToInt32(cidObj);

                                        // Insert the restored product into the tableProductList
                                        string insertQuery = @"
                                    INSERT INTO tableProductList (pcode, barcode, productname, cid, supprice, markup, price, supplier, reorder) 
                                    VALUES (@pcode, @barcode, @productname, @cid, @supprice, @markup, @price, @supplier, @reorder)";
                                        using (SqlCommand restoreCommand = new SqlCommand(insertQuery, con))
                                        {
                                            restoreCommand.Parameters.AddWithValue("@pcode", pcodeValue);
                                            restoreCommand.Parameters.AddWithValue("@barcode", barcode);
                                            restoreCommand.Parameters.AddWithValue("@productname", productName);
                                            restoreCommand.Parameters.AddWithValue("@cid", cid);
                                            restoreCommand.Parameters.AddWithValue("@supprice", supPrice);
                                            restoreCommand.Parameters.AddWithValue("@markup", markupPrice);
                                            restoreCommand.Parameters.AddWithValue("@price", price);
                                            restoreCommand.Parameters.AddWithValue("@supplier", supplier);
                                            restoreCommand.Parameters.AddWithValue("@reorder", reorder);

                                            restoreCommand.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }

                        // Remove the restored product from tableDeletedItems
                        string deleteQuery = "DELETE FROM tableDeletedItems WHERE pcode = @pcode";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, con))
                        {
                            deleteCommand.Parameters.AddWithValue("@pcode", pcode);
                            deleteCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Selected products have been restored successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvDeleteItems.Rows.Remove(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteall_Click(object sender, EventArgs e)
        {
            
            int total = dgvDeleteItems.Rows.Cast<DataGridViewRow>().Count(p => Convert.ToBoolean(p.Cells["Select"].Value) == true);

            if (total > 0)
            {
                string message = $"Are you sure you want to delete {total} row(s)?";
                if (MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DeleteSelectedRows();
                }
            }
            else
            {
                MessageBox.Show("No rows selected for deletion.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteSelectedRows()
        {
            
            var rowsToDelete = new List<DataGridViewRow>();

            // Collect rows to delete
            foreach (DataGridViewRow row in dgvDeleteItems.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    rowsToDelete.Add(row);
                }
            }

            if (rowsToDelete.Count == 0)
            {
                MessageBox.Show("No selected rows to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open(); // Open the database connection
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            foreach (var row in rowsToDelete)
                            {
                                string pcode = row.Cells["pcode"].Value.ToString(); // Assuming 'pcode' is the primary key

                                // SQL DELETE statement
                                string deleteQuery = "DELETE FROM [PIERANGELO].[dbo].[tableDeletedItems] WHERE [pcode] = @pcode";
                                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, con, transaction))
                                {
                                    deleteCommand.Parameters.AddWithValue("@pcode", pcode);
                                    deleteCommand.ExecuteNonQuery(); // Execute the DELETE statement
                                }

                                dgvDeleteItems.Rows.Remove(row); // Remove the row from the DataGridView
                            }

                            transaction.Commit(); // Commit the transaction
                            MessageBox.Show("Selected rows have been successfully deleted.");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Rollback the transaction in case of an error
                            MessageBox.Show("Error during deletion: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message);
            }
        }


        private void cbCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadDeletedProducts();
        }

        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            LoadDeletedProducts();
        }

        public void LoadDeletedCategory()
        {
            int i = 0;
            dgvdeletecategory.Rows.Clear();

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                try
                {
                    con.Open();

                    // SQL query with WHERE clause to filter by category
                    string query = "SELECT * FROM tableDeletedCategory WHERE category LIKE @category";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // Add parameter for the search text (using LIKE for partial matching)
                        string searchText = string.IsNullOrEmpty(txtsearchcat.Text) ? "%" : "%" + txtsearchcat.Text + "%";
                        com.Parameters.AddWithValue("@category", searchText);

                        // Execute the query
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            // Loop through the results and add rows to DataGridView
                            while (read.Read())
                            {
                                i++;
                                dgvdeletecategory.Rows.Add(i, read["id"].ToString(), read["category"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Display error message in case of exception
                    MessageBox.Show("Error fetching category list: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed, even in case of exception
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }


        private void txtsearchcat_TextChanged(object sender, EventArgs e)
        {
            LoadDeletedCategory();
        }

        private void btnrestorecat_Click(object sender, EventArgs e)
        {

            if (dgvdeletecategory.Rows.Count == 0)
            {
                MessageBox.Show("No categories to restore.", "No Categories", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool categoryRestored = false;

            foreach (DataGridViewRow row in dgvdeletecategory.Rows)
            {
                // Check if the checkbox in the row is checked
                bool isChecked = Convert.ToBoolean(row.Cells["SelectCat"].Value);
                if (!isChecked)
                    continue;

                // Get the selected category ID and name
                string selectedCategoryID = row.Cells["id"].Value.ToString();
                string selectedCategoryName = row.Cells["cat"].Value.ToString();

                // Confirm if the user wants to restore the category
                if (MessageBox.Show($"Do you want to restore the category '{selectedCategoryName}'?", "Restore Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                    {
                        try
                        {
                            con.Open();

                            // Check if the category already exists in tableCategory
                            string checkQuery = "SELECT COUNT(*) FROM tableCategory WHERE category = @category";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                            {
                                checkCmd.Parameters.AddWithValue("@category", selectedCategoryName);
                                int count = (int)checkCmd.ExecuteScalar();

                                if (count > 0)
                                {
                                    MessageBox.Show($"Category '{selectedCategoryName}' already exists in the active categories.", "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue;
                                }
                            }

                            // Insert the category back into tableCategory
                            string restoreQuery = "INSERT INTO tableCategory (category) VALUES (@category)";
                            using (SqlCommand restoreCmd = new SqlCommand(restoreQuery, con))
                            {
                                restoreCmd.Parameters.AddWithValue("@category", selectedCategoryName);
                                restoreCmd.ExecuteNonQuery();
                            }

                            // Remove the category from tableDeletedCategory
                            string deleteQuery = "DELETE FROM tableDeletedCategory WHERE id = @id";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                            {
                                deleteCmd.Parameters.AddWithValue("@id", selectedCategoryID);
                                deleteCmd.ExecuteNonQuery();
                            }

                            categoryRestored = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error restoring category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }
                    }
                }
            }

            // Refresh the DataGridView if any category was restored
            if (categoryRestored)
            {
                LoadDeletedCategory();
                MessageBox.Show("Selected categories restored successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No categories were restored.", "Restore Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDeleteItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RecycleBin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click_1(sender,e);
            }
        }
    }
}
