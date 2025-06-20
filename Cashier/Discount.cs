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
using System.Reflection;

namespace PIERANGELO
{
    public partial class Discount : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        

        public Discount(CashierModule cm)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            ShowDiscount();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void ShowDiscount()
        {
            try
            {
                con.Open();

                com = new SqlCommand("SELECT * FROM Discounts", con);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    cbdiscount.Items.Add(new { Text = dr["DiscountName"].ToString(), Value = dr["DiscountPercentage"].ToString() });
                }
                dr.Close();
                con.Close();
                cbdiscount.DisplayMember = "Text";
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
 
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbdiscount.Text))
                {
                    MessageBox.Show("Please Enter Discount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (DiscountVerification dv = new DiscountVerification(this))
                    {
                        dv.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Discount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                btnConfirm_Click(sender, e);
            }
        }

        private void cbdiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                string priceText = txtPrice.Text.Replace("₱", "").Trim();
                double discountpercent = double.Parse(((dynamic)cbdiscount.SelectedItem).Value);
                double price = double.Parse(priceText);
                double discountamount = price * discountpercent;
                txttotaldiscount.Text = discountamount.ToString("₱#,##0.00");
            }
            catch (Exception)
            {
                txttotaldiscount.Text = "₱0.00";
            }

        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
