using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGWellbing
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFocusStats();
                LoadAppUsage();
            }
        }

        private void LoadFocusStats()
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                // Get total focus sessions
                SqlCommand cmdSessions = new SqlCommand("SELECT COUNT(*) FROM FocusSessions WHERE UserId = @UserId", con);
                cmdSessions.Parameters.AddWithValue("@UserId", Session["UserId"]);
                lblTotalSessions.Text = cmdSessions.ExecuteScalar().ToString();

                // Get total focus time
                SqlCommand cmdTime = new SqlCommand("SELECT SUM(Duration) FROM FocusLogs WHERE UserId = @UserId", con);
                cmdTime.Parameters.AddWithValue("@UserId", Session["UserId"]);
                object result = cmdTime.ExecuteScalar();
                lblTotalTime.Text = result != DBNull.Value ? result.ToString() + " mins" : "0 mins";
            }
        }

        private void LoadAppUsage()
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT AppName, UsageTime FROM AppUsageLogs WHERE UserId = @UserId", con);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                SqlDataReader dr = cmd.ExecuteReader();
                gvAppUsage.DataSource = dr;
                gvAppUsage.DataBind();
            }
        }
    }
}
