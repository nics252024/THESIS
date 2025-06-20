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
    public partial class CreateDiscount : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();
        
        public CreateDiscount()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            Discount();
            this.KeyPreview = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            DiscountDetails dd = new DiscountDetails(this);
            dd.ShowDialog();
            
        }
        public void Discount()
        {
            int i = 0;
            dgvDiscount.Rows.Clear();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("SELECT * FROM Discounts", con))
                    {
                        SqlDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            i++;

                            string DiscountID = dr["DiscountID"].ToString();
                            string discountName = dr["DiscountName"].ToString();
                            string discountPercent = dr["DiscountPercentage"].ToString();

                            if (discountName == "Percent")
                            {
                                discountPercent += "%";
                            }
                            // Add the row to the DataGridView
                            dgvDiscount.Rows.Add(i, DiscountID, discountName, discountPercent);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void dgvDiscount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dgvDiscount.Columns[e.ColumnIndex].Name;
            if(name == "Edit")
            {
                DiscountDetails dc = new DiscountDetails(this);
                dc.lblDiscountId.Text = dgvDiscount.Rows[e.RowIndex].Cells[1].Value.ToString();
                dc.txtdiscountvalue.Text = dgvDiscount.Rows[e.RowIndex].Cells[3].Value.ToString();
                dc.txtdescription.Text = dgvDiscount.Rows[e.RowIndex].Cells[2].Value.ToString();
                dc.btnsavediscount.Enabled = false;
             
                Discount();
                dc.Show();
                
            }
            else if(name == "Delete")
            {
                if(MessageBox.Show("Want to delete Discounts?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    com = new SqlCommand("DELETE FROM Discounts WHERE DiscountID =  @ID", con);
                    com.Parameters.AddWithValue("@ID", dgvDiscount.Rows[e.RowIndex].Cells[1].Value.ToString());
                    com.ExecuteNonQuery();
                    con.Close();
                    Discount();
                }
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click_1(sender, e);
                e.Handled = true;
            }
        }
    }
}

