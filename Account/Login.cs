using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIERANGELO
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        DatabaseConnection dbcon = new DatabaseConnection();
        SqlDataReader dr;
        
        public string password, usernameaccount = "";
        public string status = "";

        private int loginAttempts = 1;
        private const int MaxLoginAttempts = 3;


        //public object profilepicture;

        public Login()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            this.KeyPreview = true;
            
        }
        
        private void btnlogin_Click(object sender, EventArgs e)
        {
            string roleaccount = "", accountname = "";
            try
            {
                bool found = false;

                con.Open();

                com = new SqlCommand("SELECT * FROM tableUserAccount WHERE username COLLATE LATIN1_General_BIN = @username AND password COLLATE Latin1_General_BIN = @password", con);
                com.Parameters.AddWithValue("@username", txtusername.Text.Trim());
                com.Parameters.AddWithValue("@password", txtpassword.Text.Trim());

                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    found = true;
                    usernameaccount = dr["username"].ToString();
                    roleaccount = dr["role"].ToString();
                    accountname = dr["name"].ToString();
                    password = dr["password"].ToString();
                    status = dr["status"].ToString(); // Fetch the status field
                }
                dr.Close();
                con.Close();

                if (found)
                {
                    if (status != "Active")
                    {
                        MessageBox.Show("Account is Inactive, Unable to Login", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    if (loginAttempts <= MaxLoginAttempts)
                    {
                        DialogResult message = MessageBox.Show("Hello! Welcome " + accountname + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (message == DialogResult.OK)
                        {
                            txtusername.Clear();
                            txtpassword.Clear();
                            this.Hide();

                            Log(accountname, $"The {roleaccount} has successfully logged in.", "Log In");

                            if (roleaccount == "Cashier")
                            {
                                CashierModule cashmods = new CashierModule();
                                cashmods.lblaccoutname.Text = accountname;
                                cashmods.lblUser.Text = usernameaccount;
                                cashmods.lblrole.Text = roleaccount + " | " + accountname;
                                cashmods.lblrole.AutoSize = true;
                                cashmods.lblrole.MaximumSize = new Size(500, 0);
                                cashmods.lblrole.TextAlign = ContentAlignment.MiddleLeft;
                                cashmods.ShowProduct();
                                cashmods.ShowDialog();
                            }
                            else
                            {
                                AdminMainForm dashmods = new AdminMainForm(usernameaccount);
                                dashmods.lblUser.Text = usernameaccount;
                                dashmods.lblname.Text = accountname;
                                dashmods.lblname.AutoSize = true;
                                dashmods.lblname.MaximumSize = new Size(200, 0);
                                dashmods.lblname.TextAlign = ContentAlignment.MiddleLeft;
                                dashmods.lblRole.Text = roleaccount;
                                dashmods.lblRole.AutoSize = true;
                                dashmods.lblRole.MaximumSize = new Size(200, 0);
                                dashmods.lblRole.TextAlign = ContentAlignment.MiddleLeft;
                                dashmods.password_ = password;
                                dashmods.username_ = usernameaccount;
                                dashmods.btndashboard.BackColor = Color.FromArgb(143, 151, 184);
                               

                                dashmods.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    loginAttempts++;
                    if (loginAttempts >= MaxLoginAttempts)
                    {
                        MessageBox.Show("Maximum login attempts reached. Please try again later.", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnlogin.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username or Password! Attempt " + loginAttempts + " of " + MaxLoginAttempts, "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void Log(string user, string activity, string action)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO tableLogHistory (username, Date, activity ,action) VALUES (@username, @date, @activity, @action)", con);
                command.Parameters.AddWithValue("@username", user);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@activity", activity);
                command.Parameters.AddWithValue("@action", action);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void checkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = checkshowpassword.Checked ? '\0' : '•';
        }
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnlogin_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button1_Click(sender, e);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("YOU WANT TO EXIT THE APPLICATION?", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
