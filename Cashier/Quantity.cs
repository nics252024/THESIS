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
    public partial class Quantity : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        

        private String pcode;
        private double price;
        private int qty;
        private String transo;

     
        CashierModule cashier;
        public Quantity(CashierModule module)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            cashier = module;
        }

        public void ProductInfo(String pcode, double price, String transo, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transo = transo;
            this.qty = qty;
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == 13) && (txtQuantity.Text != String.Empty))
            {
                String id = "";
                int salesqty = 0;
                Boolean found = false;
                
                con.Open();
                com = new SqlCommand("SELECT * FROM tableSales WHERE transactionNo = @transactionNo AND pcode = @pcode", con);
                com.Parameters.AddWithValue("@transactionNo", cashier.lbltransaction.Text);
                com.Parameters.AddWithValue("@pcode", pcode);
                dr = com.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString();
                    salesqty = int.Parse(dr["qty"].ToString());
                }
                else
                {
                    found = false;
                }
                dr.Close();
                con.Close();


                if(found == true)
                {
                    if (qty < (int.Parse(txtQuantity.Text) + salesqty))
                    {
                        MessageBox.Show("Remaining Quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    con.Open();
                    com = new SqlCommand("UPDATE tableSales SET qty += @qty WHERE id = '" + id + "'", con);
                    com.Parameters.AddWithValue("@qty", int.Parse(txtQuantity.Text));
                    com.ExecuteNonQuery();
                    con.Close();
                    cashier.txtBarcode.Clear();
                    cashier.txtBarcode.Focus();
                    cashier.ShowSales();
                    this.Dispose();
                }
                else
                {
                    if (qty < int.Parse(txtQuantity.Text))
                    {
                        MessageBox.Show("Remaining Quantity on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    con.Open();
                    com = new SqlCommand("INSERT INTO tableSales (transactionNo ,pcode ,price ,qty ,transdate,cashier) VALUES (@transactionNo, @pcode, @price, @qty, @transdate,@cashier)", con);
                    com.Parameters.AddWithValue("@transactionNo", transo);
                    com.Parameters.AddWithValue("@pcode", pcode);
                    com.Parameters.AddWithValue("@price", price);
                    com.Parameters.AddWithValue("@qty", int.Parse(txtQuantity.Text));
                    com.Parameters.AddWithValue("@transdate", DateTime.Now);
                    com.Parameters.AddWithValue("@cashier", cashier.lblaccoutname.Text);
                    com.ExecuteNonQuery();
                    con.Close();

                    cashier.txtBarcode.Clear();
                    cashier.txtBarcode.Focus();
                    cashier.ShowSales();
                    this.Dispose();
                }
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
