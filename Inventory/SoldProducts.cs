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
    public partial class SoldProducts : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        public SoldProducts()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void ShowSoldProduct()
        {
            try
            {
                dgvSoldProducts.Rows.Clear();
                int i = 0;
                con.Open();

                com = new SqlCommand(
                    "SELECT s.pcode, p.productname, s.price, SUM(s.qty) AS total_quantity, SUM(s.discount) AS _discount, SUM(s.total) AS total " +
                    "FROM tableSales AS s " +
                    "INNER JOIN tableProductList AS p ON s.pcode = p.pcode " +
                    "WHERE s.status = 'Sold' AND s.transdate BETWEEN @StartDate AND @EndDate " +
                    "AND (s.pcode LIKE @pcode OR p.productname LIKE @productname) " +
                    "GROUP BY s.pcode, p.productname, s.price", con);
                com.Parameters.AddWithValue("@pcode", "%" + txtsearchsoldproducts.Text + "%");
                com.Parameters.AddWithValue("@productname", "%" + txtsearchsoldproducts.Text + "%");
                com.Parameters.AddWithValue("@StartDate", dt1sp.Value.Date);
                com.Parameters.AddWithValue("@EndDate", dt2sp.Value.Date);
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    i++;
                    dgvSoldProducts.Rows.Add(i, dr["pcode"].ToString(), dr["productname"].ToString(),
                                             "₱" + double.Parse(dr["price"].ToString()).ToString("#,##0.00"),
                                             dr["total_quantity"].ToString(),
                                             "₱" + double.Parse(dr["_discount"].ToString()).ToString("#,##0.00"),
                                             "₱" + double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                }
                dr.Close();
                con.Close();

                con.Open();
                com = new SqlCommand(
                    "SELECT ISNULL(SUM(total), 0) FROM tableSales WHERE status = 'Sold' AND transdate BETWEEN @StartDate AND @EndDate", con);
                com.Parameters.AddWithValue("@StartDate", dt1sp.Value.Date);
                com.Parameters.AddWithValue("@EndDate", dt2sp.Value.Date);
                object totalSales = com.ExecuteScalar();
                lblTotal.Text = "₱" + Convert.ToDouble(totalSales).ToString(" #,##0.00");
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnloadsoldproduct_Click(object sender, EventArgs e)
        {
            ShowSoldProduct();
        }

        private void txtsearchsoldproducts_TextChanged(object sender, EventArgs e)
        {
            ShowSoldProduct();
        }

        private void dt1sp_ValueChanged(object sender, EventArgs e)
        {
            ShowSoldProduct();
        }

        private void dt2sp_ValueChanged(object sender, EventArgs e)
        {
            ShowSoldProduct();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
