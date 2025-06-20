using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace PIERANGELO
{
    public partial class DashBoard : Form
    {
        SqlConnection con;
        DatabaseConnection dbcon = new DatabaseConnection();
       

        public DashBoard()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            con.ConnectionString = dbcon.DataConnection();

            ShowTopCategory();
            ShowExpiry();
            ShowChartSales();
            PopulateMonths();
            PopulateYears();
            PopulateCatMonths();
            cbcatmonths.SelectedIndex = DateTime.Now.Month - 1;
            cbmonths.SelectedIndex = DateTime.Now.Month - 1; 
            cbyear.SelectedItem = DateTime.Now.Year;
            

        }
        private void PopulateMonths()
        {
            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
            foreach (string month in monthNames)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    cbmonths.Items.Add(month);
                }
            }
        }
        private void PopulateCatMonths()
        {
            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
            foreach (string month in monthNames)
            {
                if (!string.IsNullOrEmpty(month))
                {
                   cbcatmonths.Items.Add(month);
                }
            }
        }

        private void PopulateYears()
        {
            int startYear = 2020; // Starting year
            int endYear = DateTime.Now.Year; // Current year
            for (int year = startYear; year <= endYear; year++)
            {
                cbyear.Items.Add(year);
            }
        }

        public void ShowChartSales()
        {
            try
            {
                con.Open();

                int selectedMonth = cbmonths.SelectedIndex + 1;
                int selectedYear = (int)cbyear.SelectedItem;

                SqlDataAdapter sqlData = new SqlDataAdapter(
                    "SELECT DAY(transdate) AS day, ISNULL(SUM(total), 0.0) AS total " +
                    "FROM tableSales " +
                    "WHERE MONTH(transdate) = @selectedMonth AND YEAR(transdate) = @selectedYear AND status = 'Sold' " +
                    "GROUP BY DAY(transdate)", con);

                sqlData.SelectCommand.Parameters.AddWithValue("@selectedMonth", selectedMonth);
                sqlData.SelectCommand.Parameters.AddWithValue("@selectedYear", selectedYear);

                DataSet dataSet = new DataSet();
                sqlData.Fill(dataSet, "Sales");

                chart1.Series.Clear();

                // Line series for daily sales trend
                Series lineSeries = new Series("Sales Trend");
                lineSeries.ChartType = SeriesChartType.Spline;
                lineSeries.BorderWidth = 2;
                lineSeries.Color = Color.Navy;

                // Bar series for daily sales amount
                Series barSeries = new Series("Daily Sales");
                barSeries.ChartType = SeriesChartType.Column;
                barSeries.Color = Color.LightSlateGray;

                double totalSalesThisMonth = 0.0;

                foreach (DataRow row in dataSet.Tables["Sales"].Rows)
                {
                    if (int.TryParse(row["day"].ToString(), out int day) &&
                        double.TryParse(row["total"].ToString(), out double total))
                    {
                        lineSeries.Points.AddXY(day, total);  
                        barSeries.Points.AddXY(day, total);
                        totalSalesThisMonth += total;
                    }
                }

                chart1.Series.Add(barSeries);  
                chart1.Series.Add(lineSeries); 

                chart1.ChartAreas[0].AxisX.Title = "Day";
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;  

                chart1.ChartAreas[0].AxisY.Title = "Sales (₱)";
                chart1.ChartAreas[0].AxisY.LabelStyle.Format = "C"; 
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false; 

                chart1.Titles.Clear();
                Title chartTitle = chart1.Titles.Add($"Sales for {cbmonths.SelectedItem} {selectedYear}");
                chartTitle.Font = new Font("Arial", 10, FontStyle.Bold); 

                lblsalesthismonth.Text = "₱" + totalSalesThisMonth.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void LogError(string message)
        {
            System.IO.File.AppendAllText("error_log.txt", $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
        public void ShowTopCategory()
        {
            try
            {
                int selectMonth = cbcatmonths.SelectedIndex + 1;

                con.Open();
                SqlDataAdapter sql = new SqlDataAdapter(
                    $"SELECT TOP 5 category, ISNULL(SUM(qty), 0) AS qty " +
                    $"FROM viewTopSelling " +
                    $"WHERE status = 'Sold' " +
                    $"AND MONTH(transdate) = @selectMonth " +
                    $"GROUP BY category " +
                    $"ORDER BY qty DESC",
                    con);

                sql.SelectCommand.Parameters.AddWithValue("@selectMonth", selectMonth);

                DataSet dataSet = new DataSet();
                sql.Fill(dataSet, "TopSelling");

                chartcategories.DataSource = dataSet.Tables["TopSelling"];
                Series series = chartcategories.Series[0];
                series.ChartType = SeriesChartType.Bar;
                series.Name = "Top 5 Selling";

                chartcategories.Series[series.Name].XValueMember = "category";
                chartcategories.Series[series.Name].YValueMembers = "qty";
                chartcategories.Series[series.Name].IsValueShownAsLabel = true;

                chartcategories.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartcategories.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                chartcategories.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chartcategories.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

                chartcategories.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                chartcategories.ChartAreas[0].AxisX.MinorTickMark.Enabled = false;
                chartcategories.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
                chartcategories.ChartAreas[0].AxisY.MinorTickMark.Enabled = false;

                chartcategories.ChartAreas[0].AxisX.LineColor = Color.Transparent;
                chartcategories.ChartAreas[0].AxisY.LineColor = Color.Transparent;

                chartcategories.Titles.Clear();
                chartcategories.Titles.Add($"Top 5 Selling Categories this {cbcatmonths.SelectedItem}");
                chartcategories.Titles[0].Font = new Font("Arial", 10, FontStyle.Bold);

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void DashBoard_Resize(object sender, EventArgs e)
         {
            panelcontent.Left = (this.Width - panelcontent.Right) / 2;
        }    
        private decimal GetDailySalesAmount()
        {
            decimal dailySales = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(total) FROM tableSales WHERE CONVERT(date, transdate) = CONVERT(date, GETDATE())", con);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    dailySales = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving daily sales: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return dailySales;
        }

        private void paneldailysales_Click(object sender, EventArgs e)
        {
            decimal dailySalesAmount = GetDailySalesAmount(); 

            SalesReport sales = new SalesReport
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                DailySalesAmount = dailySalesAmount 
            };

            panelcontrol.Controls.Add(sales);
            sales.BringToFront();
            sales.ShowSalesRecord(); 
            sales.Show();
        }
        public void ShowExpiry()
        {
            int i = 0;
            dgvExpired.Rows.Clear();

            using (SqlConnection conRead = new SqlConnection(dbcon.DataConnection()))
            {
                conRead.Open();

                // Correct the SQL query
                using (SqlCommand com = new SqlCommand(
                    "SELECT p.pcode, p.productname, s.expirydate, s.qtyRO, p.cid " +
                    "FROM tablePODetails AS s " +
                    "JOIN tableProductList AS p ON p.pcode = s.pcode " +
                    "WHERE s.expirydate <= DATEADD(day, 30, GETDATE())", conRead))
                {
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string expirydatestring = "";
                            if (dr["expirydate"] != DBNull.Value)
                            {
                                DateTime expiryDate = Convert.ToDateTime(dr["expirydate"]);
                                expirydatestring = expiryDate.ToString("yyyy-MM-dd");

                                DateTime threeWeeksBeforeExpiry = expiryDate.AddDays(-21);

                                if (DateTime.Now.Date >= threeWeeksBeforeExpiry.Date && DateTime.Now.Date < expiryDate.Date)
                                {
                                    i++;
                                    dgvExpired.Rows.Add(i, dr["pcode"].ToString(), dr["productname"].ToString(), expirydatestring);
                                }
                                else if (expiryDate.Date <= DateTime.Now.Date)
                                {
                                    using (SqlConnection conInsert = new SqlConnection(dbcon.DataConnection()))
                                    {
                                        conInsert.Open();

                                        using (SqlCommand insertCom = new SqlCommand(
                                            "INSERT INTO tableDisposal (productname, pcode, cid, expirydate, qty, disposaldate) " +
                                            "VALUES (@productname, @pcode, @cid, @expirydate, @qty, @disposaldate)", conInsert))
                                        {
                                            insertCom.Parameters.AddWithValue("@productname", dr["productname"].ToString());
                                            insertCom.Parameters.AddWithValue("@pcode", dr["pcode"].ToString());
                                            insertCom.Parameters.AddWithValue("@cid", dr["cid"].ToString());
                                            insertCom.Parameters.AddWithValue("@expirydate", expiryDate);
                                            insertCom.Parameters.AddWithValue("@qty", dr["qtyRO"]);
                                            insertCom.Parameters.AddWithValue("@disposaldate", DateTime.Now); 
                                            insertCom.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ShowTopSelling()
        {
            int i = 0;
            dgvtopSelling.Rows.Clear();
            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();

                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                string query = @"SELECT TOP 5 pcode, productname, ISNULL(SUM(qty), 0) as qty, 
                          price, ISNULL(SUM(total), 0) as total 
                   FROM viewTopSelling 
                   WHERE status = 'Sold' 
                     AND CAST(transdate AS DATE) BETWEEN @FirstDayOfMonth AND @LastDayOfMonth 
                   GROUP BY pcode, productname, price
                   ORDER BY qty DESC"; 

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@FirstDayOfMonth", firstDayOfMonth);
                    com.Parameters.AddWithValue("@LastDayOfMonth", lastDayOfMonth);

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            dgvtopSelling.Rows.Add(i,
                                                    dr["productname"].ToString(),
                                                    dr["qty"].ToString(),
                                                    Convert.ToDecimal(dr["price"]).ToString("₱0.00"),
                                                    Convert.ToDecimal(dr["total"]).ToString("₱0.00"));
                        }
                    }
                }
            }
        }

        private void panelexpiry_Click(object sender, EventArgs e)
        {
            ProductList list = new ProductList();
            list.ShowProductList();
            list.ShowDialog();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            ProductList list = new ProductList();
            list.ShowProductList();
            list.ShowDialog();
        }
        private void charttopselling_Click(object sender, EventArgs e)
        {
            TopSellingProducts selling = new TopSellingProducts();
            selling.TopLevel = false;
            selling.FormBorderStyle = FormBorderStyle.None;
            panelcontrol.Controls.Add(selling);
            selling.BringToFront(); 
            selling.Show();
        }
        private void panel6_Click(object sender, EventArgs e)
        {
            ReturnExchangeAdmin cp = new ReturnExchangeAdmin();
            cp.TopLevel = false;
            cp.FormBorderStyle = FormBorderStyle.None;
            cp.Dock = DockStyle.Fill;
            panelcontrol.Controls.Add(cp);
            cp.BringToFront();
            cp.ShowReturnExchange();
            cp.Show();
        }
        private decimal GetTotalSalesAmount()
        {
            decimal totalSales = 0;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(total) FROM tableSales", con);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    totalSales = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving total sales: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return totalSales;
        }

        private void paneltotalsales_Click_1(object sender, EventArgs e)
        {
            decimal totalSalesAmount = GetTotalSalesAmount();
            SalesReport sales = new SalesReport
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                TotalSalesAmount = totalSalesAmount 
            };

            panelcontrol.Controls.Add(sales);
            sales.BringToFront();
            sales.ShowSalesRecord();
            sales.Show();
        }

        private void cbyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChartSales();
        }

        private void cbmonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChartSales();
        }

        private void dgvExpired_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                
                string pcode = dgvExpired.Rows[e.RowIndex].Cells["pcode"].Value.ToString();

                ExpiryDate expiryForm = new ExpiryDate(pcode);
                expiryForm.Show();
            }
        }

        private void cbcatmonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTopCategory();
           
        }
    }
}
