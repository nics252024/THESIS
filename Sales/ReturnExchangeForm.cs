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
    public partial class ReturnExchangeForm : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        private string transNo;
        public ReturnExchangeForm(string transno)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.transNo = transno;
            ShowTransactionNO();

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void ShowTransactionNO()
        {
            int i = 0;
            dgvexchange.Rows.Clear(); 

            try
            {
                con.Open(); 
                com = new SqlCommand(@"SELECT 
                                r.id, 
                                s.id AS sale_id, 
                                s.transactionNo, 
                                s.pcode, 
                                p.productname, 
                                s.qty, 
                                s.price, 
                                s.discount,
                                s.total
                              FROM 
                                tableSales AS s 
                              INNER JOIN 
                                tablePODetails AS r ON s.pcode = r.pcode 
                              INNER JOIN 
                                tableProductList AS p ON s.pcode = p.pcode 
                              WHERE 
                                s.transactionNo = @transNo", con);
                com.Parameters.AddWithValue("@transNo", transNo);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    i++; 
                    decimal price = decimal.Parse(dr["price"].ToString());
                    decimal discount = decimal.Parse(dr["discount"].ToString());
                    decimal total = decimal.Parse(dr["total"].ToString());
                    dgvexchange.Rows.Add(
                        i, 
                        dr["id"].ToString(), 
                        dr["sale_id"].ToString(), 
                        dr["transactionNo"].ToString(),
                        dr["pcode"].ToString(), 
                        dr["productname"].ToString(), 
                        dr["qty"].ToString(), 
                        $"₱{price:#,##0.00}",  
                        $"₱{discount:#,##0.00}",
                        $"₱{total:#,##0.00}"
                    );
                }

                dr.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        private void dgvexchange_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvexchange.Columns[e.ColumnIndex].Name;
            if (colName == "cancel")
            {
                CancelOrder cd = new CancelOrder(this);
                cd.lblpo.Text = dgvexchange.Rows[e.RowIndex].Cells[1].Value.ToString();
                cd.lblId.Text = dgvexchange.Rows[e.RowIndex].Cells[2].Value.ToString();
                cd.lbltransactionID.Text = dgvexchange.Rows[e.RowIndex].Cells[3].Value.ToString();
                cd.lblpcode.Text = dgvexchange.Rows[e.RowIndex].Cells[4].Value.ToString();
                cd.lblproductname.Text = dgvexchange.Rows[e.RowIndex].Cells[5].Value.ToString();
                cd.lblprice.Text = dgvexchange.Rows[e.RowIndex].Cells[7].Value.ToString();
                cd.lblqty.Text = dgvexchange.Rows[e.RowIndex].Cells[6].Value.ToString();
                cd.lbldiscount.Text = dgvexchange.Rows[e.RowIndex].Cells[8].Value.ToString();
                cd.lbltotal.Text = dgvexchange.Rows[e.RowIndex].Cells[9].Value.ToString();
                cd.txtCancelledBy.Text = lblcashiername.Text;
                
                cd.ShowDialog();
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
