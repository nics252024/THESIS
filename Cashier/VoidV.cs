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
    public partial class VoidV : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        public VoidV()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
        }

        private void btnConfirmAdmin_Click(object sender, EventArgs e)
        {
            CashierModule cashier = (CashierModule)Application.OpenForms["CashierModule"];

            // Check if password field is empty
            if (string.IsNullOrEmpty(txtpassword.Text))
            {
                MessageBox.Show("Please enter the password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string user = string.Empty;

                // Database connection and query
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("SELECT username FROM tableUserAccount WHERE username = @username AND password = @password AND role = 'Admin'", con))
                    {
                        com.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                        com.Parameters.AddWithValue("@password", txtpassword.Text.Trim());

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                user = dr["username"].ToString();
                            }
                        }
                    }
                }

                // Check if the user was found
                if (string.IsNullOrEmpty(user))
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirm void operation
                if (MessageBox.Show("Do you want to void the selected row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cashier.dgSales.CurrentCell != null)
                    {
                        string transactionNo = cashier.lbltransaction.Text;
                        Log(user, $"Successfully voided transaction: {transactionNo}", "Void");
                        VoidSpecificRow(cashier.dgSales, cashier.dgSales.CurrentCell.RowIndex);
                        cashier.ShowSales();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No row is selected to void.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Log(string user, string activity, string action)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO tableLogHistory (username, Date, activity, action) VALUES (@username, @Date, @activity, @action)", con))
                    {
                        command.Parameters.AddWithValue("@username", user);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        command.Parameters.AddWithValue("@activity", activity);
                        command.Parameters.AddWithValue("@action", action);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while logging activity: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void VoidSpecificRow(DataGridView dgSales, int rowIndex)
        {
            try
            {
                // Validate row index and DataGridView content
                if (rowIndex < 0 || dgSales.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid row selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Retrieve product ID from the selected row
                string id = dgSales.Rows[rowIndex].Cells["proId"].Value?.ToString(); // Ensure column name matches your database

                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Invalid product ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Database operation to delete the row
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("DELETE FROM tableSales WHERE id = @id", con))
                    {
                        com.Parameters.AddWithValue("@id", id);
                        int rowsAffected = com.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Selected row has been voided successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No row was voided. Please check the product ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while voiding the row: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VoidV_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnConfirmAdmin_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

}
