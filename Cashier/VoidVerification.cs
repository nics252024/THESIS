using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class VoidVerification : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        public string VerifiedUser { get; private set; }


        public VoidVerification()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            
        }

        private void btnConfirmAdmin_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtpassword.Text))
            {
                MessageBox.Show("Please enter the password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string user = string.Empty;

                // Authenticate admin credentials
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

                if (string.IsNullOrEmpty(user))
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirmation for voiding all rows
                if (MessageBox.Show("Do you want to void all products?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    VoidAllRows(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void VoidAllRows(string user)
        {
            CashierModule cashier = (CashierModule)Application.OpenForms["CashierModule"];
            if (cashier == null)
            {
                MessageBox.Show("Cashier module is not open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string transactionNo = cashier.lbltransaction.Text.Trim();
            if (string.IsNullOrEmpty(transactionNo))
            {
                MessageBox.Show("Transaction number is missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("DELETE FROM tableSales WHERE transactionNo = @transactionNo", con))
                    {
                        com.Parameters.AddWithValue("@transactionNo", transactionNo);
                        com.ExecuteNonQuery();
                    }
                }

                // Log the void action
                Log(user, $"Successfully voided transaction: {transactionNo}", "Void");

                MessageBox.Show("All rows have been voided successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the DataGridView
                cashier.ShowSales();
                this.Dispose(); // Close the admin confirmation form
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while voiding all rows: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Log(string user, string activity, string action)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO tableLogHistory (username, Date, activity, action) VALUES (@username, @date, @activity, @action)", con))
                    {
                        command.Parameters.AddWithValue("@username", user);
                        command.Parameters.AddWithValue("@date", DateTime.Now);
                        command.Parameters.AddWithValue("@activity", activity);
                        command.Parameters.AddWithValue("@action", action);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while logging the action: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LogNotification(string transactionNo, string cashierName, string actionType)
        {
            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(
                    "INSERT INTO tableAdminNotifications (TransactionID, ActionType, CashierName, Details) " +
                    "VALUES (@TransactionID, @ActionType, @CashierName, @Details)", con))
                {
                    com.Parameters.AddWithValue("@TransactionID", transactionNo);
                    com.Parameters.AddWithValue("@ActionType", actionType);
                    com.Parameters.AddWithValue("@CashierName", cashierName);
                    com.Parameters.AddWithValue("@Details", "Transaction voided."); // Customize details as needed
                    com.ExecuteNonQuery();
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void VoidVerification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirmAdmin_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
