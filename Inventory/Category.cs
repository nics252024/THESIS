using Microsoft.ReportingServices.Diagnostics.Internal;
using PIERANGELO.Inventory;
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
    public partial class Category : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader read;
        DatabaseConnection dbcon = new DatabaseConnection();
       
        public Category()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
       
        public void CategoryList()
        {
            int i = 0;
            dataGridViewCategory.Rows.Clear();

            // Open the connection
            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                try
                {
                    con.Open();

                    // SQL query with WHERE clause to filter by category
                    using (SqlCommand com = new SqlCommand("SELECT * FROM tableCategory WHERE category LIKE @category", con))
                    {
                        // Add parameter to SQL command
                        com.Parameters.AddWithValue("@category", "%" + txtsearch.Text + "%"); // Use LIKE for partial matching

                        // Execute the query
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            // Loop through the results and add rows to DataGridView
                            while (read.Read())
                            {
                                i++;
                                dataGridViewCategory.Rows.Add(i, read["id"].ToString(), read["category"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching category list: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    con.Close();
                }
            }
        }

        private void dataGridViewCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dataGridViewCategory.Columns[e.ColumnIndex].Name;

            if (columnName == "Edit")
            {
                // Open the CreateCategory form and populate it with selected data for editing
                CreateCategory cc = new CreateCategory();
                cc.txtcategory.Text = dataGridViewCategory.Rows[e.RowIndex].Cells[2].Value.ToString(); // Category Name
                cc.lblID.Text = dataGridViewCategory.Rows[e.RowIndex].Cells[1].Value.ToString(); // Category ID
                cc.btnsave.Enabled = false;
                cc.btnupdate.Enabled = true;
                cc.ShowDialog(); // Use ShowDialog for modal behavior
            }
            else if (columnName == "Delete")
            {
                // Ask the user for confirmation before deleting the category
                if (MessageBox.Show("Do you want to remove this category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                        {
                            con.Open();

                            // Retrieve the category details before deletion
                            string categoryId = dataGridViewCategory.Rows[e.RowIndex].Cells[1].Value.ToString(); // Category ID
                            string categoryName = dataGridViewCategory.Rows[e.RowIndex].Cells[2].Value.ToString(); // Category Name

                            // Check if the category is being used in the tableProductList
                            string checkQuery = "SELECT COUNT(*) FROM tableProductList WHERE cid = @cid";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                            {
                                checkCmd.Parameters.AddWithValue("@cid", categoryId);
                                int productCount = (int)checkCmd.ExecuteScalar();

                                if (productCount > 0)
                                {
                                    MessageBox.Show($"Category '{categoryName}' cannot be deleted because it is being used in products.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Stop further execution if the category is in use
                                }
                            }

                            // Insert the deleted category details into tableDeletedCategory
                            using (SqlCommand insertCmd = new SqlCommand(
                                "INSERT INTO tableDeletedCategory (id, category, deletedDate) VALUES (@id, @category, @deletedDate)", con))
                            {
                                insertCmd.Parameters.AddWithValue("@id", categoryId);
                                insertCmd.Parameters.AddWithValue("@category", categoryName);
                                insertCmd.Parameters.AddWithValue("@deletedDate", DateTime.Now); // Timestamp for deletion
                                insertCmd.ExecuteNonQuery();
                            }

                            // Now delete the category from the tableCategory
                            using (SqlCommand deleteCmd = new SqlCommand("DELETE FROM tableCategory WHERE id = @id", con))
                            {
                                deleteCmd.Parameters.AddWithValue("@id", categoryId);
                                deleteCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Category successfully removed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the category list after deletion
                            CategoryList();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting category: " + ex.Message);
                    }
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            CategoryList();
        }


        private void btncreatenew_Click_1(object sender, EventArgs e)
        {
            CreateCategory category = new CreateCategory();
            category.ShowDialog();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Category_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}
