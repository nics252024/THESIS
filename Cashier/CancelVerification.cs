using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class CancelVerification : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        CancelOrder co;
        
        public CancelVerification(CancelOrder co)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            this.co = co;
           
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void CancelSales(string user, string actionType)
        {
            // Remove peso signs and trim whitespace before parsing
            string priceWithoutPeso = co.lblprice.Text.Replace("₱", "").Trim();

            con.Open();
            SqlCommand com = new SqlCommand("INSERT INTO tableCancel (transactionNo, pcode, price, qty, transdate, voidby, cancelledby, reason, action, actionType) VALUES (@transactionNo, @pcode, @price, @qty, @transdate, @voidby, @cancelledby, @reason, @action, @actionType)", con);
            com.Parameters.AddWithValue("@transactionNo", co.lbltransactionID.Text);
            com.Parameters.AddWithValue("@pcode", co.lblpcode.Text);
            com.Parameters.AddWithValue("@price", double.Parse(priceWithoutPeso));
            com.Parameters.AddWithValue("@qty", int.Parse(co.txtCancelQty.Text));
            com.Parameters.AddWithValue("@transdate", DateTime.Now);
            com.Parameters.AddWithValue("@voidby", user);
            com.Parameters.AddWithValue("@cancelledby", co.txtCancelledBy.Text);
            com.Parameters.AddWithValue("@reason", co.txtReasons.Text);
            com.Parameters.AddWithValue("@action", co.cbATI.Text);
            com.Parameters.AddWithValue("@actionType", actionType); // Refund or Exchange
            com.ExecuteNonQuery();
            con.Close();
        }


        private void adminverification_KeyDown_1(object sender, KeyEventArgs e)
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

        private void btnConfirmAdmin_Click(object sender, EventArgs e)
        {
            ReturnExchange exchange = (ReturnExchange)Application.OpenForms["ReturnExchange"];
            try
            {
                if (!string.IsNullOrEmpty(txtpassword.Text))
                {
                    con.Open();
                    string query = "SELECT * FROM tableUserAccount WHERE username = @username AND password = @password AND role = 'Admin'";
                    com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@username", txtusername.Text);
                    com.Parameters.AddWithValue("@password", txtpassword.Text);
                    dr = com.ExecuteReader();

                    if (dr.Read())
                    {
                        string user = dr["username"].ToString();
                        string name = dr["name"].ToString();
                        string role = dr["role"].ToString();
                        dr.Close();
                        con.Close();

                        string actionType = co.cbActionType.Text; // "Refund" or "Exchange"
                        bool returnToInventory = co.cbATI.Text == "YES";

                        // Log the cancel action in tableCancel
                        CancelSales(user, actionType);

                        con.Open();

                        if (actionType == "Refund")
                        {
                            int qty = int.Parse(co.txtCancelQty.Text);
                            double price = double.Parse(co.lblprice.Text.Replace("₱", "").Trim());
                            double total = qty * price;

                            if (returnToInventory)
                            {
                                // Update sales and add back to inventory
                                SqlCommand updateSalesForRefund = new SqlCommand(
                                    "UPDATE tableSales SET qty = qty - @cancelQty, total = total - @total WHERE transactionNo = @transactionNo AND pcode = @pcode", con);
                                updateSalesForRefund.Parameters.AddWithValue("@cancelQty", qty);
                                updateSalesForRefund.Parameters.AddWithValue("@transactionNo", co.lbltransactionID.Text);
                                updateSalesForRefund.Parameters.AddWithValue("@pcode", co.lblpcode.Text);
                                updateSalesForRefund.Parameters.AddWithValue("@total", total);
                                updateSalesForRefund.ExecuteNonQuery();

                                SqlCommand updateInventory = new SqlCommand(
                                    "UPDATE tablePODetails SET qtyRO = qtyRO + @cancelQty WHERE id = @id", con);
                                updateInventory.Parameters.AddWithValue("@cancelQty", qty);
                                updateInventory.Parameters.AddWithValue("@id", co.lblpo.Text);
                                updateInventory.ExecuteNonQuery();
                            }
                            else
                            {
                                // Set sales quantity and total to zero
                                SqlCommand cancelSales = new SqlCommand(
                                    "UPDATE tableSales SET qty = 0, total = 0 WHERE transactionNo = @transactionNo AND pcode = @pcode", con);
                                cancelSales.Parameters.AddWithValue("@transactionNo", co.lbltransactionID.Text);
                                cancelSales.Parameters.AddWithValue("@pcode", co.lblpcode.Text);
                                cancelSales.ExecuteNonQuery();
                            }
                        }
                        else if (actionType == "Exchange")
                        {
                            co.cbATI.Enabled = false; 
                        }

                        // Update tableCancel status
                        SqlCommand updateCancelStatus = new SqlCommand(
                            "UPDATE tableCancel SET status = 'Complete' WHERE transactionNo = @transactionNo AND pcode = @pcode", con);
                        updateCancelStatus.Parameters.AddWithValue("@transactionNo", co.lbltransactionID.Text);
                        updateCancelStatus.Parameters.AddWithValue("@pcode", co.lblpcode.Text);
                        updateCancelStatus.ExecuteNonQuery();

                        con.Close();

                        // Log the action with the customized notification
                        string details = actionType == "Refund" ? "Transaction refunded." : "Transaction exchanged.";
                        LogNotification(co.lbltransactionID.Text, name, details);

                        MessageBox.Show("Successfully completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        this.Dispose();
                        co.RefreshProductList();
                        co.Dispose();
                        exchange.ShowSalesRecord();
                        Log(name, $"The {role} has successfully adjusted the transaction.", actionType);
                    }
                    else
                    {
                        dr.Close();
                        con.Close();
                        MessageBox.Show("Access Denied! Only admins are allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Password field cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    com.Parameters.AddWithValue("@ActionType", details.Contains("refunded") ? "Refund" : "Exchange");
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
    }
}
