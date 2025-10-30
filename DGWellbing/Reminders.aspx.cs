using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGWellbing
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DGWellbeingDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReminders();
            }
        }

        // Load all reminders for the logged-in user
        private void LoadReminders()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ReminderID, Title, ReminderDateTime FROM Reminders WHERE UserID = @UserID ORDER BY ReminderDateTime ASC";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                gvReminders.DataSource = reader;
                gvReminders.DataBind();
            }
        }

        protected void btnAddReminder_Click(object sender, EventArgs e)
        {
            // Ensure input is not empty
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDateTime.Text))
            {
                Response.Write("<script>alert('Please fill all fields.');</script>");
                return;
            }

            // Validate and parse DateTime
            DateTime reminderDateTime;
            if (!DateTime.TryParse(txtDateTime.Text, out reminderDateTime))
            {
                Response.Write("<script>alert('Invalid date/time format. Use YYYY-MM-DD HH:MM.');</script>");
                return;
            }

            // Insert into database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Reminders (UserID, Title, ReminderDateTime) VALUES (@UserID, @Title, @ReminderDateTime)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@ReminderDateTime", reminderDateTime);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadReminders(); // Refresh reminders list
        }


        // Edit Reminder
        protected void gvReminders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvReminders.EditIndex = e.NewEditIndex;
            LoadReminders();
        }

        // Delete Reminder
        protected void gvReminders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int reminderID = Convert.ToInt32(gvReminders.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Reminders WHERE ReminderID = @ReminderID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ReminderID", reminderID);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            LoadReminders(); // Refresh list
        }

    }
}
