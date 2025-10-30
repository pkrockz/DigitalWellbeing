using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DGWellbing
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string theme = Session["Theme"] != null ? Session["Theme"].ToString() : "Light";

                // Apply theme by setting the correct CSS file
                themeStylesheet.Href = theme == "Dark" ? "dark-theme.css" : "styles.css";
            }
            if (Session["UserID"] == null)
            {
                // Hide menu if user is not logged in
                pnlMenu.Visible = false;
            }
            else
            {
                // Show menu if user is logged in
                pnlMenu.Visible = true;
            }
        }
    }
}