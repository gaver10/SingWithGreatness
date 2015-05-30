using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SingWithGreatnessWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            wrongLoginLabel.Visible = false;

            if (loginUsernameTextbox.Text == "Steve" && loginPasswordTextbox.Text == "bees")
            {
                Response.Redirect("~/Mixer.aspx");
            }
            else
            {
                wrongLoginLabel.Visible = true;
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mixer.aspx");
        }
    }
}