using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace PIERANGELO
{
    public partial class PendingTransaction : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        public PendingTransaction()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            datefrom.Value = DateTime.Now;
            dateto.Value = DateTime.Now;
            ShowPendingRecord();
            PopulatePaymentMethods();
            ShowCashier();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
           
        }
        public void ShowPendingRecord()
        {
            int recordCount = 0;
            double totalSales = 0;

            dgvSalesRecord.Rows.Clear();

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(@"
        SELECT 
            s.transactionNo, 
            s.cashier, 
            p.MethodName AS paymentMethod, 
            s.transdate, 
            SUM(s.qty) AS total_qty, 
            SUM(s.total) AS total_amount 
        FROM 
            tableSales AS s
        LEFT JOIN 
            PaymentMethods AS p
        ON 
            s.paymentMethod = p.paymentMethodId
        WHERE 
            s.transdate BETWEEN @DateFrom AND @DateTo 
            AND LTRIM(RTRIM(LOWER(s.status))) = 'pending'
            AND s.transactionNo LIKE @TransactionNo
            " + GetAdditionalFilters() + @"
        GROUP BY 
            s.transactionNo, s.cashier, p.MethodName, s.transdate
        ORDER BY 
            s.transdate DESC", con);

                // Add parameters
                string searchValue = string.IsNullOrWhiteSpace(txtsearchproduct.Text) ? "%" : "%" + txtsearchproduct.Text.Trim() + "%";
                com.Parameters.Add(new SqlParameter("@TransactionNo", SqlDbType.VarChar) { Value = searchValue });
                com.Parameters.Add(new SqlParameter("@DateFrom", SqlDbType.DateTime) { Value = datefrom.Value.Date });
                com.Parameters.Add(new SqlParameter("@DateTo", SqlDbType.DateTime) { Value = dateto.Value.Date.AddDays(1).AddSeconds(-1) });

                if (cbCashier.Text != "All Cashier")
                {
                    com.Parameters.Add(new SqlParameter("@CashierName", SqlDbType.VarChar) { Value = cbCashier.Text });
                }
                if (cbPayment.Text != "All Payment Methods")
                {
                    com.Parameters.Add(new SqlParameter("@PaymentMethod", SqlDbType.VarChar) { Value = cbPayment.Text });
                }

                SqlDataReader dr = com.ExecuteReader();

                // Populate DataGridView
                while (dr.Read())
                {
                    recordCount++;
                    dgvSalesRecord.Rows.Add(
                        recordCount,
                        dr["transactionNo"],
                        dr["cashier"],
                        dr["paymentMethod"],
                        Convert.ToDateTime(dr["transdate"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        dr["total_qty"],
                        "₱" + Convert.ToDouble(dr["total_amount"]).ToString("#,##0.00")
                    );
                    totalSales += Convert.ToDouble(dr["total_amount"]);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving pending records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            lblTotalSales.Text = $"₱{totalSales:N2}";
        }

        private string GetAdditionalFilters()
        {
            string filters = "";

            // Add Cashier filter
            if (cbCashier.Text != "All Cashier")
            {
                filters += " AND s.cashier = @CashierName";
            }

            // Add Payment Method filter
            if (cbPayment.Text != "All Payment Methods")
            {
                filters += " AND p.MethodName = @PaymentMethod";
            }

            return filters;
        }


        public void ShowCashier()
        {
            cbCashier.Items.Clear();
            cbCashier.Items.Add("All Cashier");

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(@"
        SELECT DISTINCT u.name 
        FROM tableUserAccount u
        INNER JOIN tableSales s ON u.name = s.cashier
        WHERE u.role = 'Cashier'", con);

                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    cbCashier.Items.Add(dr["name"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving cashiers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            cbCashier.SelectedIndex = 0; // Default to "All Cashier"
        }

        public void PopulatePaymentMethods()
        {
            cbPayment.Items.Clear();
            cbPayment.Items.Add("All Payment Methods"); // Default option

            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("SELECT MethodName FROM PaymentMethods", con);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    cbPayment.Items.Add(dr["MethodName"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving payment methods: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            cbPayment.SelectedIndex = 0; // Default selection
        }


        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            ShowPendingRecord();
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            ShowPendingRecord();
        }

        private void cbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPendingRecord();
        }

        private void cbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPendingRecord();
        }

        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            ShowPendingRecord();
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
                    ShowTransac(transno, cashierName, Payment, e.RowIndex);
                }
            }
        }
        private void ShowTransac(string transno, string cashierName, string Payment, int rowIndex)
        {
            TransactionNo no = new TransactionNo(transno);
            no.StartPosition = FormStartPosition.CenterScreen; // This will center the form on the screen
            no.lblcashiername.Text = cashierName;
            no.lblpayment.Text = Payment;

            no.Show();
        }
    }

}
