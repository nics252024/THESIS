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

namespace PIERANGELO.Report
{
    public partial class POReportPrint : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        POReports pro;
        public POReportPrint(POReports pro)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.pro = pro;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void PurchaseOrder()
        {
            try
            {
                // Set the report path
                string reportPath = Path.Combine(Application.StartupPath, @"Report\Report5.rdlc");
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show("Report file not found.");
                    return;
                }

                // Set up report viewer properties
                rvReport.LocalReport.ReportPath = reportPath;
                rvReport.LocalReport.DataSources.Clear();

                // Initialize DataSet and SqlDataAdapter
                DataSet1 data = new DataSet1();

                // Open the SQL connection and execute query
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection())) // Replace with your actual connection string
                {
                    con.Open();

                    string query;

                    // Query for "All Supplier" selection
                    if (pro.cbSupplier.Text == "All Supplier")
                    {
                        query = @"
                SELECT 
                    p.PoNO, 
                    o.productname, 
                    SUM(o.qtyPO) AS qtyPO,
                    SUM(o.pricePO) AS pricePO,
                    SUM(o.amountPO) AS amountPO,
                    SUM(o.qtyRO) AS qtyRO,
                    SUM(o.priceRO) AS priceRO,
                    SUM(o.amountRO) AS amountRO,
                    o.pcode,
                    p.transdate,
                    o.status
                FROM
                    tablePurchaseOrders AS p
                INNER JOIN
                    tablePODetails AS o
                ON
                    p.PoNo = o.PoNo
                WHERE
                    p.transdate BETWEEN @datefrom AND @dateto
                GROUP BY
                    p.PoNO, p.supname, o.productname, o.pcode, p.transdate, o.status";
                    }
                    // Query for specific supplier selection
                    else
                    {
                        query = @"
                SELECT 
                    p.PoNO, 
                    o.productname, 
                    SUM(o.qtyPO) AS qtyPO,
                    SUM(o.pricePO) AS pricePO,
                    SUM(o.amountPO) AS amountPO,
                    SUM(o.qtyRO) AS qtyRO,
                    SUM(o.priceRO) AS priceRO,
                    SUM(o.amountRO) AS amountRO,
                    o.pcode,
                    p.transdate,
                    o.status
                FROM
                    tablePurchaseOrders AS p
                INNER JOIN
                    tablePODetails AS o
                ON
                    p.PoNo = o.PoNo
                WHERE
                    p.transdate BETWEEN @datefrom AND @dateto
                    AND p.supname = @supname
                GROUP BY
                    p.PoNO, p.supname, o.productname, o.pcode, p.transdate, o.status";
                    }

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@datefrom", pro.datefrom.Value.Date);
                        command.Parameters.AddWithValue("@dateto", pro.dateto.Value.Date.AddDays(1).AddSeconds(-1));

                        // Add supplier parameter only if it's not "All Supplier"
                        if (pro.cbSupplier.Text != "All Supplier")
                        {
                            command.Parameters.AddWithValue("@supname", pro.cbSupplier.Text);
                        }

                        // Execute the query and fill the dataset
                        using (SqlDataAdapter sql = new SqlDataAdapter(command))
                        {
                            sql.Fill(data.Tables["dataPODetails"]);
                        }
                    }
                }

                // Set report parameters
                ReportParameter suppliername = new ReportParameter("suppliername", pro.cbSupplier.Text);
                ReportParameter datefrom = new ReportParameter("datefrom", pro.datefrom.Value.ToShortDateString());
                ReportParameter dateto = new ReportParameter("dateto", pro.dateto.Value.ToShortDateString());

                rvReport.LocalReport.SetParameters(new ReportParameter[] { suppliername, datefrom, dateto });

                // Bind the data source to the report
                var report = new ReportDataSource("DataSet2", data.Tables["dataPODetails"]);
                rvReport.LocalReport.DataSources.Add(report);

                // Configure the report viewer display settings
                rvReport.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                rvReport.ZoomMode = ZoomMode.Percent;
                rvReport.ZoomPercent = 100;

                // Refresh the report
                rvReport.RefreshReport();
            }
            catch (Exception ex)
            {
                // Show detailed error message
                MessageBox.Show("Error: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
