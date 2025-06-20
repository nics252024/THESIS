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
    public partial class DiscountVerification : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        Discount disc;
        
        public DiscountVerification(Discount disc)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            this.disc = disc;
            
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnConfirmAdmin_Click(object sender, EventArgs e)
        {
            CashierModule cashier = (CashierModule)Application.OpenForms["CashierModule"];
            try
            {
                if (!string.IsNullOrEmpty(txtpassword.Text))
                {
                    string user = cashier.lblname.Text;
                    con.Open(); // Ensure the connection is open
                    com = new SqlCommand("SELECT * FROM tableUserAccount WHERE username = @username AND password = @password AND role = 'Admin'", con);
                    com.Parameters.AddWithValue("@username", txtusername.Text);
                    com.Parameters.AddWithValue("@password", txtpassword.Text);
                    dr = com.ExecuteReader();

                    if (dr.Read())
                    {
                        dr.Close();
                        con.Close();

                        if (MessageBox.Show("Click yes to add discount.", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            com = new SqlCommand("UPDATE tableSales SET discpercent = @discpercent WHERE id = @id", con);
                            com.Parameters.AddWithValue("@discpercent", double.Parse(((dynamic)disc.cbdiscount.SelectedItem).Value));
                            com.Parameters.AddWithValue("@id", int.Parse(disc.lblID.Text));
                            com.ExecuteNonQuery();
                            con.Close();

                            // Log the action in tableAdminNotifications
                            LogNotification(cashier.lbltransaction.Text, user, "Discount applied");

                            // Log the action in tableLogHistory
                            Log(user, "Applied discount to transaction", "Discount");

                            cashier.ShowSales();
                            this.Close();
                            disc.Dispose();
                        }
                    }
                    else
                    {
                        dr.Close();
                        con.Close();
                        MessageBox.Show("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message);
            }
        }
        private void LogNotification(string transactionNo, string cashierName, string details)
        {
            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(
                    "INSERT INTO tableAdminNotifications (TransactionID, ActionType, CashierName, Details) " +
                    "VALUES (@TransactionID, @ActionType, @CashierName, @Details)", con))
                {
                    com.Parameters.AddWithValue("@TransactionID", transactionNo);
                    com.Parameters.AddWithValue("@ActionType", "Discount");
                    com.Parameters.AddWithValue("@CashierName", cashierName);
                    com.Parameters.AddWithValue("@Details", details);
                    com.ExecuteNonQuery();
                }
            }
        }
        private void Log(string user, string activity, string action)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO tableLogHistory (username, Date, activity ,action) VALUES (@username, @date, @activity, @action)", con);
                command.Parameters.AddWithValue("@username", user);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@activity", activity);
                command.Parameters.AddWithValue("@action", action);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void DiscountVerification_KeyDown(object sender, KeyEventArgs e)
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
    }
}
