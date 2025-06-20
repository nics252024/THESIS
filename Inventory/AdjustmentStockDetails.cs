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
    public partial class AdjustmentStockDetails : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;

        StockLevelsList list;

        public AdjustmentStockDetails(StockLevelsList list)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.list = list;
            this.KeyPreview = true;



        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void btnconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the quantity input
                if (!int.TryParse(txtqty.Text, out int qty) || qty <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity greater than zero for adjustment.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    // Retrieve the category ID based on the category name
                    int categoryId;
                    using (SqlCommand getCategoryCmd = new SqlCommand("SELECT id FROM tableCategory WHERE category = @category", con))
                    {
                        getCategoryCmd.Parameters.AddWithValue("@category", lblcategory.Text);
                        var result = getCategoryCmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Category not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        categoryId = Convert.ToInt32(result);
                    }

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        // Stock adjustments (ADD/REMOVE)
                        if (cbaction.Text == "REMOVE")
                        {
                            cmd.CommandText = "UPDATE tablePODetails SET qtyRO = qtyRO - @qtyRO WHERE id = @id";
                            cmd.Parameters.AddWithValue("@qtyRO", qty);
                            cmd.Parameters.AddWithValue("@id", lblID.Text);
                            cmd.ExecuteNonQuery();
                        }
                        else if (cbaction.Text == "ADD")
                        {
                            cmd.CommandText = "UPDATE tablePODetails SET qtyRO = qtyRO + @qtyRO WHERE id = @id";
                            cmd.Parameters.AddWithValue("@qtyRO", qty);
                            cmd.Parameters.AddWithValue("@id", lblID.Text);
                            cmd.ExecuteNonQuery();
                        }

                        // Insert into tableDisposal if the reason is "Expired"
                        if (cbReason.Text == "Expired")
                        {
                            cmd.CommandText = "INSERT INTO tableDisposal(productname, pcode, cid, expirydate, qty, disposaldate) " +
                                              "VALUES (@productname, @pcode, @cid, @expirydate, @qty, @disposaldate)";

                            cmd.Parameters.Clear();  // Clear parameters for the new query

                            // Parse expiry date
                            DateTime expiryDate;
                            if (DateTime.TryParse(lblExpirydate.Text, out expiryDate))
                            {
                                cmd.Parameters.AddWithValue("@productname", lblproductname.Text);
                                cmd.Parameters.AddWithValue("@pcode", lblproductcode.Text);
                                cmd.Parameters.AddWithValue("@cid", categoryId);  // Use the retrieved category ID
                                cmd.Parameters.AddWithValue("@expirydate", expiryDate);  // Use the parsed DateTime value
                                cmd.Parameters.AddWithValue("@qty", qty);
                                cmd.Parameters.AddWithValue("@disposaldate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show("Invalid expiry date format.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Insert stock adjustment tracking
                        cmd.CommandText = "INSERT INTO tableStockAdjustment(PoNo, pcode, qty, action, reason, remarks, transdate, suppliername) " +
                                          "VALUES (@PoNo, @pcode, @qty, @action, @reason, @remarks, @transdate, @suppliername)";
                        cmd.Parameters.Clear();  // Clear parameters for the new query
                        cmd.Parameters.AddWithValue("@PoNo", lblPoNO.Text);
                        cmd.Parameters.AddWithValue("@pcode", lblproductcode.Text);  // Assuming you have a pcode label
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@action", cbaction.Text);
                        cmd.Parameters.AddWithValue("@reason", cbReason.Text);
                        cmd.Parameters.AddWithValue("@remarks", txtremarks.Text);
                        cmd.Parameters.AddWithValue("@transdate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@suppliername", lblSupplier.Text);
                        cmd.ExecuteNonQuery();
                    }

                    list.ShowAdjustmentStock();
                    MessageBox.Show("Adjustment Successfully Saved!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void Clear()
        {

            txtremarks.Clear();
            lblSupplier.Text= "";
            txtqty.Clear();
            cbaction.Text = "";
            lblproductcode.Text = "";
            lblproductname.Text = "";
            lblPoNO.Text = "";
        }

        private void AdjustmentStockDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnconfirm_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
