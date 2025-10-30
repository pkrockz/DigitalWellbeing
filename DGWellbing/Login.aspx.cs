using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace DGWellbing
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear session on page load (optional)
            Session.Clear();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Encrypt input password using Base64 encoding (same as registration)
            string encryptedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, Name FROM Users WHERE Email = @Email AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Store user details in session
                            Session["UserID"] = reader["UserID"];
                            Session["UserName"] = reader["Name"];

                            Response.Redirect("Dashboard.aspx"); // Redirect to Dashboard or Home Page
                        }
                        else
                        {
                            lblMessage.Text = "Invalid email or password.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx"); // Redirect to registration page
        }
    }
}
