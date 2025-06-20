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

namespace PIERANGELO
{
    public partial class DiscountDetails : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        CreateDiscount create;
        public DiscountDetails(CreateDiscount create)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.create = create;
            this.KeyPreview = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnsavediscount_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Want to Save this Discount?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(txtdiscountvalue.Text))
                    {
                        MessageBox.Show("Please Enter the Discount Value.");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtdescription.Text))
                    {
                        MessageBox.Show("Please Enter Discount Name Value.");
                        return;
                    }

                    using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                    {
                        con.Open();

                        // Check if the discount already exists
                        using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Discounts WHERE DiscountName = @DiscountName OR DiscountPercentage = @DiscountPercentage", con))
                        {
                            checkCmd.Parameters.AddWithValue("@DiscountName", txtdescription.Text);
                            checkCmd.Parameters.AddWithValue("@DiscountPercentage", txtdiscountvalue.Text);

                            int exists = (int)checkCmd.ExecuteScalar();

                            if (exists > 0)
                            {
                                MessageBox.Show("A discount with the same name or percentage already exists.");
                                return;
                            }
                        }

                        // If no existing discount, insert the new one
                        using (SqlCommand com = new SqlCommand("INSERT INTO Discounts (DiscountName, DiscountPercentage) VALUES (@DiscountName, @DiscountPercentage)", con))
                        {
                            com.Parameters.AddWithValue("@DiscountName", txtdescription.Text);
                            com.Parameters.AddWithValue("@DiscountPercentage", txtdiscountvalue.Text);

                            com.ExecuteNonQuery();
                            MessageBox.Show("Discount has been Saved.");
                            Clear();
                            create.Discount();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {

            txtdiscountvalue.Clear();
            txtdiscountvalue.Focus();
            txtdescription.Clear();
            txtdescription.Focus();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtdiscountvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '%')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '%' && txtdiscountvalue.Text.Contains("%"))
            {
                e.Handled = true;
            }
        }

        private void DiscountDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsavediscount_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Want to Update Discount?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Validation
                    if (string.IsNullOrEmpty(txtdiscountvalue.Text))
                    {
                        MessageBox.Show("Please enter the discount value.");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtdescription.Text))
                    {
                        MessageBox.Show("Please enter the discount description.");
                        return;
                    }
                    if (string.IsNullOrEmpty(lblDiscountId.Text))
                    {
                        MessageBox.Show("No discount ID selected. Please select a discount to update.");
                        return;
                    }

                    // Prepare data
                    string description = txtdescription.Text.Trim();
                    string discountvalue = txtdiscountvalue.Text.Trim().Replace("%", "").Replace("₱", "");
                    string discountId = lblDiscountId.Text.Trim();

                    // Validate numeric discount value
                    if (!decimal.TryParse(discountvalue, out decimal parsedDiscountValue))
                    {
                        MessageBox.Show("Please enter a valid numeric discount value.");
                        return;
                    }

                    // Database connection and query
                    using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                    {
                        con.Open();
                        string query = "UPDATE Discounts SET DiscountName = @DiscountName, DiscountPercentage = @DiscountPercentage WHERE DiscountID = @DiscountID";

                        using (SqlCommand com = new SqlCommand(query, con))
                        {
                            // Add parameters to the query
                            com.Parameters.AddWithValue("@DiscountName", description);
                            com.Parameters.AddWithValue("@DiscountPercentage", parsedDiscountValue);
                            com.Parameters.AddWithValue("@DiscountID", discountId);

                            int rowsAffected = com.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Discount updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Discount update failed. Please check the discount ID.");
                            }
                        }
                    }

                    // Clear fields and refresh the discount list
                    Clear();
                    create.Discount();
                    this.Dispose();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }
    }
 }
