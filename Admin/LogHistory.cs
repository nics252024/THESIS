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
    public partial class LogHistory : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        
        public LogHistory()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            LoadHistoryLog();
            LoadActivityLog();
            LoadListOfTransaction();
            ShowUser();
            ShowCashier();
            PopulateCashierComboBox();
            PopulatePaymentMethodComboBox();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void LoadHistoryLog()
        {
            int i = 0;
            dgvhistorylog.Rows.Clear();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    string query = "SELECT l.Date, l.activity, l.username, l.action " +
                                   "FROM tableLogHistory AS l " +
                                   "INNER JOIN tableUserAccount AS u ON l.username = u.name " +
                                   "WHERE (l.action = 'Log In' OR l.action = 'Log Out') " +
                                   "AND l.Date BETWEEN @DateFrom AND @DateTo";

                    // Filter by user if specified
                    if (cbuser.Text != "All User" && !string.IsNullOrEmpty(cbuser.Text))
                    {
                        query += " AND u.name = @name";
                    }

                    // Add ordering based on action
                    if (cbActions.Text == "Log In")
                    {
                        query += " AND l.action = 'Log In'"; // Filter for Log In
                    }
                    else if (cbActions.Text == "Log Out")
                    {
                        query += " AND l.action = 'Log Out'"; // Filter for Log Out
                    }

                    query += " ORDER BY l.Date DESC"; // Order by date

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@DateFrom", datefrom.Value.Date);
                        com.Parameters.AddWithValue("@DateTo", dateto.Value.Date.AddDays(1).AddSeconds(-1));

                        if (cbuser.Text != "All User" && !string.IsNullOrEmpty(cbuser.Text))
                        {
                            com.Parameters.AddWithValue("@name", cbuser.Text);
                        }

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                i++;
                                dgvhistorylog.Rows.Add(i, dr["Date"].ToString(), dr["activity"].ToString(), dr["username"].ToString(), dr["action"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        public void ShowCashier()
        {
            cbuser.Items.Clear();
            cbuser.Items.Add("All User");
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("SELECT name FROM tableUserAccount", con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                cbuser.Items.Add(dr["name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void LoadActivityLog()
        {
            int i = 0;
            dgvActivity.Rows.Clear();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    string query = "SELECT a.Date, a.activity, a.username, a.action " +
                                   "FROM tableLogHistory AS a " +
                                   "INNER JOIN tableUserAccount AS u ON a.username = u.name " +
                                   "WHERE a.action = 'Void Transaction' " +
                                   "AND a.Date BETWEEN @DateFrom AND @DateTo";

                    // Check if a specific user is selected
                    if (cbactionUser.Text != "All User" && !string.IsNullOrEmpty(cbactionUser.Text))
                    {
                        query += " AND u.name = @name";
                    }

                    // No need to check action type here since it's already filtered in the WHERE clause
                    // query += " AND a.action = 'Void Transaction'"; // This is redundant

                    // Order by date descending
                    query += " ORDER BY a.Date DESC";

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        // Add date parameters
                        com.Parameters.AddWithValue("@DateFrom", dt1.Value.Date);
                        com.Parameters.AddWithValue("@DateTo", dt2.Value.Date.AddDays(1).AddSeconds(-1));

                        // Add user parameter if applicable
                        if (cbactionUser.Text != "All User" && !string.IsNullOrEmpty(cbactionUser.Text))
                        {
                            com.Parameters.AddWithValue("@name", cbactionUser.Text);
                        }

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                i++;
                                dgvActivity.Rows.Add(i, dr["Date"].ToString(), dr["activity"].ToString(), dr["username"].ToString(), dr["action"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        public void ShowUser()
        {
            cbactionUser.Items.Clear();
            cbactionUser.Items.Add("All User");
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("SELECT name FROM tableUserAccount", con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                cbactionUser.Items.Add(dr["name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void cbuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHistoryLog();
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            LoadHistoryLog();
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            LoadHistoryLog();
        }

        private void cbActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHistoryLog();
        }

        private void cbactionacty_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadActivityLog();
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadActivityLog();
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            LoadActivityLog();
        }

        private void cbactionUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadActivityLog();
        }
        public void LoadListOfTransaction()
        {
            int i = 0;
            dgvListOfTransction.Rows.Clear();

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                SqlCommand com;

                // Check if "All Cashier" is selected
                if (cbCashier.Text == "All Cashier")
                {
                    // No filtering for cashier, but filter by payment method
                    com = new SqlCommand(@"
            SELECT 
                s.transactionNo, 
                CAST(MIN(s.transdate) AS DATE) AS transdate, 
                pm.MethodName AS paymentMethod, 
                s.reference,
                MIN(s.cashier) AS cashier, 
                SUM(s.total) AS total 
            FROM 
                tableSales s
            INNER JOIN 
                PaymentMethods pm ON pm.PaymentMethodID = s.paymentMethod
            WHERE 
                s.transdate BETWEEN @DateFrom AND @DateTo 
                AND s.status = 'SOLD' 
                AND s.transactionNo LIKE @trans 
                AND pm.MethodName LIKE @paymentMethod
            GROUP BY 
                s.transactionNo, pm.MethodName, s.reference", con);

                    com.Parameters.Add("@cashier", SqlDbType.NVarChar).Value = "%";  // Wildcard for all cashiers
                }
                else
                {
                    // Filter by both cashier and payment method
                    com = new SqlCommand(@"
            SELECT 
                s.transactionNo, 
                CAST(MIN(s.transdate) AS DATE) AS transdate, 
                pm.MethodName AS paymentMethod, 
                s.reference,
                MIN(s.cashier) AS cashier, 
                SUM(s.total) AS total 
            FROM 
                tableSales s
            INNER JOIN 
                PaymentMethods pm ON pm.PaymentMethodID = s.paymentMethod
            WHERE 
                s.transdate BETWEEN @DateFrom AND @DateTo 
                AND s.status = 'SOLD' 
                AND s.transactionNo LIKE @trans 
                AND s.cashier LIKE @cashier
                AND pm.MethodName LIKE @paymentMethod
            GROUP BY 
                s.transactionNo, pm.MethodName, s.reference", con);

                    com.Parameters.Add("@cashier", SqlDbType.NVarChar).Value = "%" + cbCashier.Text.Trim() + "%";
                }

                // Add payment method filtering for both cases
                if (cbPayment.Text == "All Payment Methods")
                {
                    com.Parameters.Add("@paymentMethod", SqlDbType.NVarChar).Value = "%";  // Wildcard for all payment methods
                }
                else
                {
                    com.Parameters.Add("@paymentMethod", SqlDbType.NVarChar).Value = cbPayment.Text.Trim();  // Specific payment method
                }

                // Add the date range and transaction number filters
                com.Parameters.Add("@DateFrom", SqlDbType.Date).Value = dttrans1.Value.Date;
                com.Parameters.Add("@DateTo", SqlDbType.Date).Value = dttrans2.Value.Date;
                com.Parameters.Add("@trans", SqlDbType.NVarChar).Value = "%" + txtsearch.Text.Trim() + "%";

                // Execute the query and populate the DataGridView
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvListOfTransction.Rows.Add(
                        i,
                        dr["transactionNo"].ToString(),
                        ((DateTime)dr["transdate"]).ToString("yyyy-MM-dd"),
                        dr["paymentMethod"].ToString(),
                        dr["reference"] != DBNull.Value ? dr["reference"].ToString() : "-",  // Display reference or a placeholder
                        dr["cashier"].ToString(),
                        ((decimal)dr["total"]).ToString("#,##0.00")
                    );
                }

                dr.Close();
            }
        }

        public void PopulateCashierComboBox()
        {
            cbCashier.Items.Clear();
            cbCashier.Items.Add("All Cashier");  // Default option to show all cashiers

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                string query = "SELECT DISTINCT cashier FROM tableSales WHERE cashier IS NOT NULL AND cashier <> ''";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cbCashier.Items.Add(dr["cashier"].ToString());
                        }
                    }
                }
                con.Close();
            }

            cbCashier.SelectedIndex = 0; // Default to "All Cashier"
        }
        public void PopulatePaymentMethodComboBox()
        {
            cbPayment.Items.Clear();
            cbPayment.Items.Add("All Payment Methods");  // Default option to show all payment methods

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                string query = "SELECT DISTINCT MethodName FROM PaymentMethods";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cbPayment.Items.Add(dr["MethodName"].ToString());
                        }
                    }
                }
                con.Close();
            }

           cbPayment .SelectedIndex = 0; // Default to "All Payment Methods"
        }


        private void btnloadsoldproduct_Click(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }

        private void cbCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }

        private void cbPayment_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }
       
        public void AdjustmentHistory()
        {
            int i = 0;
            dgvAdjust.Rows.Clear();

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                try
                {
                    con.Open();

                    using (SqlCommand com = new SqlCommand("SELECT * FROM viewAdjustment WHERE transdate BETWEEN @StartDate AND @EndDate AND productname LIKE @ProductName AND reason LIKE @Reason", con))
                    {
                        com.Parameters.AddWithValue("@StartDate", dtadjust1.Value.Date);
                        com.Parameters.AddWithValue("@EndDate", dtadjust2.Value.Date);
                        com.Parameters.AddWithValue("@ProductName", txtsearchadjust.Text + "%");
                        com.Parameters.AddWithValue("@Reason", cbReason.Text + "%");

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                i++;
                                DateTime transDate = dr.IsDBNull(7) ? DateTime.MinValue : dr.GetDateTime(7);
                                string formattedDate = transDate != DateTime.MinValue ? transDate.ToString("yyyy-MM-dd") : "N/A";

                                dgvAdjust.Rows.Add(
                                    i,
                                    dr.IsDBNull(0) ? "N/A" : dr[0].ToString(),
                                    dr.IsDBNull(1) ? "N/A" : dr[1].ToString(),
                                    dr.IsDBNull(2) ? "N/A" : dr[2].ToString(),
                                    dr.IsDBNull(3) ? "N/A" : dr[3].ToString(),
                                    dr.IsDBNull(4) ? "N/A" : dr[4].ToString(),
                                    dr.IsDBNull(5) ? "N/A" : dr[5].ToString(),
                                    dr.IsDBNull(6) ? "N/A" : dr[6].ToString(),
                                    formattedDate,
                                    dr.IsDBNull(8) ? "N/A" : dr[8].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void cbReason_SelectedValueChanged(object sender, EventArgs e)
        {
            AdjustmentHistory();
        }

        private void txtsearchadjust_TextChanged(object sender, EventArgs e)
        {
            AdjustmentHistory();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtadjust1_ValueChanged_1(object sender, EventArgs e)
        {
            AdjustmentHistory();
        }

        private void dtadjust2_ValueChanged(object sender, EventArgs e)
        {
            AdjustmentHistory();
        }

        private void dttrans1_ValueChanged(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }

        private void dttrans2_ValueChanged(object sender, EventArgs e)
        {
            LoadListOfTransaction();
        }

        private void LogHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}

