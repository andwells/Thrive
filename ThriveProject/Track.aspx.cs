using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Track : System.Web.UI.Page
{
    private ISearchableDataManager manager;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["User"] == null)
        {
            Response.Redirect("~/account/login.aspx");
        }
        if (Session["Manager"] != null)
        {
            manager = (ISearchableDataManager)Session["Manager"];
        }
        else
        {
            manager = new FoodManager(sqlDSLocal, sqlDSAccess, ((User)Session["User"]).UserID);
        }
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
    }
    protected void btnSearchFood_Click(object sender, EventArgs e)
    {
        String type = ddlSearchType.SelectedValue;
        String query = tbFood.Text;
        switch (type)
        {
            case "name":
                DataView temp = (DataView)manager.Search(query);
                Session["Dataview"] = temp;

                if (temp == null || temp.Table.Rows.Count == 0)
                {
                    gvResults.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "The food you are looking for does not exist. Do you want to create it?";
                    lbtnCreate.Visible = true;
                }
                else
                {
                    gvResults.Visible = true;
                    gvResults.DataSource = temp;
                    gvResults.DataBind();
                }
                break;
            case "type":
                manager.searchByType(query);
                break;
            case "category":
                manager.searchByCategory(query);
                break;
            default:
                return;
        }
    }
    protected void gvResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvResults.PageIndex = e.NewPageIndex;
        if (Session["Dataview"] != null)
        {
            gvResults.DataSource = (DataView)Session["Dataview"];
        }
        else
        {
            //Response.Write("Error binding DataView");
        }
        gvResults.DataBind();
    }
    protected void gvResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*The reason that there are no checks to see if the DataViewExists in Session
        is because the DataView has to exists for the SelectedIndexChanged Event to fire*/
        DataRow row = ((DataView)Session["DataView"]).Table.Rows[gvResults.SelectedRow.DataItemIndex];

        int id, calories;
        String name;
        List<String> categories = new List<String>();
        bool isRestaurant = false;

        id = (int) row[0];
        calories = (int) row[3];
        

        //Adjust to include parsing serving size
        if (row.Table.Columns.Count > 5)
        {
            name = (string)row[2];
            isRestaurant = (!row[4].Equals(DBNull.Value)) ? (bool)row[4] : false;
            if (!row[5].Equals(DBNull.Value))
            {
                categories.AddRange(((String)row[5]).Split(','));
            }
        }
        else
        {
            name = (string)row[1];
        }
        
        
        Food temp = new Food(id, calories, name, categories, isRestaurant, "");
        Session["SelectedFood"] = temp;
        
        //This works
        gvResults.Visible = false;

        //Add logic for display how many servings and what meal
    }
    protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count == 4)
        {
            e.Row.Cells[1].Visible = false;
        }

        //if (e.Row.Cells.Count > 6)
        //{
        //    e.Row.Cells[5].Visible = false;
        //    e.Row.Cells[6].Visible = false;
        //}
    }
}