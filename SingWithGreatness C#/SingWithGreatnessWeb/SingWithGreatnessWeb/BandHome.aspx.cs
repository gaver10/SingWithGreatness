using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SingWithGreatnessWeb
{
    public partial class BandHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDB();
            }
        }

        protected void LoadDB()
        {
            // load for associated band
        }

        protected void addPathButton_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["tracksTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["tracksTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        string path = ((Label)bandGridview.Rows[rowIndex].Cells[0].FindControl("pathLabel")).ToString();
                        string user = ((Label)bandGridview.Rows[rowIndex].Cells[1].FindControl("userLabel")).ToString();

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Path"] = path;
                        dtCurrentTable.Rows[i - 1]["User"] = user;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["tracksTable"] = dtCurrentTable;

                    bandGridview.DataSource = dtCurrentTable;
                    bandGridview.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["tracksTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["tracksTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Label path = ((Label)bandGridview.Rows[rowIndex].Cells[0].FindControl("pathLabel"));
                        Label user = ((Label)bandGridview.Rows[rowIndex].Cells[1].FindControl("userLabel"));

                        path.Text = dt.Rows[i]["Path"].ToString();
                        user.Text = dt.Rows[i]["User"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
    }
}