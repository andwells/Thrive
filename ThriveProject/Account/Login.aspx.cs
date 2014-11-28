using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.AspNet.Membership.OpenAuth;

public partial class Account_Login : Page
{
    private IDataManager manager;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["manager"] == null)
        {
            manager = new UserManager();
        }
        else
        {
            manager = (IDataManager)Session["manager"];
        }
        RegisterHyperLink.NavigateUrl = "Register";
        OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

        var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        if (!String.IsNullOrEmpty(returnUrl))
        {
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }
    }
    protected void loginDude_LoggedIn(object sender, EventArgs e)
    {
        User x = (User)manager.Get(loginDude.UserName);

        Session["User"] = x;
        Response.Redirect("~/");
    }
}