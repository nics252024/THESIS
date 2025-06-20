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
    public partial class ReturnExchange : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();

        public string cashname;
        public ReturnExchange()
        {
            InitializeComponent();
            
            con = new SqlConnection(dbcon.DataConnection());
            datefrom.Value = DateTime.Now;
            dateto.Value = DateTime.Now;
            ShowSalesRecord();
            PopulatePaymentMethods();
            
        }

        public void ShowSalesRecord()
        {
            int a = 0;
            double totalSales = 0;

            dgvSalesRecord.Rows.Clear();

            try
            {
                // Validate the date range
                if (datefrom.Value.Date > dateto.Value.Date)
                {
                    MessageBox.Show("Invalid date range. Please select a valid range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();
                SqlCommand com;
                SqlDataReader dr;

                // Base SQL query
                string baseQuery = @"
            SELECT 
                s.transactionNo,
                s.cashier,
                p.MethodName AS paymentMethod,
                MIN(s.transdate) AS transdate, -- Earliest transaction date
                SUM(s.qty) AS total_qty,
                SUM(s.total) AS total_amount
            FROM 
                tableSales AS s
            INNER JOIN 
                PaymentMethods AS p ON s.paymentMethod = p.paymentMethodId
            WHERE 
                s.transdate BETWEEN @DateFrom AND @DateTo
                AND s.status = 'Sold'
                AND s.transactionNo LIKE @TransactionNo";

                // Add dynamic filtering for cashier and payment method
                if (lblCashiername.Text != "All Cashier")
                {
                    baseQuery += " AND s.cashier = @CashierName";
                }
                if (cbPayment.Text != "All Payment Methods")
                {
                    baseQuery += " AND p.MethodName = @PaymentMethod";
                }

                // Group and sort the results
                baseQuery += @"
            GROUP BY 
                s.transactionNo, 
                s.cashier, 
                p.MethodName
            ORDER BY 
                MIN(s.transdate) DESC";

                com = new SqlCommand(baseQuery, con);

                // Add parameters
                com.Parameters.AddWithValue("@TransactionNo", "%" + txtsearchproduct.Text.Trim() + "%");
                com.Parameters.AddWithValue("@DateFrom", datefrom.Value.Date);
                com.Parameters.AddWithValue("@DateTo", dateto.Value.Date.AddDays(1).AddSeconds(-1));
                if (lblCashiername.Text != "All Cashier")
                {
                    com.Parameters.AddWithValue("@CashierName", lblCashiername.Text);
                }
                if (cbPayment.Text != "All Payment Methods")
                {
                    com.Parameters.AddWithValue("@PaymentMethod", cbPayment.Text);
                }

                dr = com.ExecuteReader();

                // Populate DataGridView
                while (dr.Read())
                {
                    a++;
                    string transDateFormatted = Convert.ToDateTime(dr["transdate"]).ToString("yyyy-MM-dd HH:mm:ss");

                    dgvSalesRecord.Rows.Add(
                        a, // Row number
                        dr["transactionNo"].ToString(), // Transaction number
                        dr["paymentMethod"].ToString(), // Payment method
                        transDateFormatted, // Transaction date
                        dr["total_qty"].ToString(), // Total quantity
                        "₱" + Convert.ToDouble(dr["total_amount"]).ToString("#,##0.00") // Total amount with peso sign
                    );

                    totalSales += Convert.ToDouble(dr["total_amount"]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            // Update the total sales label
            lblTotalSales.Text = $"₱{totalSales:#,##0.00}";
        }




        public void PopulatePaymentMethods()
        {
            cbPayment.Items.Clear(); // Clear any existing items to avoid duplicates
            cbPayment.Items.Add("All Payment Methods"); // Add default option for filtering all methods
            cbPayment.Items.Add("Cash");
            cbPayment.Items.Add("GCash");

            cbPayment.SelectedIndex = 0; // Set default selection to "All Payment Methods"
        }

        private void dgvSalesRecord_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colname = dgvSalesRecord.Columns[e.ColumnIndex].Name;
                if (colname == "transNo")
                {
                    string transno = dgvSalesRecord.Rows[e.RowIndex].Cells["transNo"].Value.ToString();
                    ShowTransac(transno, e.RowIndex); 
                }
            }
        }

        private void ShowTransac(string transno, int rowIndex) 
        {
            ReturnExchangeForm no = new ReturnExchangeForm(transno);
            no.StartPosition = FormStartPosition.CenterScreen;
            no.lblcashiername.Text = lblCashiername.Text;
            no.lbltotalamount.Text = dgvSalesRecord[3, rowIndex].Value.ToString();
            no.ShowTransactionNO();
            no.Show();
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }

        private void dgvSalesRecord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }
    }
}
