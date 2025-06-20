using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace PIERANGELO
{
    public partial class Payment : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        CashierModule cm;
        public Payment(CashierModule cm)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.cm = cm;    
            this.KeyPreview = true;
        }
        private async Task ProcessTransactionAsync()
        {
            try
            {
                double changeAmount = 0;
                double cashAmount = 0;

                // Attempt to parse the cash amount
                if (!double.TryParse(txtCash.Text.Replace("₱", "").Trim(), out cashAmount) || cashAmount <= 0)
                {
                    MessageBox.Show("Invalid or inadequate cash amount. Please enter a correct amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Attempt to parse the change amount
                if (!double.TryParse(txtChange.Text.Replace("₱", "").Trim(), out changeAmount) || changeAmount < 0)
                {
                    MessageBox.Show("Inadequate amount, enter the correct amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    await con.OpenAsync();

                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            for (int i = 0; i < cm.dgSales.Rows.Count; i++)
                            {
                                string salesId = cm.dgSales.Rows[i].Cells[2].Value.ToString();
                                int qty = int.Parse(cm.dgSales.Rows[i].Cells[8].Value.ToString());
                                string id = cm.dgSales.Rows[i].Cells[1].Value.ToString();
                                

                                using (SqlCommand cmd = new SqlCommand("UpdateSales", con, transaction))
                                {

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@input_id", id);
                                    cmd.Parameters.AddWithValue("@input_quantity", qty);
                                    cmd.Parameters.AddWithValue("@sales_id", salesId);
                                    cmd.CommandTimeout = 300; // Increase the command timeout to 5 minutes

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            // Get payment method ID for cash
                            using (SqlCommand com = new SqlCommand("SELECT PaymentMethodID FROM PaymentMethods WHERE MethodName = 'Cash'", con, transaction))
                            {
                                object paymentMethodId = await com.ExecuteScalarAsync();
                                if (paymentMethodId != null)
                                {
                                    // Update sales with payment method
                                    for (int i = 0; i < cm.dgSales.Rows.Count; i++)
                                    {
                                        string salesId = cm.dgSales.Rows[i].Cells[2].Value.ToString();

                                        using (SqlCommand updateCmd = new SqlCommand("UpdateSalesWithPaymentMethod", con, transaction))
                                        {
                                            updateCmd.CommandType = CommandType.StoredProcedure;
                                            updateCmd.Parameters.AddWithValue("@sales_id", salesId);
                                            updateCmd.Parameters.AddWithValue("@payment_method_id", paymentMethodId);
                                            await updateCmd.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            // Commit the transaction
                            transaction.Commit();

                            // Print the receipt
                            ReceiptPrint receipt = new ReceiptPrint(cm);
                            receipt.PrintReceipt(cashAmount.ToString("#,##0.00"), changeAmount.ToString("#,##0.00"));
                            receipt.ShowDialog();
                            

                            MessageBox.Show("Transaction Successfully Saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cm.GetTransaction();
                            cm.ShowSales();
                            cm.ShowProduct();
                            this.Dispose();
                        }
                        catch
                        {
                            // Rollback the transaction if any command fails
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnEnter_Click(object sender, EventArgs e)
        {
             await ProcessTransactionAsync();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Remove peso sign and parse the text as double
                double sales = double.Parse(txtSales.Text.Replace("₱", ""));
                double cash = double.Parse(txtCash.Text.Replace("₱", ""));

                // Calculate change
                double change = cash - sales;

                // Format and display the change with peso sign
                txtChange.Text = "₱" + change.ToString("#,##0.00");
            }
            catch (Exception)
            {
                // Handle any errors and display 0.00 with peso sign
                txtChange.Text = "₱0.00";
            }

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Payment_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                btnEnter_Click(sender, e);
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbexactamount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbexactamount.Checked)
                {
                    if (string.IsNullOrEmpty(txtSales.Text))
                    {
                        MessageBox.Show("Please enter a total amount first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string salesText = txtSales.Text.Replace("₱", "").Trim();
                    if (decimal.TryParse(salesText, out decimal totalAmount))
                    {
                        txtCash.Text = totalAmount.ToString("₱#,##0.00"); // Format as currency
                    }
                    else
                    {
                        MessageBox.Show("Invalid total amount. Please check the value in txtSales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                   
                    txtCash.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
