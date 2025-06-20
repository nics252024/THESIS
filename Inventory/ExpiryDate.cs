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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ZXing;
using PIERANGELO.Inventory;

namespace PIERANGELO
{

    public partial class ExpiryDate : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        private string procode;
        

       
        public ExpiryDate(string prcode)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            procode = prcode;
            ShowExpiry(procode);
            this.KeyPreview = true;
        }
        public void ShowExpiry(string procode)
        {
            dgvExpiry.Rows.Clear(); // Clear existing rows from the DataGridView

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                try
                {
                    con.Open();

                    // SQL query to select product details from the stock and product list tables
                    string sqlQuery = @"
    SELECT 
        r.id, 
        r.pcode, 
        p.productname, 
        c.category, 
        p.supprice, 
        p.markup, 
        p.price, 
        p.criticallevel, 
        p.reorder, 
        p.safetylevel,
        r.qtyRO,            -- Quantity from tableProductList
        r.expirydate,     -- Expiry date from tablePODetails
        p.status
    FROM 
        tablePODetails AS r
    INNER JOIN 
        tableProductList AS p ON r.pcode = p.pcode
    LEFT JOIN 
        tableCategory AS c ON c.id = p.cid
    WHERE 
        r.pcode = @pcode";

                    using (SqlCommand com = new SqlCommand(sqlQuery, con))
                    {
                        // Ensure procode is initialized before calling ShowExpiry
                        com.Parameters.AddWithValue("@pcode", procode);

                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            int i = 0; // Row counter
                            while (dr.Read())
                            {
                                i++;

                                // Handle null values for expiry date, prices, and quantity
                                string expiryDate = dr["expirydate"] != DBNull.Value
                                    ? Convert.ToDateTime(dr["expirydate"]).ToString("yyyy-MM-dd")
                                    : "No Expiry Date";

                                string supprice = dr["supprice"] != DBNull.Value
                                    ? $"₱{Convert.ToDecimal(dr["supprice"]):N2}"
                                    : "₱0.00";

                                string markup = dr["markup"] != DBNull.Value
                                    ? $"₱{Convert.ToDecimal(dr["markup"]):N2}"
                                    : "₱0.00";

                                string price = dr["price"] != DBNull.Value
                                    ? $"₱{Convert.ToDecimal(dr["price"]):N2}"
                                    : "₱0.00";

                                string qty = dr["qtyRO"] != DBNull.Value
                                    ? dr["qtyRO"].ToString()
                                    : "0";

                                string status = dr["status"] != DBNull.Value
                                    ? dr["status"].ToString()
                                    : string.Empty;

                                // Add the row to the DataGridView
                                dgvExpiry.Rows.Add(i,
                                                   dr["id"].ToString(),
                                                   dr["pcode"].ToString(),
                                                   dr["productname"].ToString(),
                                                   supprice,
                                                   markup,
                                                   price,
                                                   qty,
                                                   expiryDate,
                                                   dr["category"].ToString(),
                                                   dr["reorder"].ToString(),
                                                   dr["safetylevel"].ToString(),
                                                   dr["criticallevel"].ToString(),
                                                   status);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching product details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
        }




        private void dataGridViewProductList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvExpiry.Columns["status"] != null && e.ColumnIndex == dgvExpiry.Columns["status"].Index)
            {
                DateTime expiryDate;
                bool isDate = DateTime.TryParse(dgvExpiry.Rows[e.RowIndex].Cells["expdate"].Value?.ToString(), out expiryDate);

                // Check if the product is expiring today or already expired
                if (isDate && expiryDate.Date <= DateTime.Now.Date)
                {
                    // Set the status to "Expired" and color the row accordingly
                    e.Value = "Expired";
                    foreach (DataGridViewCell cell in dgvExpiry.Rows[e.RowIndex].Cells)
                    {
                        cell.Style.BackColor = Color.LightCoral;
                    }
                }
                else
                {
                    // Only check stock levels if the product is not expired
                    int reorder, qty, critical, safety;

                    // Try parsing the values from the cells
                    if (!int.TryParse(dgvExpiry.Rows[e.RowIndex].Cells["reorderlevel"].Value?.ToString(), out reorder) ||
                        !int.TryParse(dgvExpiry.Rows[e.RowIndex].Cells["qty"].Value?.ToString(), out qty) ||
                        !int.TryParse(dgvExpiry.Rows[e.RowIndex].Cells["criticallevel"].Value?.ToString(), out critical) ||
                        !int.TryParse(dgvExpiry.Rows[e.RowIndex].Cells["safetylevel"].Value?.ToString(), out safety))
                    {
                        return;
                    }

                    // Check stock levels
                    if (qty <= 0)
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.Value = "Out of Stock";
                    }
                    else if (qty <= critical)
                    {
                        e.CellStyle.BackColor = Color.BurlyWood;
                        e.Value = "Critical Stock";
                    }
                    else if (qty <= reorder)  // Change to <= to include cases where qty == reorder
                    {
                        e.CellStyle.BackColor = Color.Salmon;
                        e.Value = "Low Stock";
                    }
                    else if (qty >= safety)
                    {
                        e.CellStyle.BackColor = Color.Green;
                        e.Value = "Safety Stock";
                    }
                }
            }
        }

        private void ExpiryDate_Load(object sender, EventArgs e)
        {
            dgvExpiry.CellFormatting += dataGridViewProductList_CellFormatting;
        }

        private void dataGridViewProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return; // Ensure the row index is valid

            string colname = dgvExpiry.Columns[e.ColumnIndex].Name;

            // Handle Edit functionality
            if (colname == "edit")
            {
                EditPrice price = new EditPrice();
                price.lblID.Text = dgvExpiry.Rows[e.RowIndex].Cells["id"].Value.ToString();
                price.txtsupprice.Text = dgvExpiry.Rows[e.RowIndex].Cells["supprice"].Value.ToString();
                price.txtmarkup.Text = dgvExpiry.Rows[e.RowIndex].Cells["markup"].Value.ToString();
                price.txtsellingprice.Text = dgvExpiry.Rows[e.RowIndex].Cells["price"].Value.ToString();
                price.lblexpiry.Text = dgvExpiry.Rows[e.RowIndex].Cells["expdate"].Value?.ToString();
                price.ShowDialog();
            }
            // Handle Delete functionality
            else if (colname == "Delete")
            {
                // Get the instance of the ProductList form
                DisposeProducts pd = Application.OpenForms.OfType<DisposeProducts>().FirstOrDefault();

                if (MessageBox.Show("Dispose Product?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                    {
                        con.Open();
                        using (SqlTransaction transaction = con.BeginTransaction())
                        {
                            try
                            {
                                int id = Convert.ToInt32(dgvExpiry.Rows[e.RowIndex].Cells["id"].Value);
                                string pcode = dgvExpiry.Rows[e.RowIndex].Cells["productcode"].Value.ToString();
                                string productname = dgvExpiry.Rows[e.RowIndex].Cells["prodname"].Value.ToString();

                                // Check for expiry date and handle null
                                DateTime? expirydate = null;
                                if (dgvExpiry.Rows[e.RowIndex].Cells["expdate"].Value != null &&
                                    DateTime.TryParse(dgvExpiry.Rows[e.RowIndex].Cells["expdate"].Value.ToString(), out DateTime parsedDate))
                                {
                                    expirydate = parsedDate.Date;
                                }

                                int qty = Convert.ToInt32(dgvExpiry.Rows[e.RowIndex].Cells["qty"].Value);

                                // Retrieve category ID (cid) from the database
                                string category = dgvExpiry.Rows[e.RowIndex].Cells["category"].Value.ToString();
                                int cid = 0;

                                using (SqlCommand categoryCommand = new SqlCommand("SELECT id FROM tableCategory WHERE category = @category", con, transaction))
                                {
                                    categoryCommand.Parameters.AddWithValue("@category", category);
                                    object result = categoryCommand.ExecuteScalar();

                                    if (result != null)
                                    {
                                        cid = Convert.ToInt32(result); // Convert category name to cid
                                    }
                                    else
                                    {
                                        throw new Exception("Category ID not found.");
                                    }
                                }

                                // Delete from tablePODetails
                                using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM tablePODetails WHERE id = @id", con, transaction))
                                {
                                    deleteCommand.Parameters.AddWithValue("@id", id);
                                    deleteCommand.ExecuteNonQuery();
                                }

                                // Insert into tableDisposal
                                using (SqlCommand sql = new SqlCommand("INSERT INTO tableDisposal(productname, pcode, cid, expirydate, qty, disposaldate) VALUES (@productname, @pcode, @cid, @expirydate, @qty, @disposaldate)", con, transaction))
                                {
                                    sql.Parameters.AddWithValue("@productname", productname);
                                    sql.Parameters.AddWithValue("@pcode", pcode);
                                    sql.Parameters.AddWithValue("@cid", cid);
                                    sql.Parameters.AddWithValue("@expirydate", (object)expirydate ?? DBNull.Value); // Handle null expiry date
                                    sql.Parameters.AddWithValue("@qty", qty);
                                    sql.Parameters.AddWithValue("@disposaldate", DateTime.Now.Date); // Use Date property to store only the date
                                    sql.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageBox.Show("Product Successfully Disposed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ShowExpiry(procode); // Refresh the DataGridView

                                // Call LoadDisposedProduct only if the instance exists
                                if (pd != null)
                                {
                                    pd.LoadDisposeProduct();
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExpiryDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}

