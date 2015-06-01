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
            DataTable dt = DbHelper.GetDBData("SELECT * FROM tracks WHERE band = '" + Globals.currentUser + "'");
            bandGridview.DataSource = dt;
            bandGridview.DataBind();

            ViewState["tracksTable"] = dt;
        }

        protected void addPathButton_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Globals.currentUser = "";
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
                        string path = bandGridview.Rows[rowIndex].Cells[2].ToString();
                        string user = bandGridview.Rows[rowIndex].Cells[3].ToString();

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Path"] = path;
                        dtCurrentTable.Rows[i - 1]["User"] = user;

                        rowIndex++;
                    }
                    var id = DbHelper.GetNextID("tracks");
                    string sql = "INSERT INTO tracks (id) VALUES ('" + id.ToString() + "')";
                    DbHelper.SendQuery(sql);

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
                        string path = bandGridview.Rows[rowIndex].Cells[2].ToString();
                        string user = bandGridview.Rows[rowIndex].Cells[3].ToString();

                        path = dt.Rows[i]["Path"].ToString();
                        user = dt.Rows[i]["User"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void bandGridview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            bandGridview.EditIndex = -1;
            this.LoadDB();
        }

        protected void bandGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = bandGridview.Rows[e.RowIndex].Cells[1].ToString();
            
            string sql = "DELETE FROM tracks WHERE id = '" + id.ToString() + "'";
            DbHelper.SendQuery(sql);

            this.LoadDB();
        }

        protected void bandGridview_RowEditing(object sender, GridViewEditEventArgs e)
        {
            bandGridview.EditIndex = e.NewEditIndex;
            this.LoadDB();
        }

        protected void bandGridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var id = bandGridview.Rows[e.RowIndex].Cells[1].ToString();
            GridViewRow row = bandGridview.Rows[e.RowIndex] as GridViewRow;

            TextBox tPath = row.FindControl("pathTextbox") as TextBox;
            TextBox tUser = row.FindControl("userTextbox") as TextBox;

            string sql = "UPDATE tracks SET path = '" + tPath.Text + "', user = '" + tUser.Text + "', band = '" + Globals.currentUser + "' WHERE id = '" + id.ToString() + "'";
            DbHelper.SendQuery(sql);

            this.LoadDB();
        }
    }
}