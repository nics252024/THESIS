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
using System.Security.Cryptography;

namespace PIERANGELO
{
    public partial class Account : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();

        public Account()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            ShowUsers();
            this.KeyPreview = true;
        }

        private void ShowUsers()
        {
            int i = 0;
            dgvUserTable.Rows.Clear();

            // Capture search and sort inputs
            string searchValue = txtsearch.Text.Trim();
            string sortRole = cbsortrole.SelectedItem?.ToString();

            // Start building the SQL query
            string query = "SELECT username, name, password, role, contactnum, status FROM tableUserAccount";

            // Apply search filter if txtsearch is not empty
            if (!string.IsNullOrEmpty(searchValue))
            {
                query += " WHERE username LIKE @search OR name LIKE @search";
            }

            // Apply sorting if a role is selected in cbsortrole
            if (!string.IsNullOrEmpty(sortRole) && sortRole != "All")
            {
                query += string.IsNullOrEmpty(searchValue) ? " WHERE" : " AND";
                query += " role = @role";
            }

            // Execute the query
            using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    // Add parameters to prevent SQL injection
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        com.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                    }
                    if (!string.IsNullOrEmpty(sortRole) && sortRole != "All")
                    {
                        com.Parameters.AddWithValue("@role", sortRole);
                    }

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            // Populate the DataGridView rows with the data from the reader
                            dgvUserTable.Rows.Add(i,
                                dr["username"].ToString(),
                                dr["name"].ToString(),
                                dr["password"].ToString(),
                                dr["role"].ToString(),
                                dr["contactnum"].ToString(),
                                dr["status"].ToString());
                        }
                    }
                }
            }
        }

        private void buttonBack_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;
        }
        private void Clear()
        {
            txtName.Clear();
            txtCreatePassword.Clear();
            txtConfirmPassword.Clear();
            txtUsername.Clear();
            cbRole.Text = "";
            txtUsername.Focus();
            txtcontact.Clear();
            
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation checks
                if (txtCreatePassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Password and confirmation password do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please Enter your Full Name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Please Enter your username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtcontact.Text))
                {
                    MessageBox.Show("Please Enter your contact number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(cbRole.Text))
                {
                    MessageBox.Show("Please select role.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Check if the username already exists
                con.Open();
                com = new SqlCommand("SELECT COUNT(*) FROM tableUserAccount WHERE username = @username", con);
                com.Parameters.AddWithValue("@username", txtUsername.Text);
                int userCount = (int)com.ExecuteScalar();
                con.Close();

                if (userCount > 0)
                {
                    MessageBox.Show("The username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Convert the image to a byte array
                byte[] profileImageBytes = null;
                if (pictureProfile.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureProfile.Image.Save(ms, pictureProfile.Image.RawFormat);
                        profileImageBytes = ms.ToArray();
                    }
                }

                // Insert data into the database
                con.Open();
                com = new SqlCommand("INSERT INTO tableUserAccount (username, password, role, name, contactnum, profilePicture) VALUES (@username, @password, @role, @name, @contactnum, @profilePicture)", con);
                com.Parameters.AddWithValue("@username", txtUsername.Text);
                com.Parameters.AddWithValue("@password", txtCreatePassword.Text);
                com.Parameters.AddWithValue("@role", cbRole.Text);
                com.Parameters.AddWithValue("@name", txtName.Text);
                com.Parameters.AddWithValue("@contactnum", txtcontact.Text);
                com.Parameters.AddWithValue("@profilePicture", profileImageBytes ?? (object)DBNull.Value);  // Use DBNull.Value if the image is null

                com.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Success! Your Account is Saved");
                Clear();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
     
        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Assuming you have a method to hash passwords
        private string HashPasswordP(string password)
        {
            // Implement your hashing logic here, using a secure algorithm like SHA256
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Method to get the hashed password from the database for a given username
       
        private void buttosavechanges_Click(object sender, EventArgs e)
        {

            try
            {
                // Open the connection
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();

                    // Fetch the current password for the given username
                    SqlCommand com = new SqlCommand("SELECT password FROM tableUserAccount WHERE username = @username", con);
                    com.Parameters.AddWithValue("@username", txtcpusername.Text);

                    string currentPassword = com.ExecuteScalar()?.ToString(); // Use ExecuteScalar to get a single value

                    // Check if the entered current password matches the stored password
                    if (currentPassword == null || currentPassword != txtcpcurrentpassword.Text)
                    {
                        MessageBox.Show("Current password did not match", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // Check if the new password matches the confirmation
                    if (txtcpnewpassword.Text != txtcpconfirmnewpassword.Text)
                    {
                        MessageBox.Show("New password and confirmation password do not match", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    com = new SqlCommand("UPDATE tableUserAccount SET password = @password WHERE username = @username", con);
                    com.Parameters.AddWithValue("@username", txtcpusername.Text);
                    com.Parameters.AddWithValue("@password", txtcpnewpassword.Text);
                   

                    com.ExecuteNonQuery();

                    MessageBox.Show("Password successfully changed", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearChangePass(); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtuname_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnsaveaa_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                com = new SqlCommand("SELECT * FROM tableUserAccount WHERE username = @username", con);
                com.Parameters.AddWithValue("@username", txtuname.Text);
                SqlDataReader dr = com.ExecuteReader();
                bool found = dr.HasRows;
                string storedPasswordHash = string.Empty;

                if (found)
                {
                    dr.Read();
                    storedPasswordHash = dr["password"].ToString(); // Fetch hashed password
                }
                dr.Close();
                con.Close();

                if (found)
                {
                    // Show the password verification form
                    using (PasswordVerification passwordForm = new PasswordVerification())
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            string enteredPassword = passwordForm.EnteredPassword;

                            // Assuming you have a method to verify the hashed password
                            if (VerifyPassword(enteredPassword, storedPasswordHash))
                            {
                                if (Delete.Checked)
                                {
                                    DeleteAccount(txtuname.Text);
                                }
                                else if (Deactive.Checked)
                                {
                                    DeactivateAccount(txtuname.Text);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Password confirmation failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Account does not exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteAccount(string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    con.Open();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        // Retrieve the account details before deletion
                        string querySelect = "SELECT name, username, password, role, contactnum FROM tableUserAccount WHERE username = @username";
                        string accountName = "", accountPassword = "", accountRole = "", accountContactNum = "";

                        using (SqlCommand cmdSelect = new SqlCommand(querySelect, con, transaction))
                        {
                            cmdSelect.Parameters.AddWithValue("@username", username);
                            using (SqlDataReader reader = cmdSelect.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    accountName = reader["name"].ToString();
                                    accountPassword = reader["password"].ToString();
                                    accountRole = reader["role"].ToString();
                                    accountContactNum = reader["contactnum"].ToString();
                                }
                            }
                        }

                        // Insert into tableDeleteAccount
                        string queryInsert = "INSERT INTO tableDeleteAccount (Name, Username, Password, Role, ContactNum, DeletionDate) " +
                                             "VALUES (@name, @username, @password, @role, @contactnum, GETDATE())";
                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, con, transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@name", accountName);
                            cmdInsert.Parameters.AddWithValue("@username", username);
                            cmdInsert.Parameters.AddWithValue("@password", accountPassword);
                            cmdInsert.Parameters.AddWithValue("@role", accountRole);
                            cmdInsert.Parameters.AddWithValue("@contactnum", accountContactNum);
                            cmdInsert.ExecuteNonQuery();
                        }

                        // Delete the account from tableUserAccount
                        string queryDelete = "DELETE FROM tableUserAccount WHERE username = @username";
                        using (SqlCommand cmdDelete = new SqlCommand(queryDelete, con, transaction))
                        {
                            cmdDelete.Parameters.AddWithValue("@username", username);
                            cmdDelete.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Account deleted successfully.");
                        ClearArchive();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DeactivateAccount(string username)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.DataConnection()))
                {
                    // Update the status to Deactive
                    string queryUpdate = "UPDATE tableUserAccount SET status = 'Deactive' WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Account deactivated successfully.");
                    ClearArchive();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Replace this with your actual password verification logic
            return enteredPassword == storedPasswordHash; // This is a placeholder
        }


        private void Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsave_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                buttosavechanges_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Enter)
            {
              btnsaveaa_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btncancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
              btncpcancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button1_Click(sender, e);
            }
        }
        public void ClearChangePass()
        {
            txtcpusername.Clear();
            txtcpcurrentpassword.Clear();
            txtcpnewpassword.Clear();
            txtcpconfirmnewpassword.Clear();
        }
        private void btncpcancel_Click(object sender, EventArgs e)
        {
            ClearChangePass();
        }

        public void ClearArchive()
        {
            txtuname.Clear();
            Delete.Checked = false;
            Deactive.Checked = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ClearArchive();

        }

        private void btnUploadProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureProfile.Image = new Bitmap(dialog.FileName);
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            ShowUsers();
        }

        private void cbsortrole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowUsers();
        }

        private void cbsortrole_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowUsers();
        }

        private void btnsave_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
