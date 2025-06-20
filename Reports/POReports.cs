using PIERANGELO.Report;
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
    public partial class POReports : Form
    {
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public POReports()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            PopulateSuppliers();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void PopulateSuppliers()
        {
            try
            {
                cbSupplier.Items.Clear();
                cbSupplier.Items.Add("All Supplier");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT DISTINCT supname FROM tableSupplier ORDER BY supname";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cbSupplier.Items.Add(dr["supname"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public void PurchaseOrder()
        {
            try
            {
                int i = 0;
                dgvPO.Rows.Clear();

                // Open the connection if it is not open
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Base query with GROUP BY PoNo
                string query = "SELECT PoNo, transdate, supname, numItems, " +
                               "totalamountPo, delivereddate, totalamountRo, status " +
                               "FROM tablePurchaseOrders " +
                               "WHERE transdate BETWEEN @datefrom AND @dateto " +  // Added space here
                               "AND PoNo LIKE @PoNo ";

                // Modify query based on supplier selection
                if (!string.IsNullOrEmpty(cbSupplier.Text) && cbSupplier.Text != "All Supplier")
                {
                    query += " AND supname LIKE @supname";
                }

                // Create SQL command
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    // Set parameters
                    com.Parameters.AddWithValue("@datefrom", datefrom.Value.Date);  // Only the date part is needed
                    com.Parameters.AddWithValue("@dateto", dateto.Value.Date.AddDays(1).AddSeconds(-1));  // Ensure full-day coverage
                    com.Parameters.AddWithValue("@PoNo", "%" + txtsearchproduct.Text + "%");  // Search filter for PoNo

                    // Add supplier parameter only if it's specified
                    if (!string.IsNullOrEmpty(cbSupplier.Text) && cbSupplier.Text != "All Supplier")
                    {
                        com.Parameters.AddWithValue("@supname", "%" + cbSupplier.Text + "%");  // Search filter for supplier
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;

                            // Parse total amounts with DBNull check
                            decimal totalAmountPO = dr["totalamountPo"] != DBNull.Value ? Convert.ToDecimal(dr["totalamountPo"]) : 0;
                            decimal totalAmountRO = dr["totalamountRo"] != DBNull.Value ? Convert.ToDecimal(dr["totalamountRo"]) : 0;

                            // Format transaction date
                            DateTime transactionDate;
                            string formattedTransactionDate = dr["transdate"] != DBNull.Value && DateTime.TryParse(dr["transdate"].ToString(), out transactionDate)
                                ? transactionDate.ToString("yyyy-MM-dd")
                                : string.Empty;

                            // Format delivered date
                            string formattedDeliveredDate = dr["delivereddate"] != DBNull.Value && DateTime.TryParse(dr["delivereddate"].ToString(), out transactionDate)
                                ? transactionDate.ToString("yyyy-MM-dd")
                                : string.Empty;

                            // Add row to DataGridView
                            dgvPO.Rows.Add(
                                i,
                                dr["PoNo"].ToString(),
                                formattedTransactionDate,
                                dr["supname"].ToString(),
                                dr["numItems"].ToString(),
                                "₱" + totalAmountPO.ToString("N2"),  // Format with peso sign
                                formattedDeliveredDate,
                                "₱" + totalAmountRO.ToString("N2"),  // Format with peso sign
                                dr["status"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }




        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            PurchaseOrder();
        }

        private void cbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            PurchaseOrder();
        }

        private void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            PurchaseOrder();
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            PurchaseOrder();
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            PurchaseOrder();
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string cname = dgvPO.Columns[e.ColumnIndex].Name;

                if (cname.Equals("pono", StringComparison.OrdinalIgnoreCase)) // Ensure case-insensitive comparison
                {
                    RODetails ro = new RODetails();
                    ro.lblPoNo.Text = dgvPO.Rows[e.RowIndex].Cells["pono"].Value.ToString();
                    ro.label3.Visible = false;
                    ro.dptrans.Visible = false;
                    ro.label10.Visible = false;
                    ro.lbltotalamountolRO.Visible = false;
                    ro.btnROreversal.Visible = false;
                    ro.btnSaveRODetails.Visible = false;
                    ro.dgvRO.Columns["partialselect"].Visible = false;
                    ro.dgvRO.Columns["allselect"].Visible = false;
                    // Optionally, call the ReceiveOrder method
                    ro.ReceiveOrder();

                    // Show the dialog
                    ro.ShowDialog();
                }
            }
        }

        private void btnPrintSales_Click(object sender, EventArgs e)
        {
            POReportPrint reportPrint = new POReportPrint(this);
            reportPrint.PurchaseOrder();
            reportPrint.ShowDialog();

        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
