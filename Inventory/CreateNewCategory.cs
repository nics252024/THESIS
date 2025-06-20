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
    public partial class CreateNewCategory : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader read;
        DatabaseConnection dbcon = new DatabaseConnection();
        public CreateNewCategory()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            CreateNewProduct product = (CreateNewProduct)Application.OpenForms["CreateNewProduct"];
            try
            {
                if (MessageBox.Show("Save the Category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                    {
                        con.Open();

                        // Check if the category already exists
                        using (SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM tableCategory WHERE Category = @category", con))
                        {
                            com.Parameters.AddWithValue("@category", txtcategory.Text);
                            int categoryCount = (int)com.ExecuteScalar();

                            if (categoryCount == 0)
                            {
                                // Category does not exist, proceed with insertion
                                com.CommandText = "INSERT INTO tableCategory(Category) VALUES (@category)";
                                com.ExecuteNonQuery();
                                MessageBox.Show("Category has been Saved.");

                                clear();
                                this.Dispose();
                                product.Category(); // Assuming this method repopulates the ComboBox

                                Category categoryForm = new Category();
                                categoryForm.CategoryList(); 
                            }
                            else
                            {
                                // Category already exists, show a message
                                MessageBox.Show("This category already exists.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close(); // Ensure the connection is closed
            }
        }
        private void clear()
        {
            btnsave.Enabled = true;
            txtcategory.Clear();
            txtcategory.Focus();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateNewCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}
