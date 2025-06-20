using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;


namespace PIERANGELO
{
    public partial class ReceiptPrint : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        CashierModule cm;
        
        public ReceiptPrint(CashierModule cm)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.cm = cm;
            this.KeyPreview = true;
        }

        private void Receipt_Load(object sender, EventArgs e)
        {

            this.rvReceipt.RefreshReport();
        }

        public void PrintReceipt(string Cash, string Change)
        {
            try
            {
                // Configure the report
                this.rvReceipt.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report1.rdlc";
                this.rvReceipt.LocalReport.DataSources.Clear();

                // Fill the dataset
                DataSet1 dataSet1 = new DataSet1();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                con.Open();
                sqlDataAdapter.SelectCommand = new SqlCommand("SELECT s.id, s.transactionNO, s.pcode, s.price, s.qty, s.discount, s.total, s.transdate, s.status, p.productname FROM tableSales AS s INNER JOIN tableProductList AS p ON p.pcode = s.pcode WHERE transactionNO LIKE @TransactionNo", con);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@TransactionNo", "%" + cm.lbltransaction.Text + "%");
                sqlDataAdapter.Fill(dataSet1.Tables["dataSales"]);
                con.Close();

                // Set report parameters
                ReportParameter discount = new ReportParameter("discount", cm.lbldiscount.Text);
                ReportParameter total = new ReportParameter("total", cm.lblsubtotal.Text);
                ReportParameter cash = new ReportParameter("cash", Cash);
                ReportParameter change = new ReportParameter("change", Change);
                ReportParameter transactiono = new ReportParameter("transactiono", cm.lbltransaction.Text);
                ReportParameter cashiername = new ReportParameter("cashiername", cm.lblaccoutname.Text);
                ReportParameter vat = new ReportParameter("vat", cm.lblVAT.Text);
                ReportParameter vatable = new ReportParameter("vatable", cm.lblVatable.Text);



                // Set the parameters in the report
                rvReceipt.LocalReport.SetParameters(new ReportParameter[] {
                discount, total, cash, change, transactiono, cashiername, vat, vatable
        });

                // Set the data source
                ReportDataSource rp = new ReportDataSource("DataSet1", dataSet1.Tables["dataSales"]);
                rvReceipt.LocalReport.DataSources.Add(rp);

                //// Print the report
                //PrintReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //private void PrintReport()
        //{
        //    try
        //    {
        //        PrinterSettings printerSettings = new PrinterSettings();
        //        PageSettings pageSettings = new PageSettings(printerSettings)
        //        {
        //            Margins = new Margins(5, 5, 5, 5) // Adjust margins as necessary
        //        };

        //        // Create a print document for the report
        //        ReportPrintDocument printDocument = new ReportPrintDocument(rvReceipt.LocalReport)
        //        {
        //            PrinterSettings = printerSettings,
        //            DefaultPageSettings = pageSettings
        //        };

               
        //        printDocument.Print();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error printing receipt: " + ex.Message);
        //    }
        //}


        private void ReceiptPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
