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
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Interfaces;

namespace PIERANGELO
{
    public partial class InventoryListPrint : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;

        InventoryReports invent;
        public InventoryListPrint(InventoryReports invent)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.invent = invent;
        }

        private void InventoryListPrint_Load(object sender, EventArgs e)
        {

            this.rvInventory.RefreshReport();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Inventoryreport()
        {
            ReportDataSource rDS;
            try
            {
                this.rvInventory.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report3.rdlc";
                this.rvInventory.LocalReport.DataSources.Clear();

                DataSet1 data = new DataSet1();
                SqlDataAdapter sql = new SqlDataAdapter();

                con.Open();

                // Base SQL query
                string query = "SELECT p.pcode, p.productname, c.category, p.price, SUM(s.qty) as totalsold, SUM(s.total) as totalamount " +
                               "FROM tableProductList as p " +
                               "INNER JOIN tableSales as s ON p.pcode = s.pcode " +
                               "INNER JOIN tableCategory as c ON p.cid = c.id " +
                               "WHERE s.status = 'SOLD' " +
                               "AND transdate BETWEEN @DateFrom AND @DateTo ";

                // Check if category filter is applied
                if (!string.IsNullOrEmpty(invent.cbCategory.Text))
                {
                    query += "AND c.category = @category ";
                }

                query += "GROUP BY p.pcode, p.productname, c.category, p.price " +
                         "ORDER BY c.category"; // Sorting by category

                sql.SelectCommand = new SqlCommand(query, con);

                // Parameters
                sql.SelectCommand.Parameters.AddWithValue("@DateFrom", invent.datefrom.Value.Date);
                sql.SelectCommand.Parameters.AddWithValue("@DateTo", invent.dateto.Value.Date.AddDays(1).AddSeconds(-1));

                // Add category parameter only if it's selected
                if (!string.IsNullOrEmpty(invent.cbCategory.Text))
                {
                    sql.SelectCommand.Parameters.AddWithValue("@category", invent.cbCategory.Text);
                }

                sql.Fill(data.Tables["dataInventory"]);
                con.Close();

                rDS = new ReportDataSource("DataSet1", data.Tables["dataInventory"]);
                rvInventory.LocalReport.DataSources.Add(rDS);
                rvInventory.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                rvInventory.ZoomMode = ZoomMode.Percent;
                rvInventory.ZoomPercent = 100;
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
