using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PIERANGELO.Inventory
{
    public partial class DisposeProducts : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        
        public DisposeProducts()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            LoadDisposeProduct();
            LoadCategories();
            this.KeyPreview = true;
        }
        private void LoadCategories()
        {
            try
            {
                cbCategory.Items.Clear(); // Clear existing items in the ComboBox
               

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    // Query to select distinct categories based on category ID (cid)
                    string query = @"SELECT DISTINCT c.category 
                             FROM tableDisposal p
                             INNER JOIN tableCategory c ON p.cid = c.id"; // Join with tableCategory to get the category name

                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Add each category name to the ComboBox
                                cbCategory.Items.Add(dr["category"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadDisposeProduct()
        {
            try
            {
                int i = 0;
                dgvDisposedProducts.Rows.Clear();

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    // Corrected SQL query
                    string query = @"
            SELECT 
                d.pcode, 
                d.productname, 
                c.category, 
                d.qty, 
                d.expirydate, 
                d.disposaldate 
            FROM 
                tableDisposal AS d 
            INNER JOIN 
                tableCategory AS c ON c.id = d.cid 
            WHERE 
                d.productname LIKE @productname
                AND c.category LIKE @category";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use proper parameters
                        cmd.Parameters.AddWithValue("@productname", "%" + txtsearchproduct.Text + "%");
                        cmd.Parameters.AddWithValue("@category", "%" + cbCategory.Text + "%");

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                i++;

                                // Handle expiry and disposal date string conversion
                                string expiryDate = "";
                                if (DateTime.TryParse(dr["expirydate"].ToString(), out DateTime parsedExpiryDate))
                                {
                                    expiryDate = parsedExpiryDate.ToString("yyyy-MM-dd");
                                }

                                string disposalDate = "";
                                if (DateTime.TryParse(dr["disposaldate"].ToString(), out DateTime parsedDisposalDate))
                                {
                                    disposalDate = parsedDisposalDate.ToString("yyyy-MM-dd");
                                }

                                // Add data to DataGridView
                                dgvDisposedProducts.Rows.Add(i,
                                    dr["pcode"].ToString(),
                                    dr["productname"].ToString(),
                                    dr["category"].ToString(),
                                    dr["qty"].ToString(),
                                    expiryDate,
                                    disposalDate);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            LoadDisposeProduct();
        }
        private void cbCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadDisposeProduct();
        }
        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisposeProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}
