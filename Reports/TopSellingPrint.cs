using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class TopSellingPrint : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        TopSellingProducts ts;

        public TopSellingPrint(TopSellingProducts ts)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.ts = ts;   
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void ShowTopSelling()
        {
            try
            {
                ReportDataSource reportDataSource;

                this.rvTopSelling.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report4.rdlc";
                this.rvTopSelling.LocalReport.DataSources.Clear();

                DataSet1 data = new DataSet1();
                SqlDataAdapter sqldata = new SqlDataAdapter();

                con.Open();
                sqldata.SelectCommand = new SqlCommand("SELECT TOP 10 pcode, productname, SUM(qty) AS qty " + "FROM viewTopSelling " + "WHERE transdate BETWEEN @StartDate AND @EndDate AND STATUS = 'SOLD' " + "GROUP BY pcode, productname " + "ORDER BY qty DESC", con);
                sqldata.SelectCommand.Parameters.AddWithValue("@StartDate", ts.dt1mp.Value.Date);
                sqldata.SelectCommand.Parameters.AddWithValue("@EndDate", ts.dt1mp.Value.Date.AddDays(1).AddSeconds(-1));
               
                sqldata.Fill(data.Tables["dataTopSelling"]);
                con.Close();

                ReportParameter datefrom = new ReportParameter("datefrom", ts.dt1mp.Value.ToShortDateString());
                ReportParameter dateto = new ReportParameter("dateto", ts.dt2mp.Value.ToShortDateString());

                rvTopSelling.LocalReport.SetParameters(datefrom);
                rvTopSelling.LocalReport.SetParameters(dateto);

                reportDataSource = new ReportDataSource("DataSet1", data.Tables["dataTopSelling"]);
                rvTopSelling.LocalReport.DataSources.Add(reportDataSource);
                rvTopSelling.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                rvTopSelling.ZoomMode = ZoomMode.Percent;
                rvTopSelling.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
