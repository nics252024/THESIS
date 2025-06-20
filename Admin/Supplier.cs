using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class Supplier : Form
    {
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        public Supplier()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
        }
        public void ShowSupplier()
        {
            dgvSuppliers.Rows.Clear();
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("SELECT  supId, supname, contactperson, email, contactnum, address FROM tableSupplier", con))
                    {
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            int i = 0;
                            while (dr.Read())
                            {
                                i += 1;
                                dgvSuppliers.Rows.Add(i,dr["supId"].ToString(), dr["supname"].ToString(), dr["contactperson"].ToString(), dr["email"].ToString(), dr["contactnum"].ToString(), dr["address"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while retrieving data: " + ex.Message);
            }
        }
        public void Clear()
        {
            txtSupname.Clear();
            txtconperson.Clear();
            txtemail.Clear();
            txtcontnum.Clear();
            txtaddress.Clear();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Save Suppliers Information?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("INSERT INTO tableSupplier (supname, contactperson, email, contactnum, address) VALUES (@supname, @contactperson, @email, @contactnum, @address)", con);
                    com.Parameters.AddWithValue("@supname", txtSupname.Text);
                    com.Parameters.AddWithValue("@contactperson", txtconperson.Text);
                    com.Parameters.AddWithValue("@email", txtemail.Text);
                    com.Parameters.AddWithValue("@contactnum", txtcontnum.Text);
                    com.Parameters.AddWithValue("@address", txtaddress.Text);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Supplier Information has been Saved.");
                    Clear();
                    ShowSupplier();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving data: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                string columnName = dgvSuppliers.Columns[e.ColumnIndex].Name;
                if (columnName == "Edit")
                {
                    lblSupId.Text = dgvSuppliers.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtSupname.Text = dgvSuppliers.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtconperson.Text = dgvSuppliers.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtemail.Text = dgvSuppliers.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtcontnum.Text = dgvSuppliers.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtaddress.Text = dgvSuppliers.Rows[e.RowIndex].Cells[6].Value.ToString();
                    btnsave.Enabled = false;
                    btnUpdate.Enabled = true;
                    ShowSupplier();
                }
                else if (columnName == "Delete")
                {
                    if (MessageBox.Show("Want to delete Supplier's Information?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand("DELETE FROM tableSupplier WHERE supId = @supId", con);
                            com.Parameters.AddWithValue("@supId", dgvSuppliers.Rows[e.RowIndex].Cells[1].Value.ToString());
                            com.ExecuteNonQuery();
                            MessageBox.Show("Supplier Information has been Deleted.");
                            ShowSupplier();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while deleting data: " + ex.Message);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update Supplier's Information?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string supId = lblSupId.Text;
                    string supname = txtSupname.Text;
                    string contactperson = txtconperson.Text;
                    string email = txtemail.Text;
                    string contactnum = txtcontnum.Text;
                    string address = txtaddress.Text;

                    using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                    {
                        con.Open();
                        using (SqlCommand com = new SqlCommand(
                            @"UPDATE tableSupplier 
                            SET supname = @supname, 
                            contactperson = @contactperson, 
                            email = @email, 
                            contactnum = @contactnum, 
                            address = @address 
                            WHERE supId = @supId", con))
                        {
                            // Set parameter values
                            com.Parameters.AddWithValue("@supId", supId);
                            com.Parameters.AddWithValue("@supname", supname);
                            com.Parameters.AddWithValue("@contactperson", contactperson);
                            com.Parameters.AddWithValue("@email", email);
                            com.Parameters.AddWithValue("@contactnum", contactnum);
                            com.Parameters.AddWithValue("@address", address);

                            // Execute the query and check rows affected
                            int rowsAffected = com.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Supplier Information has been updated.");
                            }
                            else
                            {
                                MessageBox.Show("No rows were updated. Please check the Supplier ID.");
                            }

                            // Clear fields, refresh data, and update UI
                            Clear();
                            ShowSupplier();
                            btnsave.Enabled = true;
                            btnUpdate.Enabled = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Supplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnUpdate_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);    
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colname = dgvSuppliers.Columns[e.ColumnIndex].Name;

                if (colname == "supname")
                {
                    string suppliername = dgvSuppliers.Rows[e.RowIndex].Cells["supname"].Value.ToString();

                    ShowSupplierDetails(suppliername);
                }
            }
        }
        private void ShowSupplierDetails(string suppliername)
        {
            SupplierProducts supplierProductsForm = new SupplierProducts(suppliername);
            supplierProductsForm.TopLevel = false;
            supplierProductsForm.lblsupplier.Text = suppliername;
            supplierProductsForm.Dock = DockStyle.Fill;
            this.Controls.Add(supplierProductsForm);
            supplierProductsForm.BringToFront();
            supplierProductsForm.Show();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcontnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Supplier_Load(object sender, EventArgs e)
        {

        }
    }
    
}
