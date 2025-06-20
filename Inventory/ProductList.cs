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
using System.Net.NetworkInformation;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using ZXing.QrCode.Internal;
using System.Reflection;
using System.Collections;
using AForge.Video;
using AForge.Video.DirectShow;
using PIERANGELO.Inventory;

namespace PIERANGELO
{
    public partial class ProductList : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;

        private Form currentForm;
        public string SelectedProductName { get; set; }

        public ProductList()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            Category();
        }
        private void ChangeForm(Form newForm)
        {
            if(currentForm != null)
            {
                currentForm.Dispose();
            }
            currentForm = newForm;
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;
            panelcontent.Controls.Add(newForm);
            newForm.BringToFront();
            newForm.Show();
        }


        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        public void ShowProductList()
        {
            try
            {
                int i = 0;
                dgvProductList.Rows.Clear(); // Clear the existing rows in the DataGridView

                con.Open();

                string query = @"
              SELECT 
                  p.pcode, p.barcode, p.productname, c.category,p.supprice, p.markup, p.price,  p.supplier, p.reorder, 
                  p.criticallevel, p.safetylevel, ISNULL(SUM(s.qtyRO), 0) AS totalqty, 
                  p.status  FROM 
                  tableProductList AS p FULL OUTER JOIN tablePODetails AS s ON p.pcode = s.pcode 
              INNER JOIN tableCategory AS c ON c.id = p.cid WHERE 
                  (p.pcode LIKE @pcode OR 
                  p.productname LIKE @productname OR 
                  p.barcode LIKE @barcode) 
                  AND (s.expiryDate IS NULL OR s.expiryDate > GETDATE())"; // Add this line to filter out expired products

                if (!string.IsNullOrEmpty(cbCategory.Text))
                {
                    query += "AND c.category = @Category ";
                }

                query += "GROUP BY p.pcode, p.barcode, p.productname, c.category, p.supprice, p.markup, p.price, p.supplier, p.reorder, p.criticallevel, p.safetylevel,  p.status ";
                query += "ORDER BY c.category DESC";

                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@pcode", "%" + txtsearchproduct.Text + "%");
                    com.Parameters.AddWithValue("@productname", "%" + txtsearchproduct.Text + "%");
                    com.Parameters.AddWithValue("@barcode", "%" + txtsearchproduct.Text + "%");

                    if (!string.IsNullOrEmpty(cbCategory.Text))
                    {
                        com.Parameters.AddWithValue("@Category", cbCategory.Text);
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            dgvProductList.Rows.Add(
                                i,
                                dr["pcode"].ToString(),
                                dr["barcode"].ToString(),
                                dr["productname"].ToString(),
                                dr["category"].ToString(),
                                "₱" + decimal.Parse(dr["supprice"].ToString()).ToString("N2"),
                                "₱" + decimal.Parse(dr["markup"].ToString()).ToString("N2"),
                                "₱" + decimal.Parse(dr["price"].ToString()).ToString("N2"),
                                dr["supplier"].ToString(),
                                dr["reorder"].ToString(),
                                dr["criticallevel"].ToString(),
                                dr["safetylevel"].ToString(),
                                dr["totalqty"].ToString(), // Correct field name for total quantity
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
                con.Close();
            }
        }


        private void txtsearchproduct_TextChanged(object sender, EventArgs e)
        {
            ShowProductList();
        }
        private void ProductList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender,e);
            }
        }


        private void btncreatenew_Click(object sender, EventArgs e)
        {
            CreateNewProduct cnp = new CreateNewProduct(this);
            cnp.btnsave.Enabled = true;
            cnp.btnUpdate.Enabled = false; 
            cnp.txtsupprice.Text = "₱0.00";
            cnp.lblprice.Text = "₱0.00";  
            cnp.txtmarkupprice.Text = "₱0.00";
            cnp.lblogpcode.Visible = false;
            cnp.txtcriticallevel.Enabled = false;
            cnp.txtreorder.Enabled = false;
            cnp.txtsafetylevel.Enabled = false;
            cnp.Category();  
            cnp.Suppliername();       
            cnp.ShowDialog();


        }

        private void dgvProductList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colname = dgvProductList.Columns[e.ColumnIndex].Name;

                if (colname == "productcode")
                {
                    // Retrieve product code
                    string prcode = dgvProductList.Rows[e.RowIndex].Cells["productcode"]?.Value?.ToString();

                    // Pass the product code and row index to ShowExpiryDate
                    ShowExpiryDate(prcode, e.RowIndex);
                }
            }
        }
        private void ShowExpiryDate(string prcode, int rowIndex)
        {

            ExpiryDate expiry = new ExpiryDate(prcode);
            System.Drawing.Point dgvPosition = dgvProductList.PointToScreen(System.Drawing.Point.Empty);
            Rectangle rowRectangle = dgvProductList.GetRowDisplayRectangle(rowIndex, true);
            int rightOffset = 131;
            int formX = dgvPosition.X + rowRectangle.X + rightOffset;
            int formY = dgvPosition.Y + rowRectangle.Y + rowRectangle.Height;
            expiry.StartPosition = FormStartPosition.Manual;
            expiry.Location = new System.Drawing.Point(formX, formY);
            expiry.Show();
        }

        private void dgvProductList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProductList.Columns[e.ColumnIndex].Name == "status")
            {
                int reorder, qty, critical, safety;

                // Try parsing the values from the cells
                if (!int.TryParse(dgvProductList.Rows[e.RowIndex].Cells["reorder"].Value?.ToString(), out reorder) ||
                    !int.TryParse(dgvProductList.Rows[e.RowIndex].Cells["qty"].Value?.ToString(), out qty) ||
                    !int.TryParse(dgvProductList.Rows[e.RowIndex].Cells["critical"].Value?.ToString(), out critical) ||
                    !int.TryParse(dgvProductList.Rows[e.RowIndex].Cells["safety"].Value?.ToString(), out safety))
                {
                    // If parsing fails, exit early
                    return;
                }

                // Check for the stock status, with correct ordering of conditions
                if (qty <= 0)
                {
                    // Check for "Out of Stock"
                    e.CellStyle.BackColor = Color.Red;
                    e.Value = "Out of Stock";
                }
                else if (qty <= critical)
                {
                    // Check for "Critical Stock"
                    e.CellStyle.BackColor = Color.BurlyWood;
                    e.Value = "Critical Stock";
                }
                else if (qty <= reorder) // Change to <= to include cases where qty == reorder
                {
                    // Check for "Low Stock"
                    e.CellStyle.BackColor = Color.Salmon;
                    e.Value = "Low Stock";
                }
                else if (qty >= safety)
                {
                    // Check for "Safety Stock"
                    e.CellStyle.BackColor = Color.Green;
                    e.Value = "Safety Stock";
                }
            }
        }

        private void dgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.RowIndex < 0) return;

            string columnName = dgvProductList.Columns[e.ColumnIndex].Name;

            if (columnName == "Edit")
            {
                CreateNewProduct pro = new CreateNewProduct(this);
                pro.Category();
                pro.Suppliername();

                // Populate fields from the selected row
                pro.lblogpcode.Text = dgvProductList.Rows[e.RowIndex].Cells[1].Value.ToString();
                pro.lblproductcode.Text = dgvProductList.Rows[e.RowIndex].Cells[1].Value.ToString();
                pro.txtBarcode.Text = dgvProductList.Rows[e.RowIndex].Cells[2].Value?.ToString();
                pro.txtProductName.Text = dgvProductList.Rows[e.RowIndex].Cells[3].Value?.ToString();
                pro.cbcCategory.Text = dgvProductList.Rows[e.RowIndex].Cells[4].Value?.ToString();
                pro.txtsupprice.Text = dgvProductList.Rows[e.RowIndex].Cells[5].Value?.ToString();
                pro.txtmarkupprice.Text = dgvProductList.Rows[e.RowIndex].Cells[6].Value?.ToString();
                pro.lblprice.Text = dgvProductList.Rows[e.RowIndex].Cells[7].Value?.ToString();
                pro.cbSuppliers.Text = dgvProductList.Rows[e.RowIndex].Cells[8].Value?.ToString();
                pro.txtreorder.Text = dgvProductList.Rows[e.RowIndex].Cells[9].Value?.ToString();
                pro.txtcriticallevel.Text = dgvProductList.Rows[e.RowIndex].Cells[10].Value?.ToString();
                pro.txtsafetylevel.Text = dgvProductList.Rows[e.RowIndex].Cells[11].Value?.ToString();
                pro.btnsave.Enabled = false;
                pro.btnUpdate.Enabled = true;
                pro.txtcriticallevel.Enabled = false;
                pro.txtreorder.Enabled = false;
                pro.txtsafetylevel.Enabled = false;
                pro.Show();

            }

            else if (columnName == "Delete")
            {
                // Confirmation dialog
                if (MessageBox.Show("You want to delete this product? Confirm this action.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bool isDeleted = false;

                    try
                    {
                        using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                        {
                            con.Open();

                            string pcode = dgvProductList.Rows[e.RowIndex].Cells[1].Value?.ToString();

                            // Check quantity
                            using (SqlCommand checkQtyCommand = new SqlCommand("SELECT qty FROM tableProductList WHERE pcode = @pcode", con))
                            {
                                checkQtyCommand.Parameters.AddWithValue("@pcode", pcode);
                                int qty = Convert.ToInt32(checkQtyCommand.ExecuteScalar());

                                if (qty > 0)
                                {
                                    MessageBox.Show("Unable to delete because there is quantity associated with this product.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    // Get product details
                                    using (SqlCommand getDetailsCommand = new SqlCommand(
                                        "SELECT p.barcode, p.productname, c.category, p.supprice, p.markup, p.price, p.supplier, p.reorder " +
                                        "FROM tableProductList p " +
                                        "INNER JOIN tableCategory c ON p.cid = c.id " +
                                        "WHERE p.pcode = @pcode", con))
                                    {
                                        getDetailsCommand.Parameters.AddWithValue("@pcode", pcode);
                                        using (SqlDataReader reader = getDetailsCommand.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                // Insert into tableDeletedProduct
                                                using (SqlCommand insertDeletedCommand = new SqlCommand(
                                                    "INSERT INTO tableDeletedItems (pcode, barcode, productName, category, supPrice, markupPrice, price, supplier, reorder) " +
                                                    "VALUES (@pcode, @barcode, @productName, @category, @supPrice, @markupPrice, @price, @supplier, @reorder)", con))
                                                {
                                                    insertDeletedCommand.Parameters.AddWithValue("@pcode", pcode);
                                                    insertDeletedCommand.Parameters.AddWithValue("@barcode", reader["barcode"]);
                                                    insertDeletedCommand.Parameters.AddWithValue("@productName", reader["productName"]);
                                                    insertDeletedCommand.Parameters.AddWithValue("@category", reader["category"]);
                                                    insertDeletedCommand.Parameters.AddWithValue("@supPrice", reader["supprice"]);
                                                    insertDeletedCommand.Parameters.AddWithValue("@markupPrice", reader["markup"]);
                                                    insertDeletedCommand.Parameters.AddWithValue("@price", reader["price"]);
                                                    insertDeletedCommand.Parameters.AddWithValue("@supplier", reader["supplier"]);
                                                    insertDeletedCommand.Parameters.AddWithValue("@reorder", reader["reorder"]);

                                                    // Close the reader before executing the insert command
                                                    reader.Close();

                                                    insertDeletedCommand.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                    }

                                    // Delete from tableProductList
                                    using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM tableProductList WHERE pcode = @pcode", con))
                                    {
                                        deleteCommand.Parameters.AddWithValue("@pcode", pcode);
                                        deleteCommand.ExecuteNonQuery();
                                    }

                                    isDeleted = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (isDeleted)
                    {
                        ShowProductList();
                    }
                }
            }
        }
     

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowProductList();
        }

        private void cbCategory_TextChanged(object sender, EventArgs e)
        {
            ShowProductList();
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            ShowProductList();

            // Highlight the selected product
            if (!string.IsNullOrEmpty(SelectedProductName))
            {
                HighlightProduct(SelectedProductName);
            }
        }
        private void HighlightProduct(string productName)
        {
            // Assuming you have a DataGridView named dgvProductList
            foreach (DataGridViewRow row in dgvProductList.Rows)
            {
                if (row.Cells["proname"].Value.ToString().Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    dgvProductList.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            ShowProductList();
        }
        private void btndispose_Click(object sender, EventArgs e)
        {
            DisposeProducts dp = new DisposeProducts();
            dp.LoadDisposeProduct();
            ChangeForm(dp);
        }

        private void buttonBack_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
