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
    public partial class CreateNewSupplier : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        public CreateNewSupplier()
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
           if (string.IsNullOrWhiteSpace(txtsupplier.Text) ||
           string.IsNullOrWhiteSpace(txtcontact.Text) ||
           string.IsNullOrWhiteSpace(txtemail.Text) ||
           string.IsNullOrWhiteSpace(txtconnum.Text) ||
           string.IsNullOrWhiteSpace(txtaddress.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            CreateNewProduct product = (CreateNewProduct)Application.OpenForms["CreateNewProduct"];
            try
            {
                if (MessageBox.Show("Save the Suppliername?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    // Check if the supplier name already exists
                    com = new SqlCommand("SELECT COUNT(*) FROM tableSupplier WHERE supname = @supname", con);
                    com.Parameters.AddWithValue("@supname", txtsupplier.Text);
                    int suppliername = (int)com.ExecuteScalar(); // ExecuteScalar returns the count

                    if (suppliername == 0)
                    {
                        // Insert new supplier if it doesn't exist
                        com = new SqlCommand("INSERT INTO tableSupplier (supname, contactperson, description, email, contactnum, address) VALUES (@supname, @contactperson, @description, @email, @contactnum, @address)", con);
                        com.Parameters.AddWithValue("@supname", txtsupplier.Text);
                        com.Parameters.AddWithValue("@contactperson", txtcontact.Text);
                        com.Parameters.AddWithValue("@email", txtemail.Text);
                        com.Parameters.AddWithValue("@contactnum", txtconnum.Text);
                        com.Parameters.AddWithValue("@address", txtaddress.Text);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Supplier Information has been Saved.");

                        Clear(); // Clear input fields
                        this.Dispose(); // Close the current form
                        product.Suppliername(); // Assuming this method repopulates the ComboBox

                        Supplier supplier = new Supplier();
                        supplier.ShowSupplier(); // Update supplier list or UI
                    }
                    else
                    {
                        MessageBox.Show("This supplier already exists.");
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
                    con.Close(); // Ensure the connection is closed
            }
        }

        private void Clear()
        {
            btnsave.Enabled = true;
            txtsupplier.Clear();
            txtsupplier.Focus();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtconnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-numeric input
            }
        }

        private void CreateNewSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave_Click(sender, e); 
                e.Handled = true; 
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}
