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
    public partial class ChangePassword : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;

        CashierModule cashier;

        private bool isCurrentPasswordShown = false;
        private bool isNewPasswordShown = false;
        private bool isRepeatPasswordShown = false;
        public ChangePassword(CashierModule cashier)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.cashier=cashier;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbcurrent_Click(object sender, EventArgs e)
        {
            if (isCurrentPasswordShown)
            {
                txtcurrentpass.PasswordChar = '•';
                isCurrentPasswordShown = false;
            }
            else
            {
                txtcurrentpass.PasswordChar = '\0'; 
                isCurrentPasswordShown = true;
            }
        }

        private void pbnew_Click(object sender, EventArgs e)
        {
            if (isNewPasswordShown)
            {
                txtnewpass.PasswordChar = '•'; 
                isNewPasswordShown = false;
            }
            else
            {
                txtnewpass.PasswordChar = '\0';
                isNewPasswordShown = true;
            }

        }

        private void pbconfirm_Click(object sender, EventArgs e)
        {
            if (isRepeatPasswordShown)
            {
                txtrepeatpass.PasswordChar = '•'; 
                isRepeatPasswordShown = false;
            }
            else
            {
                txtrepeatpass.PasswordChar = '\0'; 
                isRepeatPasswordShown = true;
            }
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            try
            {

                string currentpass = dbcon.PasswordValue(cashier.lblUser.Text);
                if (currentpass != txtcurrentpass.Text)
                {
                    MessageBox.Show(" Current Password did not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtnewpass.Text != txtrepeatpass.Text)
                {
                    MessageBox.Show("Password did not match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Change Password", "Confimation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        com = new SqlCommand("UPDATE tableUserAccount SET password = @password WHERE username = @username", con);
                        com.Parameters.AddWithValue("@username", cashier.lblUser.Text);
                        com.Parameters.AddWithValue("@password", txtnewpass.Text);
                        com.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Successfully have the new Password!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
    

