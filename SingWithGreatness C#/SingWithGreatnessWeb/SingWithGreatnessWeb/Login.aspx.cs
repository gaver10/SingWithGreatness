using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
            DataTable usersReturned = DbHelper.GetDBData("SELECT * FROM users WHERE username = '" + loginUsernameTextbox.Text + "' AND password = '" + loginPasswordTextbox.Text + "'");

            if (usersReturned.Rows.Count > 0)
            {
                Globals.currentUser = loginUsernameTextbox.Text;

                if (Convert.ToInt32(usersReturned.Rows[0]["userType"].ToString()) == 0)
                {
                    Response.Redirect("~/Mixer.aspx");
                }
                else
                {
                    Response.Redirect("~/BandHome.aspx");
                }
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
            else if (DbHelper.GetDBData("SELECT * FROM users WHERE username = '" + registerUsernameTextbox.Text + "'").Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "usernameError", "alert('Username already exists.');", true);
            }
            else if (DbHelper.GetDBData("SELECT * FROM users WHERE email = '" + registerEmailTextbox + "'").Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "emailError", "alert('Email address already registered.');", true);
            }
            else
            {
                int id = DbHelper.GetNextID("users");
                string sql = "INSERT INTO users (id, username, password, email, userType) VALUES ('" + id.ToString() + "', '" + registerUsernameTextbox.Text + "', '" +
                    registerPasswordTextbox.Text + "', '" + registerEmailTextbox.Text + "', '" + accountTypeRadioButtonList.SelectedIndex + "')";
                DbHelper.SendQuery(sql);

                Globals.currentUser = registerUsernameTextbox.Text;

                if (accountTypeRadioButtonList.SelectedIndex == 0)
                {
                    Response.Redirect("~/Mixer.aspx");
                }
                else
                {
                    Response.Redirect("~/BandHome.aspx");
                }
            }
        }
    }
}