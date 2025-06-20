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

namespace PIERANGELO.Inventory
{
    public partial class EditPrice : Form
    {
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlConnection con = new SqlConnection();
        public EditPrice()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse the new prices from the textboxes, removing the currency symbol if necessary
                decimal newSupPrice = decimal.Parse(txtsupprice.Text.Replace("₱", "").Trim());
                decimal newMarkup = decimal.Parse(txtmarkup.Text.Replace("₱", "").Trim());
                decimal newSellingPrice = decimal.Parse(txtsellingprice.Text.Replace("₱", "").Trim());
                string id = lblID.Text;

                // Call the method to update the prices by expiry date
                UpdatePricesByExpiryDate(newSupPrice, newMarkup, newSellingPrice, id);

                MessageBox.Show("Prices updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers for prices.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdatePricesByExpiryDate(decimal newSupPrice, decimal newMarkup, decimal newSellingPrice, string id)
        {
            // Your SQL query to update the prices based on the expiry date
            string query = @"
            UPDATE dbo.tableProductList
            SET 
            supprice = @newSupPrice,
            markup = @newMarkup,
            price = @newSellingPrice
            FROM dbo.tableProductList 
            INNER JOIN dbo.tableStock ON dbo.tableProductList.pcode = dbo.tableStock.pcode
            WHERE dbo.tableStock.id = @id";

            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@newSupPrice", newSupPrice);
                    cmd.Parameters.AddWithValue("@newMarkup", newMarkup);
                    cmd.Parameters.AddWithValue("@newSellingPrice", newSellingPrice);
                    cmd.Parameters.AddWithValue("@id", id); // Pass the id variable here

                    // Execute the update command
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{rowsAffected} product(s) updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No products were found with the specified ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("An SQL error occurred: " + sqlEx.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ComputeSellingPrice()
        {
            decimal supPrice;
            decimal markup;

            // Try to parse the values; if parsing fails, default to 0
            bool isSupPriceValid = decimal.TryParse(txtsupprice.Text.Replace("₱", "").Trim(), out supPrice);
            bool isMarkupValid = decimal.TryParse(txtmarkup.Text.Replace("₱", "").Trim(), out markup);

            if (isSupPriceValid && isMarkupValid)
            {
                // Calculate the selling price
                decimal sellingPrice = supPrice + markup;

                // Update the selling price textbox with formatted value
                txtsellingprice.Text = $"₱{sellingPrice:N2}"; // Format as currency
            }
            else
            {
                txtsellingprice.Text = "₱0.00"; // Default to 0 if input is invalid
            }
        }

        private void txtsupprice_TextChanged(object sender, EventArgs e)
        {
            ComputeSellingPrice();
        }

        private void txtmarkup_TextChanged(object sender, EventArgs e)
        {
            ComputeSellingPrice();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
