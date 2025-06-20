using AForge.Video;
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
using ZXing;
namespace PIERANGELO
{
    public partial class CreateNewProduct : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        ProductList list;
       
        public CreateNewProduct(ProductList list)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.list = list;
            this.KeyPreview = true;
        }
      

       public string GenerateProductCode(string category)
        {
            Random random = new Random();
            string firstTwoLetters = category.Length >= 2 ? category.Substring(0, 2).ToUpper() : category.ToUpper();
            string randomNumbers = random.Next(10000000, 99999999).ToString();
            return firstTwoLetters + randomNumbers;
        }

        public void Category()
        {
            cbcCategory.Items.Clear();
            con.Open();
            com = new SqlCommand("SELECT category FROM tableCategory", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                cbcCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
        public void Suppliername()
        {
            try
            {
                cbSuppliers.Items.Clear();
                con.Open();
                com = new SqlCommand("SELECT supname FROM tableSupplier", con);
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    cbSuppliers.Items.Add(dr[0].ToString());
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void Clear()
        {
            txtProductName.Clear();
            txtBarcode.Clear();
            txtsupprice.Clear();
            txtmarkupprice.Clear();
            cbSuppliers.SelectedIndex = -1;
            txtreorder.Clear();
            txtcriticallevel.Clear();
            txtsafetylevel.Clear();
            lblproductcode.Text = "";
            lblprice.Text = "";
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirm save action
                if (MessageBox.Show("Want to Save Product?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                // Validate inputs
                if (!ValidateInputs())
                    return;

                string cid = GenerateCategoryId(cbcCategory.Text);
                decimal supplierPrice = decimal.Parse(txtsupprice.Text.Trim('₱'));
                decimal markupAmount = decimal.Parse(txtmarkupprice.Text.Trim('₱'));
                decimal sellingPrice = CalculateSellingPrice(supplierPrice, markupAmount);

                // Generate product code
                lblproductcode.Text = GenerateProductCode(cbcCategory.Text);

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    // Check for duplicates
                    if (IsDuplicateBarcode(con, txtBarcode.Text))
                    {
                        MessageBox.Show("Barcode already exists.");
                        return;
                    }

                    if (IsDuplicateProductName(con, txtProductName.Text))
                    {
                        MessageBox.Show("Product name already exists.");
                        return;
                    }

                    // Insert product into database
                    InsertProduct(con, lblproductcode.Text, txtBarcode.Text, txtProductName.Text, cid, supplierPrice, markupAmount, sellingPrice);
                }

                // Notify user and clear form
                MessageBox.Show("Product has been saved.");
                Clear();
                list.ShowProductList();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsDuplicateBarcode(SqlConnection con, string barcode)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tableProductList WHERE barcode = @barcode", con))
            {
                cmd.Parameters.AddWithValue("@barcode", barcode);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private bool IsDuplicateProductName(SqlConnection con, string productName)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tableProductList WHERE productname = @productname", con))
            {
                cmd.Parameters.AddWithValue("@productname", productName);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private void InsertProduct(SqlConnection con, string pcode, string barcode, string productName, string cid, decimal supPrice, decimal markup, decimal price)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO tableProductList (pcode, barcode, productname, cid, supprice, markup, price, supplier, reorder, criticallevel, safetylevel) VALUES(@pcode, @barcode, @productname, @cid, @supprice, @markup, @price, @supplier, @reorder, @criticallevel, @safetylevel)", con))
            {
                cmd.Parameters.AddWithValue("@pcode", pcode);
                cmd.Parameters.AddWithValue("@barcode", barcode);
                cmd.Parameters.AddWithValue("@productname", productName);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@supprice", supPrice);
                cmd.Parameters.AddWithValue("@markup", markup);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@supplier", cbSuppliers.Text);
                cmd.Parameters.AddWithValue("@reorder", txtreorder.Text);
                cmd.Parameters.AddWithValue("@criticallevel", txtcriticallevel.Text);
                cmd.Parameters.AddWithValue("@safetylevel", txtsafetylevel.Text);
                cmd.ExecuteNonQuery();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                MessageBox.Show("Barcode is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cbSuppliers.Text))
            {
                MessageBox.Show("Supplier name is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cbcCategory.Text) || string.IsNullOrEmpty(GenerateCategoryId(cbcCategory.Text)))
            {
                MessageBox.Show("Invalid or missing category.");
                return false;
            }

            if (!decimal.TryParse(txtsupprice.Text.Trim('₱'), out _))
            {
                MessageBox.Show("Invalid supplier price format.");
                return false;
            }

            if (!decimal.TryParse(txtmarkupprice.Text.Trim('₱'), out _))
            {
                MessageBox.Show("Invalid markup price format.");
                return false;
            }

            return true;
        }

        private void UpdateSellingPrice(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtsupprice.Text.Trim('₱'), out decimal supprice) &&
                decimal.TryParse(txtmarkupprice.Text.Trim('₱'), out decimal markupprice))
            {
                decimal sellingPrice = CalculateSellingPrice(supprice, markupprice);
                lblprice.Text = "₱" + sellingPrice.ToString("0.00");
            }
            else
            {
                lblprice.Text = "₱0.00";
            }
        }

        public decimal CalculateSellingPrice(decimal supplierPrice, decimal markupAmount)
        {
            
            decimal sellingPrice = supplierPrice + markupAmount;
            return sellingPrice;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirm update action
                if (MessageBox.Show("Product Details Updated?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Validate inputs
                    if (string.IsNullOrWhiteSpace(txtProductName.Text))
                    {
                        MessageBox.Show("Product name is required.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtBarcode.Text))
                    {
                        MessageBox.Show("Barcode is required.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(cbSuppliers.Text))
                    {
                        MessageBox.Show("Supplier name is required.");
                        return;
                    }

                    // Ensure the existing pcode is set and not modified
                    string existingPcode = lblogpcode.Text; // Assuming the pcode is displayed on a label

                    using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                    {
                        con.Open();
                        using (SqlCommand com = new SqlCommand("UPDATE tableProductList SET barcode = @barcode, productname = @productname, cid = @cid, supprice = @supprice, markup = @markup, price = @price, supplier = @supplier, reorder = @reorder, criticallevel = @criticallevel, safetylevel = @safetylevel WHERE pcode = @pcode", con))
                        {
                            com.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                            com.Parameters.AddWithValue("@productname", txtProductName.Text);
                            com.Parameters.AddWithValue("@cid", GenerateCategoryId(cbcCategory.Text)); // Assuming this is still needed
                            com.Parameters.AddWithValue("@supprice", decimal.Parse(txtsupprice.Text.Trim('₱')));
                            com.Parameters.AddWithValue("@markup", decimal.Parse(txtmarkupprice.Text.Trim('₱')));
                            com.Parameters.AddWithValue("@price", CalculateSellingPrice(decimal.Parse(txtsupprice.Text.Trim('₱')), decimal.Parse(txtmarkupprice.Text.Trim('₱'))));
                            com.Parameters.AddWithValue("@supplier", cbSuppliers.Text);
                            com.Parameters.AddWithValue("@reorder", txtreorder.Text);
                            com.Parameters.AddWithValue("@criticallevel", txtcriticallevel.Text);
                            com.Parameters.AddWithValue("@safetylevel", txtsafetylevel.Text);
                            com.Parameters.AddWithValue("@pcode", existingPcode); // Use existing pcode for the update

                            com.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Product has been updated.");
                    Clear();
                    list.ShowProductList();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private string GenerateCategoryId(string categoryText)
        {
            string categoryId = "";

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("SELECT id FROM tableCategory WHERE category = @category", con))
                    {
                        com.Parameters.AddWithValue("@category", categoryText);
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                categoryId = reader["id"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Category not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving category ID: " + ex.Message);
            }

            return categoryId;
        }
        private void txtreorder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
            this.Close();
        }
        private void supprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void markupprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnaddsupplier_Click(object sender, EventArgs e)
        {
            CreateNewSupplier supname = new CreateNewSupplier();
            supname.ShowDialog();
        }

        private void btnaddcategory_Click(object sender, EventArgs e)
        {
            CreateNewCategory cat = new CreateNewCategory();
            cat.ShowDialog();
        }
        private void txtsupprice_TextChanged(object sender, EventArgs e)
        {
            UpdateSellingPrice(sender, e);
        }

        private void txtmarkupprice_TextChanged(object sender, EventArgs e)
        {
            UpdateSellingPrice(sender, e);
        }

        public void cbcCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbcCategory.SelectedItem != null) // Ensure an item is selected
            {
                string category = cbcCategory.SelectedItem.ToString(); // Get the selected category
                string productCode = GenerateProductCode(category); // Generate product code based on selected category
                lblproductcode.Text = productCode; // Update the label with the product code
            }
        }

        private void checkboxlevels_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxlevels.Checked)
            {
                txtcriticallevel.Enabled = true; // Enable txtcriticallevel
                txtreorder.Enabled = true;       // Enable txtreorder
                txtsafetylevel.Enabled = true;   // Enable txtsafetylevel
            }
            else
            {
                txtcriticallevel.Enabled = false; // Disable txtcriticallevel
                txtreorder.Enabled = false;       // Disable txtreorder
                txtsafetylevel.Enabled = false;   // Disable txtsafetylevel
            }
        }

        private void checkatutomaticlevel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkatutomaticlevel.Checked)
            {
                // Automatically calculate and populate the levels
                AutoCalculateLevelsFromDataGridView();
            }
            else
            {
                // Clear or reset the levels if checkbox is unchecked
                txtsafetylevel.Clear();
                txtcriticallevel.Clear();
                txtreorder.Clear();
            }

        }
        private void AutoCalculateLevelsFromDataGridView()
        {
            if (list.dgvProductList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = list.dgvProductList.SelectedRows[0];
                int currentQty = int.Parse(selectedRow.Cells[12].Value.ToString());
                int averageDailySales = GetAverageDailySalesForProduct();

                int safetyStock = averageDailySales * 5;
                int criticalLevel = safetyStock / 2;
                int reorderLevel = safetyStock + (averageDailySales * 3); 
                txtsafetylevel.Text = safetyStock.ToString();
                txtcriticallevel.Text = criticalLevel.ToString();
                txtreorder.Text = reorderLevel.ToString();
            }
            else
            {
                MessageBox.Show("Please select a product to calculate stock levels.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private int GetAverageDailySalesForProduct()
        {
            int totalSales = 0;
            int days = 0;

            // Query to calculate average daily sales from the tableSales
            string query = @"
        SELECT SUM(qty) AS TotalSales, COUNT(DISTINCT CONVERT(date, transdate)) AS SalesDays 
        FROM tableSales
        WHERE pcode = @pcode";

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@pcode", lblogpcode.Text);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalSales = reader["TotalSales"] != DBNull.Value ? Convert.ToInt32(reader["TotalSales"]) : 0;
                            days = reader["SalesDays"] != DBNull.Value ? Convert.ToInt32(reader["SalesDays"]) : 1;
                        }
                    }
                }
            }

            // Ensure that division by zero doesn't happen
            return (totalSales > 0 && days > 0) ? (totalSales / days) : 1;
        }
   
        private void txtsafetylevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { 
              e.Handled = true;
            }
       }

        private void txtcriticallevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbcCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            } 

        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateNewProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); 
                e.Handled = true; 
            }
        }
    }
}
