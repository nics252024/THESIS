using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class TopSellingProducts : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        public TopSellingProducts()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            Category();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Category()
        {
            cbcategory.Items.Clear();
            cbcategory.Items.Add("ALL CATEGORY");
            con.Open(); 
            com = new SqlCommand("SELECT category FROM tableCategory", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                cbcategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void ShowTopProducts()
        {
            int i = 0;
            dgvMovingProducts.Rows.Clear();

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                // Define the base query for fetching top-selling products
                string query = @"
            SELECT s.pcode, p.productname, c.category, 
                   ISNULL(SUM(s.qty), 0) AS qty, 
                   ISNULL(SUM(s.total), 0) AS total 
            FROM tableProductList AS p 
            INNER JOIN tableSales AS s ON p.pcode = s.pcode 
            INNER JOIN tableCategory AS c ON c.id = p.cid
            WHERE s.transdate BETWEEN @StartDate AND @EndDate 
            AND s.STATUS = 'SOLD' "; // Added table alias for STATUS to avoid confusion

                // Check if a specific category is selected and modify the query accordingly
                if (cbcategory.Text != "ALL CATEGORY" && !string.IsNullOrEmpty(cbcategory.Text))
                {
                    query += "AND c.category = @Category "; // Specify the table for category
                }

                // Group by the necessary fields
                query += "GROUP BY s.pcode, p.productname, c.category ";

                // Determine the order based on the selected option
                switch (cbTopSelling.Text)
                {
                    case "Quantity":
                        query += "ORDER BY qty DESC";
                        break;
                    case "Total Amount":
                        query += "ORDER BY total DESC";
                        break;
                    default:
                        query += "ORDER BY c.category DESC"; // Added table alias for clarity
                        break;
                }

                con.Open();

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    // Add the date parameters
                    com.Parameters.AddWithValue("@StartDate", dt1mp.Value.Date);
                    com.Parameters.AddWithValue("@EndDate", dt2mp.Value.Date.AddDays(1).AddSeconds(-1));

                    // Add category parameter if needed
                    if (cbcategory.Text != "ALL CATEGORY" && !string.IsNullOrEmpty(cbcategory.Text))
                    {
                        com.Parameters.AddWithValue("@Category", cbcategory.Text);
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        // Populate the DataGridView with results
                        while (dr.Read())
                        {
                            i++;
                            dgvMovingProducts.Rows.Add(
                                i,
                                dr["pcode"].ToString(),
                                dr["productname"].ToString(),
                                dr["category"].ToString(),
                                dr["qty"].ToString(),
                                "₱" + decimal.Parse(dr["total"].ToString()).ToString("#,##0.00") // Changed to decimal for precision
                            );
                        }
                    }
                }
            }
        }


        private void btnfilter_Click(object sender, EventArgs e)
        {
            if(cbTopSelling.Text == String.Empty)
            {
                MessageBox.Show("Select Sort by", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ShowTopProducts();
            //LoadTopSellingChart();
        }

        private void linkprint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TopSellingPrint tsp = new TopSellingPrint(this);
            tsp.ShowTopSelling();
            tsp.ShowDialog();
        }

        private void cbTopSelling_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void TopSellingProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnfilter_Click(sender, e);
            }
        }

        private void cbTopSelling_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTopProducts();
        }

        private void cbcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTopProducts();
        }

        private void dt1mp_ValueChanged(object sender, EventArgs e)
        {
            ShowTopProducts();
        }

        private void dt2mp_ValueChanged(object sender, EventArgs e)
        {
            ShowTopProducts();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbcategory_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowTopProducts();
        }

        private void cbTopSelling_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowTopProducts();
        }

        private void cbcategory_TextChanged(object sender, EventArgs e)
        {
            ShowTopProducts();
        }
    }
}
