using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGWellbing
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFocusSessions();
                CheckOngoingSession();
            }
        }

        private void CheckOngoingSession()
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT StartTime FROM FocusSessions WHERE UserId = @UserId AND EndTime IS NULL", con);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                object startTimeObj = cmd.ExecuteScalar();

                if (startTimeObj != null)
                {
                    btnStartFocus.Enabled = false;
                    btnPauseFocus.Enabled = true;
                    btnEndFocus.Enabled = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "startTimer", "startTimer();", true);
                }
            }
        }

        protected void btnStartFocus_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO FocusSessions (UserId, StartTime) VALUES (@UserId, @StartTime)", con);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                cmd.Parameters.AddWithValue("@StartTime", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

            btnStartFocus.Enabled = false;
            btnPauseFocus.Enabled = true;
            btnEndFocus.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "startTimer", "startTimer();", true);
        }

        protected void btnEndFocus_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmdStart = new SqlCommand("SELECT StartTime FROM FocusSessions WHERE UserId = @UserId AND EndTime IS NULL", con);
                cmdStart.Parameters.AddWithValue("@UserId", Session["UserId"]);
                object startTimeObj = cmdStart.ExecuteScalar();

                if (startTimeObj != null)
                {
                    DateTime startTime = Convert.ToDateTime(startTimeObj);
                    TimeSpan duration = DateTime.Now - startTime;
                    int totalSeconds = (int)duration.TotalSeconds;

                    SqlCommand cmdEnd = new SqlCommand("UPDATE FocusSessions SET EndTime = @EndTime, Duration = @Duration WHERE UserId = @UserId AND EndTime IS NULL", con);
                    cmdEnd.Parameters.AddWithValue("@EndTime", DateTime.Now);
                    cmdEnd.Parameters.AddWithValue("@Duration", totalSeconds);  // Storing duration in seconds
                    cmdEnd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    cmdEnd.ExecuteNonQuery();
                }
            }

            btnStartFocus.Enabled = true;
            btnPauseFocus.Enabled = false;
            btnEndFocus.Enabled = false;
            ClientScript.RegisterStartupScript(this.GetType(), "resetTimer", "elapsedTime = 0; document.getElementById('timer').innerText = '00:00:00';", true);
            LoadFocusSessions();
        }


        private void LoadFocusSessions()
{
    string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DGWellbeingDB.mdf;Integrated Security=True";
    using (SqlConnection con = new SqlConnection(connStr))
    {
        con.Open();
        SqlCommand cmd = new SqlCommand(" SELECT StartTime, EndTime, FORMAT(DATEADD(SECOND, Duration, 0), 'HH:mm:ss') AS Duration FROM FocusSessions WHERE UserId = @UserId ORDER BY StartTime DESC", con);
        cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

        SqlDataReader dr = cmd.ExecuteReader();
        gvFocusSessions.DataSource = dr;
        gvFocusSessions.DataBind();
    }
}

    }
}