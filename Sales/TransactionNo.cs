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
    public partial class TransactionNo : Form
    {
        SqlConnection con = new SqlConnection();
       
        DatabaseConnection dbcon = new DatabaseConnection();
        private string transNo;
        public TransactionNo(string transno)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            transNo = transno;
            ShowTransactionNO();
        }
        public void ShowTransactionNO()
        {
            int i = 0;
            dataGridViewProductList.Rows.Clear();

            try
            {
                con.Open();
                string query = "SELECT s.id, s.transactionNo, s.pcode, p.productname, s.qty, s.price, s.discount " +
                               "FROM tableSales as s " +
                               "INNER JOIN tableProductList as p ON s.pcode = p.pcode " +
                               "WHERE s.transactionNo = @transNo";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@transNo", transNo);
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            dataGridViewProductList.Rows.Add(
                                i,
                                dr["id"].ToString(),
                                dr["transactionNo"].ToString(),
                                dr["pcode"].ToString(),
                                dr["productname"].ToString(),
                                dr["qty"].ToString(),
                                "₱" + decimal.Parse(dr["price"].ToString()).ToString("#,##0.00"),
                                "₱" + double.Parse(dr["discount"].ToString()).ToString("#,##0.00")
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
