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
    public partial class SalesReport : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        public decimal TotalSalesAmount { get; set; }
        public decimal DailySalesAmount { get; set; }
        public string cashiersname;
      
        public SalesReport()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            datefrom.Value = DateTime.Now;
            dateto.Value = DateTime.Now;

            ShowSalesRecord();
            ShowCashier();
            PopulatePaymentMethods();
        }
        public void ShowSalesRecord()
        {
            int a = 0;
            double totalSales = 0;

            dgvSalesRecord.Rows.Clear();

            try
            {
                con.Open();
                SqlCommand com;
                SqlDataReader dr;

                // Base query with grouping and aggregation
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
            PaymentMethods AS p
        ON 
            s.paymentMethod = p.paymentMethodId
        WHERE 
            s.transdate BETWEEN @DateFrom AND @DateTo 
            AND s.status = 'Sold'
            AND s.transactionNo LIKE @TransactionNo";

                // Add cashier and payment method filters
                if (cbCashier.Text == "All Cashier")
                {
                    if (cbPayment.Text != "All Payment Methods")
                    {
                        baseQuery += " AND p.MethodName = @PaymentMethod";
                    }
                }
                else
                {
                    baseQuery += " AND s.cashier = @CashierName";
                    if (cbPayment.Text != "All Payment Methods")
                    {
                        baseQuery += " AND p.MethodName = @PaymentMethod";
                    }
                }

                // Group by transaction number, cashier, and payment method
                baseQuery += @"
        GROUP BY 
            s.transactionNo, 
            s.cashier, 
            p.MethodName
        ORDER BY 
            MIN(s.transdate) DESC"; // Sort by earliest transaction date

                com = new SqlCommand(baseQuery, con);

                // Add parameters
                com.Parameters.AddWithValue("@TransactionNo", "%" + txtsearchproduct.Text.Trim() + "%");
                com.Parameters.AddWithValue("@DateFrom", datefrom.Value.Date);
                com.Parameters.AddWithValue("@DateTo", dateto.Value.Date.AddDays(1).AddSeconds(-1));

                if (cbCashier.Text != "All Cashier")
                {
                    com.Parameters.AddWithValue("@CashierName", cbCashier.Text);
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
                    string transDateFormatted = Convert.ToDateTime(dr["transdate"]).ToString("yyyy-MM-dd HH:mm:ss"); // Format transdate with date and time

                    dgvSalesRecord.Rows.Add(
                        a, // Row number
                        dr["transactionNo"].ToString(), // Transaction number
                        dr["cashier"].ToString(), // Cashier name
                        dr["paymentMethod"].ToString(), // Payment method
                        transDateFormatted, // Transaction date with time
                        dr["total_qty"].ToString(), // Total quantity
                        "₱" + Convert.ToDouble(dr["total_amount"]).ToString("#,##0.00") // Total amount with peso sign
                    );

                    totalSales += Convert.ToDouble(dr["total_amount"]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            // Display total sales in label
            lblTotalSales.Text = $"₱{totalSales:N2}";
        }



        public void PopulatePaymentMethods()
        {
            cbPayment.Items.Clear(); // Clear any existing items to avoid duplicates
            cbPayment.Items.Add("All Payment Methods"); // Add default option for filtering all methods
            cbPayment.Items.Add("Cash");
            cbPayment.Items.Add("GCash");

            cbPayment.SelectedIndex = 0; // Set default selection to "All Payment Methods"
        }


        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }

        private void btnPrintSales_Click(object sender, EventArgs e)
        {
            SalesReportPrint salesReport = new SalesReportPrint(this);
            salesReport.TopLevel = false;
            salesReport.ShowRecordSales();
            salesReport.StartPosition = FormStartPosition.Manual;
            salesReport.Location = new Point(
                (this.ClientSize.Width - salesReport.Width) / 2 + this.Left,
                (this.ClientSize.Height - salesReport.Height) / 2 + this.Top
            );
            this.Controls.Add(salesReport);
            salesReport.BringToFront();
            salesReport.Show(); 
        }

        private void cbCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void ShowCashier()
        {
            cbCashier.Items.Clear();
            cbCashier.Items.Add("All Cashier");

            try
            {
                con.Open();
                com = new SqlCommand(
                    @"SELECT DISTINCT u.name 
              FROM tableUserAccount u
              INNER JOIN tableSales s ON u.name = s.cashier
              WHERE u.role = 'Cashier'",
                    con
                );

                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    cbCashier.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (dr != null) dr.Close();
                if (con.State == ConnectionState.Open) con.Close();
            }
             
               cbCashier.SelectedIndex = 0;
 
        }



        private void cbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }

        private void dgvSalesRecord_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colname = dgvSalesRecord.Columns[e.ColumnIndex].Name;
                if (colname == "transNo")
                {
                    string transno = dgvSalesRecord.Rows[e.RowIndex].Cells["transNo"].Value.ToString();
                    string cashierName = dgvSalesRecord.Rows[e.RowIndex].Cells["cashierName"].Value.ToString();
                    string Payment = dgvSalesRecord.Rows[e.RowIndex].Cells["paymentmethod"].Value.ToString();
                    ShowTransac(transno, cashierName,Payment, e.RowIndex);
                }
            }
        }
        private void ShowTransac(string transno, string cashierName, string Payment,int rowIndex)
        {
            TransactionNo no = new TransactionNo(transno);
            no.StartPosition = FormStartPosition.CenterScreen; // This will center the form on the screen
            no.lblcashiername.Text = cashierName;
            no.lblpayment.Text = Payment;

            no.Show();
        }

        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSalesRecord();
        }
        private void btndispose_Click(object sender, EventArgs e)
        {
            PendingTransaction pending = new PendingTransaction();
            pending.TopLevel = false;
            pending.ShowPendingRecord();
            pending.StartPosition = FormStartPosition.Manual;
            pending.Location = new Point(
                (this.ClientSize.Width - pending.Width) / 2 + this.Left,
                (this.ClientSize.Height - pending.Height) / 2 + this.Top
            );
            this.Controls.Add(pending);
            pending.BringToFront();
            pending.Show();
        }
    }
}
