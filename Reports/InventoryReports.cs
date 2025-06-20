using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class InventoryReports : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        public InventoryReports()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            Category();

        }
        public void Category()
        {
            cbCategory.Items.Clear();
            con.Open();
            com = new SqlCommand("SELECT category FROM tableCategory", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                cbCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void ShowInventory()
        {
            int i = 0;
            dgvInventoryList.Rows.Clear();

            // Check if connection is properly initialized
            if (con == null)
            {
                MessageBox.Show("Database connection is not initialized.");
                return;
            }

            try
            {
                // Open the connection
                con.Open();

                // SQL query
                string query = @"
            SELECT 
                p.pcode, 
                p.productname, 
                c.category, 
                r.qtyRO, 
                p.price,
                SUM(r.qtyRO * p.price) AS totalamount,
                SUM(s.qty) AS totalsold, 
                SUM(s.qty * p.price) AS totalsales,
                ISNULL(CONVERT(varchar, r.expirydate, 23), 'NA') AS expirydate,
                p.status
            FROM 
                tableProductList AS p 
                INNER JOIN tablePODetails AS r ON p.pcode = r.pcode 
                INNER JOIN tableCategory AS c ON p.cid = c.id 
                INNER JOIN tableSales AS s ON p.pcode = s.pcode
            WHERE 
                (p.pcode LIKE @pcode OR p.productname LIKE @productname) 
                AND s.transdate BETWEEN @DateFrom AND @DateTo";

                // Adding category filter if selected
                if (!string.IsNullOrEmpty(cbCategory.Text))
                {
                    query += " AND c.category = @Category";
                }

                // Grouping
                query += " GROUP BY p.pcode, p.productname, c.category, r.qtyRO, p.price, r.expirydate, p.status";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    // Parameters
                    com.Parameters.AddWithValue("@pcode", "%" + txtsearchproduct.Text + "%");
                    com.Parameters.AddWithValue("@productname", "%" + txtsearchproduct.Text + "%");
                    com.Parameters.AddWithValue("@DateFrom", datefrom.Value.Date);
                    com.Parameters.AddWithValue("@DateTo", dateto.Value.Date.AddDays(1).AddSeconds(-1));

                    if (!string.IsNullOrEmpty(cbCategory.Text))
                    {
                        com.Parameters.AddWithValue("@Category", cbCategory.Text);
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            dgvInventoryList.Rows.Add(
                                i,
                                dr["pcode"].ToString(),
                                dr["productname"].ToString(),
                                dr["category"].ToString(),
                                dr["qtyRO"].ToString(),
                                "₱" + decimal.Parse(dr["price"].ToString()).ToString("#,##0.00"),
                                "₱" + decimal.Parse(dr["totalamount"].ToString()).ToString("#,##0.00"),
                                dr["totalsold"].ToString(),
                                "₱" + decimal.Parse(dr["totalsales"].ToString()).ToString("#,##0.00"),
                                dr["expirydate"].ToString(),
                                dr["status"].ToString()
                            );
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("An error occurred while executing the SQL query: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void btnPrintSales_Click(object sender, EventArgs e)
        {
            InventoryListPrint print = new InventoryListPrint(this);
            print.TopLevel = false;
            print.Inventoryreport();
            // Center the form
            print.StartPosition = FormStartPosition.Manual;
            print.Location = new Point(
                (this.ClientSize.Width - print.Width) / 2 + this.Left,
                (this.ClientSize.Height - print.Height) / 2 + this.Top
            );

            // Ensure the form is in the correct position in the parent container
            this.Controls.Add(print);
            print.BringToFront();
            print.Show();
        }

        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            ShowInventory();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInventory();
        }

        private void cbCategory_TextChanged(object sender, EventArgs e)
        {
            ShowInventory();
        }

        private void datefrom_ValueChanged(object sender, EventArgs e)
        {
            ShowInventory();
        }

        private void dateto_ValueChanged(object sender, EventArgs e)
        {
            ShowInventory();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvInventoryList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the column is "status"
            if (dgvInventoryList.Columns[e.ColumnIndex].Name == "status")
            {
                // Define reorder, critical, and safety levels
                int reorder = 0;
                int critical = 0;
                int safety = 0;

                // Ensure row and "pcode" cell exist
                if (e.RowIndex < 0 || dgvInventoryList.Rows[e.RowIndex].Cells["pcode"].Value == null)
                    return;

                // Get the pcode and qty values
                string pcode = dgvInventoryList.Rows[e.RowIndex].Cells["pcode"].Value.ToString();
                int qty = dgvInventoryList.Rows[e.RowIndex].Cells["qty"].Value != null
                    ? Convert.ToInt32(dgvInventoryList.Rows[e.RowIndex].Cells["qty"].Value)
                    : 0;

                // Create a new connection specifically for this operation
                using (SqlConnection newCon = new SqlConnection(con.ConnectionString))
                {
                    newCon.Open();

                    // Query to retrieve reorder, critical, and safety levels
                    using (SqlCommand com = new SqlCommand(
                        "SELECT reorder, criticallevel, safetylevel FROM tableProductList WHERE pcode = @pcode", newCon))
                    {
                        com.Parameters.AddWithValue("@pcode", pcode);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                reorder = dr.GetInt32(dr.GetOrdinal("reorder"));
                                critical = dr.GetInt32(dr.GetOrdinal("criticallevel"));
                                safety = dr.GetInt32(dr.GetOrdinal("safetylevel"));
                            }
                        }
                    }
                }

                // Set the label for stock status based on quantity and thresholds
                if (qty <= 0)
                {
                    e.Value = "Out of Stock";
                }
                else if (qty <= critical)
                {
                    e.Value = "Critical Stock";
                }
                else if (qty < reorder)
                {
                    e.Value = "Low Stock";
                }
                else if (qty >= safety)
                {
                    e.Value = "Safety Stock";
                }
            }
        }
    }
}
