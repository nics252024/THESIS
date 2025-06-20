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
using Microsoft.Reporting.WinForms;
using System.IO;

namespace PIERANGELO
{
    public partial class SalesReportPrint : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        SalesReport sales;
        
        public SalesReportPrint(SalesReport sales)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.sales = sales;
            
        }

        public void ShowRecordSales()
        {
            try
            {
                // Define the report path
                string reportPath = Path.Combine(Application.StartupPath, @"Report\Report2.rdlc");
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Report file not found.");
                    return;
                }

                // Setup ReportViewer
                rvReport.LocalReport.ReportPath = reportPath;
                rvReport.LocalReport.DataSources.Clear();

                // Prepare dataset and SQL command
                DataSet1 data = new DataSet1();
                SqlDataAdapter sqldata = new SqlDataAdapter();
                string query;
                SqlCommand command;

                con.Open();

                // Conditional query building
                if (sales.cbCashier.Text == "All Cashier" && sales.cbPayment.Text == "All Payment Methods")
                {
                    query = @"
                SELECT s.id, s.transactionNo, p.pcode, p.productname, c.MethodName, 
                       s.qty, s.discount, s.price, s.cashier, s.total 
                FROM tableSales AS s
                INNER JOIN tableProductList AS p ON s.pcode = p.pcode
                INNER JOIN PaymentMethods AS c ON s.paymentMethod = c.PaymentMethodId
                WHERE s.status = 'SOLD' AND s.transdate BETWEEN @DateFrom AND @DateTo";
                    command = new SqlCommand(query, con);
                }
                else
                {
                    query = @"
                SELECT s.id, s.transactionNo, p.pcode, p.productname, c.MethodName, 
                       s.qty, s.discount, s.price, s.cashier, s.total 
                FROM tableSales AS s
                INNER JOIN tableProductList AS p ON s.pcode = p.pcode
                INNER JOIN PaymentMethods AS c ON s.paymentMethod = c.PaymentMethodId
                WHERE s.status = 'SOLD' 
                  AND s.transdate BETWEEN @DateFrom AND @DateTo 
                  AND s.cashier = @Cashier 
                  AND c.MethodName = @MethodName";
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@Cashier", sales.cbCashier.Text);
                    command.Parameters.AddWithValue("@MethodName", sales.cbPayment.Text);
                }

                // Add common parameters
                command.Parameters.AddWithValue("@DateFrom", sales.datefrom.Value.Date);
                command.Parameters.AddWithValue("@DateTo", sales.dateto.Value.Date.AddDays(1).AddSeconds(-1));

                // Fill the dataset
                sqldata.SelectCommand = command;
                sqldata.Fill(data.Tables["dataSalesReport"]);
                con.Close();

                // Handle empty results
                if (data.Tables["dataSalesReport"].Rows.Count == 0)
                {
                    MessageBox.Show("No records found for the selected date range and cashier.");
                    return;
                }

                // Setup report parameters
                ReportParameter[] parameters = {
            new ReportParameter("cashiername", sales.cbCashier.Text),
            new ReportParameter("PaymentMethod", sales.cbPayment.Text),
            new ReportParameter("datefrom", sales.datefrom.Value.ToShortDateString()),
            new ReportParameter("dateto", sales.dateto.Value.ToShortDateString())
        };

                rvReport.LocalReport.SetParameters(parameters);
                rvReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", data.Tables["dataSalesReport"]));
                rvReport.SetDisplayMode(DisplayMode.PrintLayout);
                rvReport.ZoomMode = ZoomMode.Percent;
                rvReport.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
