using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class Account_Register : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
    }

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);
        Guid newUserID = (Guid)Membership.GetUser(RegisterUser.UserName).ProviderUserKey;

        Session["UserString"] = RegisterUser.UserName + ";" + newUserID.ToString();

        // Insert a new record into UserProfiles
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string insertSql = "INSERT INTO ProfileData(UserId) VALUES(@UserId)";
 
     using (SqlConnection myConnection = new SqlConnection(connectionString))
     {
          myConnection.Open();
          SqlCommand myCommand = new SqlCommand(insertSql, myConnection);
          myCommand.Parameters.AddWithValue("@UserId", newUserID);
          myCommand.ExecuteNonQuery();
          myConnection.Close();
     }

     //string continueUrl = RegisterUser.ContinueDestinationPageUrl;
     //if (!OpenAuth.IsLocalUrl(continueUrl))
     //{
     //    continueUrl = "~/";
     //}
     Response.Redirect("~/Account/AdditionalInformation.aspx");
        
    }
    protected void RegisterUser_ActiveStepChanged(object sender, EventArgs e)
    {

    }
}