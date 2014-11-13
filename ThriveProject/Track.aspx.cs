using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Food : System.Web.UI.Page
{
    private IDataManager manager;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["UserString"] == null)
        {
            Response.Redirect("~/account/login.aspx");
        }
        if (Session["Manager"] != null)
        {
            manager = (IDataManager)Session["Manager"];
        }
        else
        {
            manager = new FoodManager(sqlDSLocal, sqlDSAccess, 
                new Guid(
                            (((String)Session["UserString"]).Split(';')[1])
                        )
                );
        }
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
    }
}