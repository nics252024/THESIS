using Microsoft.ReportingServices.Diagnostics.Internal;
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
    public partial class SupplierProducts : Form
    {
        SqlConnection con;
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        private string supplierName;


        public SupplierProducts(string supplierName)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.supplierName = supplierName;
            ShowSupplierProducts();
        }
        private void ShowSupplierProducts()
        {
            try
            {
                con.Open();
                string query = @"SELECT productname, supprice
                         FROM tableProductList 
                         WHERE supplier = @supplier";
                SqlCommand cmd = new SqlCommand(query, con);

                // Add supplier parameter before executing the query
                cmd.Parameters.AddWithValue("@supplier", supplierName);

                dr = cmd.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    i += 1;
                    // Format supprice with a peso sign
                    string formattedPrice = "₱" + Convert.ToDecimal(dr["supprice"]).ToString("N2");
                    dgvSupProd.Rows.Add(i, dr["productname"].ToString(), formattedPrice);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading Products", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
