using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.CodeDom;
using System.IO;

namespace PIERANGELO
{
    public partial class EditProfile : Form
    {
        public string Username { get; set; }
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        DatabaseConnection dbcon = new DatabaseConnection();

        public EditProfile(string username)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.DataConnection());
            Username = username;
            LoadProfilePicture(Username);
            this.KeyPreview = true;
        }
       


        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadProfilePicture(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username is empty. Cannot load profile picture.");
                return;
            }

            string query = "SELECT name, contactnum, profilePicture FROM tableUserAccount WHERE username = @username";

            using (SqlConnection conn = new SqlConnection(dbcon.DataConnection()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve and set the user's name
                            if (reader["name"] != DBNull.Value)
                            {
                                txtName.Text = reader["name"].ToString();
                            }

                            // Retrieve and set the user's contact number
                            if (reader["contactnum"] != DBNull.Value)
                            {
                                txtcontact.Text = reader["contactnum"].ToString();
                            }

                            // Retrieve and set the profile picture
                            if (reader["profilePicture"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])reader["profilePicture"];

                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    pictureProfile.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                pictureProfile.Image = null; // Or set a default image
                            }
                        }
                        else
                        {
                            // Handle case where no user is found
                            txtName.Text = string.Empty;
                            txtcontact.Text = string.Empty;
                            pictureProfile.Image = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profile picture: " + ex.Message);
                }
            }
        }



        private void pictureProfile_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0,pictureProfile.Width - 1, pictureProfile.Height - 1);
                Region rg = new Region(gp);
                pictureProfile.Region = rg;

                g.DrawEllipse(new Pen(Color.Black, 1), 0, 0, pictureProfile.Width - 1, pictureProfile.Height - 1);
            }
        }

        private void btnUploadProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureProfile.Image = new Bitmap(dialog.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Username is empty. Cannot delete profile picture.");
                return;
            }

            string query = "UPDATE tableUserAccount SET ProfilePicture = NULL WHERE username = @username";

            using (SqlConnection conn = new SqlConnection(dbcon.DataConnection()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", Username);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile picture deleted successfully.");
                        // Clear the local UI element displaying the profile picture
                        pictureProfile.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("No profile picture found to delete.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting profile picture: " + ex.Message);
                }
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            AdminMainForm admin = (AdminMainForm)Application.OpenForms["AdminMainForm"];
            if (string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Username is empty. Cannot save profile.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(dbcon.DataConnection()))
                {
                    string query = "UPDATE tableUserAccount SET name = @name, contactnum = @contactnum, profilePicture = @profilePicture, username = @newUsername WHERE username = @username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", Username);
                        cmd.Parameters.AddWithValue("@newUsername", txtUsername.Text); // Assuming txtUsername is the TextBox for the new username
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@contactnum", txtcontact.Text);

                        if (pictureProfile.Image != null)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (Bitmap bmp = new Bitmap(pictureProfile.Image))
                                {
                                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                cmd.Parameters.AddWithValue("@profilePicture", ms.ToArray());
                            }
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@profilePicture", DBNull.Value); // Or use a default value
                        }

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile updated successfully.");
                            Username = txtUsername.Text; // Update the local variable to the new username

                            // Close the current AdminMainForm instance
                            admin.Close();

                            // Reopen the AdminMainForm with the updated username
                            AdminMainForm newAdminForm = new AdminMainForm(Username);
                            newAdminForm.Show();
                            newAdminForm.Activate(); // Bring the new form to the foreground
                        }
                        else
                        {
                            MessageBox.Show("Profile update failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message);
            }
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditProfile_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnsave_Click(sender, e);
            }
            else if(e.KeyCode == Keys.Escape)
            {
                buttonBack_Click(sender, e);
            }
        }
    }
}
