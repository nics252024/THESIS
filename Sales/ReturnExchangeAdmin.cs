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
    public partial class ReturnExchangeAdmin : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        public ReturnExchangeAdmin()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            ShowActionType();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void ShowActionType()
        {
            
            cbActionType.Items.Clear();

            cbActionType.Items.Add("All");
            cbActionType.Items.Add("Refund");
            cbActionType.Items.Add("Exchange");
        }
        public void ShowReturnExchange()
        {
            int i = 0;
            dgvCancelled.Rows.Clear();

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(@"
            SELECT c.transactionNo, c.pcode, p.productname, c.price, c.qty, 
                   c.total, CONVERT(date, c.transdate) AS transdate, 
                   c.voidby, c.cancelledby, c.reason, c.ActionType 
            FROM tableCancel AS c 
            INNER JOIN tableProductList AS p ON c.pcode = p.pcode 
            WHERE c.transdate BETWEEN @dt1 AND @dt2", con))
                {
                    // Add date parameters
                    com.Parameters.AddWithValue("@dt1", dt1cancel.Value.Date);
                    com.Parameters.AddWithValue("@dt2", dt2cancel.Value.Date.AddDays(1).AddSeconds(-1));

                    // Prepare to filter by action type
                    if (cbActionType.SelectedItem != null && cbActionType.SelectedItem.ToString() != "All")
                    {
                        com.CommandText += " AND c.ActionType = @ActionType";
                        com.Parameters.AddWithValue("@ActionType", cbActionType.SelectedItem.ToString());
                    }

                    // Prepare to filter by search text
                    if (!string.IsNullOrEmpty(txtsearchproduct.Text))
                    {
                        com.CommandText += " AND (c.transactionNo LIKE @searchTerm OR p.productname LIKE @searchTerm)";
                        com.Parameters.AddWithValue("@searchTerm", "%" + txtsearchproduct.Text + "%");
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            dgvCancelled.Rows.Add(
                                i,
                                dr["transactionNo"].ToString(),
                                dr["pcode"].ToString(),
                                dr["productname"].ToString(),
                                "₱" + decimal.Parse(dr["price"].ToString()).ToString("#,##0.00"),
                                dr["qty"].ToString(),
                                "₱" + decimal.Parse(dr["total"].ToString()).ToString("#,##0.00"),
                                Convert.ToDateTime(dr["transdate"]).ToString("yyyy-MM-dd"), // Format the date
                                dr["voidby"].ToString(),
                                dr["cancelledby"].ToString(),
                                dr["reason"].ToString(),
                                dr["ActionType"].ToString()
                            );
                        }
                    }
                }
            }
        }

        private void dt1cancel_ValueChanged(object sender, EventArgs e)
        {
           ShowReturnExchange();
        }

        private void dt2cancel_ValueChanged(object sender, EventArgs e)
        {
            ShowReturnExchange();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbActionType_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowReturnExchange();
        }

        private void cbActionType_TextChanged(object sender, EventArgs e)
        {
            ShowReturnExchange();
        }

        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            ShowReturnExchange();
        }
    }
}
