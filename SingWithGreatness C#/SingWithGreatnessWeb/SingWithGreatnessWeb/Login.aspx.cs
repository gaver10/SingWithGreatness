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
            // check against db

            if (loginUsernameTextbox.Text == "Steve" && loginPasswordTextbox.Text == "bees")
            {
                Response.Redirect("~/Mixer.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "loginError", "alert('Incorrect login details.');", true);
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(registerUsernameTextbox.Text) ||
                string.IsNullOrEmpty(registerPasswordTextbox.Text) ||
                string.IsNullOrEmpty(registerEmailTextbox.Text) ||
                accountTypeRadioButtonList.SelectedIndex < 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "registerError", "alert('All fields must be filled in order to register.');", true);
            }
            else
            {
                // check if username or email already exists
                // else sign them up

                if (accountTypeRadioButtonList.SelectedIndex == 0)
                {
                    Response.Redirect("~/BandHome.aspx");
                }
                else
                {
                    Response.Redirect("~/Mixer.aspx");
                }
            }
        }
    }
}