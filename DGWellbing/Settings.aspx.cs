using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGWellbing
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserSettings();
            }
        }

        private void LoadUserSettings()
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string query = "SELECT EnableReminders, Theme FROM Users WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())  // Only runs if user settings exist
                        {
                            chkEnableReminders.Checked = dr.GetBoolean(0);  // First column (EnableReminders)
                            rblTheme.SelectedValue = dr.GetString(1);  // Second column (Theme)

                            // Store theme in session
                            Session["Theme"] = rblTheme.SelectedValue;
                        }
                    }
                }
            }
        }


        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            string selectedTheme = rblTheme.SelectedValue;

            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Users SET EnableReminders = @EnableReminders, Theme = @Theme WHERE UserId = @UserId", con);
                cmd.Parameters.AddWithValue("@EnableReminders", chkEnableReminders.Checked);
                cmd.Parameters.AddWithValue("@Theme", selectedTheme);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                cmd.ExecuteNonQuery();
            }

            // Update theme in session
            Session["Theme"] = selectedTheme;

            // Reload page to apply new theme
            Response.Redirect(Request.RawUrl);
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}