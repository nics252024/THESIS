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
    public partial class GcashPayment : Form
    {
        public string SalesId { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; }

        SqlConnection con = new SqlConnection();
        DatabaseConnection dbcon = new DatabaseConnection();
        CashierModule cm;
       
        public GcashPayment(CashierModule cm)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            this.cm = cm;
        
            
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private async Task ProcessTransactionAsync()
        {
            
            double cashAmount = 0;
            string reference = txtReference.Text;

            // Validate that the reference number is provided
            if (string.IsNullOrEmpty(reference))
            {
                MessageBox.Show("Please enter the reference number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
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

                                // Assuming you have methods to update stock and sales details
                                await UpdateStockAndSalesAsync(con, transaction, id, qty, salesId);
                            }

                            // Get payment method ID for GCash
                            object paymentMethodId = await GetPaymentMethodIdAsync(con, transaction);
                            if (paymentMethodId != null)
                            {
                                // Update sales with payment method and reference
                                await UpdateSalesWithPaymentMethodAsync(con, transaction, paymentMethodId, reference);
                            }
                            else
                            {
                                // Handle case where the payment method ID was not found
                                MessageBox.Show("GCash payment method not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }

                            // Commit the transaction if everything is successful
                            transaction.Commit();

                            // Print receipt
                            //PrintReceiptGcash gcash = new PrintReceiptGcash(cm);
                            //gcash.PrintReceipt(cashAmount.ToString("#,##0.00"), reference);
                            //gcash.ShowDialog();

                            // Update UI and refresh data
                            cm.GetTransaction();
                            cm.ShowSales();
                            cm.ShowProduct();

                            MessageBox.Show("Transaction successfully saved!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Dispose();
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if any command fails
                            transaction.Rollback();
                            MessageBox.Show($"An error occurred during the transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle connection-level or outer exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task UpdateStockAndSalesAsync(SqlConnection con, SqlTransaction transaction, string productId, int quantity, string salesId)
        {
            using (SqlCommand cmd = new SqlCommand("UpdateSales", con, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@input_id", productId);
                cmd.Parameters.AddWithValue("@input_quantity", quantity);
                cmd.Parameters.AddWithValue("@sales_id", salesId);
                cmd.CommandTimeout = 300;

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task<object> GetPaymentMethodIdAsync(SqlConnection con, SqlTransaction transaction)
        {
            using (SqlCommand com = new SqlCommand("SELECT PaymentMethodID FROM PaymentMethods WHERE MethodName = 'GCash'", con, transaction))
            {
                return await com.ExecuteScalarAsync();
            }
        }

        private async Task UpdateSalesWithPaymentMethodAsync(SqlConnection con, SqlTransaction transaction, object paymentMethodId, string reference)
        {
            using (SqlCommand updateCmd = new SqlCommand("UpdateSalesWithPaymentMethod", con, transaction))
            {
                updateCmd.CommandType = CommandType.StoredProcedure;
                updateCmd.Parameters.AddWithValue("@sales_id", SalesId);
                updateCmd.Parameters.AddWithValue("@payment_method_id", paymentMethodId);
                updateCmd.Parameters.AddWithValue("@reference", reference);

                await updateCmd.ExecuteNonQueryAsync();
            }
        }

        private async void btnEnter_Click_1(object sender, EventArgs e)
        {
            await ProcessTransactionAsync();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GcashPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnter_Click_1(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}
