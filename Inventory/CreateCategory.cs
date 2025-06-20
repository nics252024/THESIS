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

namespace PIERANGELO.Inventory
{
    public partial class CreateCategory : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader read;
        DatabaseConnection dbcon = new DatabaseConnection();

        public CreateCategory()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void clear()
        {
            btnsave.Enabled = true;
            btnupdate.Enabled = false;
            txtcategory.Clear();
            txtcategory.Focus();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            Category category = (Category)Application.OpenForms["Category"];
            try
            {
                if (MessageBox.Show("Save the Category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();

                    // Check if the category already exists
                    com = new SqlCommand("SELECT COUNT(*) FROM tableCategory WHERE Category = @category", con);
                    com.Parameters.AddWithValue("@category", txtcategory.Text);
                    int categoryCount = (int)com.ExecuteScalar();

                    if (categoryCount == 0)
                    {
                        // Category does not exist, proceed with insertion
                        com = new SqlCommand("INSERT INTO tableCategory(Category) VALUES (@category)", con);
                        com.Parameters.AddWithValue("@category", txtcategory.Text);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Category has been Saved.");
                        clear();
                        category.CategoryList();
                        this.Close();
                    }
                    else
                    {
                        // Category already exists, show a message
                        MessageBox.Show("This category already exists.");
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Category category = (Category)Application.OpenForms["Category"];
            try
            {
                if (MessageBox.Show("Want to Update Category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("UPDATE tableCategory set category = @category where id like '" + lblID.Text + "'", con);
                    com.Parameters.AddWithValue("@category", txtcategory.Text);
                    com.ExecuteNonQuery();
                    con.Close(); ;
                    MessageBox.Show("Update Successfully");
                    category.CategoryList();
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CreateCategory_KeyDown(object sender, KeyEventArgs e)
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
