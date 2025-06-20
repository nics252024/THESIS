using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace PIERANGELO
{
    public partial class PrintReceiptGcash : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        CashierModule cm;
        public PrintReceiptGcash(CashierModule cm)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.cm = cm;
            this.KeyPreview = true;
        }

        private void PrintReceiptGcash_Load(object sender, EventArgs e)
        {

            this.rvReceiptGcash.RefreshReport();
        }
        public void PrintReceipt(string cash, string reference)
        {
            try
            {
                // Configure the report path
                this.rvReceiptGcash.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report6.rdlc";
                this.rvReceiptGcash.LocalReport.DataSources.Clear(); 

                // Fill the dataset
                DataSet1 dataSet1 = new DataSet1();
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    sqlDataAdapter.SelectCommand = new SqlCommand(
                        @"SELECT s.id, s.transactionNO, s.pcode, s.price, s.qty, s.discount, s.total, 
                         s.transdate, s.status, s.reference, p.productname 
                      FROM tableSales AS s
                      INNER JOIN tableProductList AS p ON p.pcode = s.pcode 
                      WHERE transactionNO LIKE @TransactionNo", con);

                    // Ensure the parameter is properly passed
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TransactionNo", "%" + cm.lbltransaction.Text + "%");

                    // Fill the dataset with the result
                    sqlDataAdapter.Fill(dataSet1.Tables["dataSalesGcash"]);
                }

                // Create report parameters
                var reportParameters = new[]
                {
            new ReportParameter("discount", cm.lbldiscount.Text),
            new ReportParameter("total", cm.lblsubtotal.Text),
            new ReportParameter("cash", cash),
            new ReportParameter("transactionno", cm.lbltransaction.Text),
            new ReportParameter("cashiername",cm.lblaccoutname.Text),
            new ReportParameter("reference", reference)
        };

                // Set the parameters in the report
                rvReceiptGcash.LocalReport.SetParameters(reportParameters);

                // Set the data source
                ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dataSet1.Tables["dataSalesGcash"]);
                rvReceiptGcash.LocalReport.DataSources.Add(reportDataSource);

                // Refresh the report viewer
                rvReceiptGcash.RefreshReport();
            }
            catch (Exception ex)
            {
                // Show error message
                MessageBox.Show("An error occurred while printing the receipt: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
