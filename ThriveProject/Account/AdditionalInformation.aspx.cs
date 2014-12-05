using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class Account_AdditionalInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["User"] == null)
        {
            Response.Redirect("~/");
        }
    }
    protected void wzrdAdditional_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        int feet = Int32.Parse(lbFeet.SelectedValue);
        int inches = Int32.Parse(lbInches.SelectedValue);
        int height = (feet * 12) + inches;
        int weight = Int32.Parse(tbWeight.Text);
        int age = Int32.Parse(tbAge.Text);
        String gender = ddlGender.SelectedValue; 
        int weightGoal = Int32.Parse(rblWeightGoal.SelectedValue);
        int sleepFlag = Int32.Parse(rblSleepTracking.SelectedValue);
        String sleepGoal = tbSleep.Text;
        int hydrationFlag = Int32.Parse(rblHydrationtracking.SelectedValue);
        String hydrationGoal = tbHydration.Text;
        
        // Insert a new record into UserProfiles
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        string insertSql = "UpdateProfile";
        using (SqlConnection myConnection = new SqlConnection(connectionString))
        {
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(insertSql, myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            Guid id = ((User)Session["User"]).UserID;
            myCommand.Parameters.AddWithValue("@UserId", id);
            myCommand.Parameters.AddWithValue("@Height", height);
            myCommand.Parameters.AddWithValue("@Weight", weight);
            myCommand.Parameters.AddWithValue("@Age", age);
            myCommand.Parameters.AddWithValue("@Gender", gender);

            //The following items need to be updated with information collected from the form
            myCommand.Parameters.AddWithValue("@SleepFlag", sleepFlag);
            myCommand.Parameters.AddWithValue("@SleepGoal", sleepGoal);
            myCommand.Parameters.AddWithValue("@HydrationFlag", hydrationFlag);
            myCommand.Parameters.AddWithValue("@HydrationGoal", hydrationGoal);
            //myCommand.Parameters.AddWithValue("@StressFlag", 0);
            //myCommand.Parameters.AddWithValue("@StressGoal", "");
            myCommand.Parameters.AddWithValue("@WeightManagementGoal", weightGoal);
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        Response.Redirect("~/");
    }

    protected void wzrdAdditional_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        //if(wzrdAdditional.ActiveStepIndex == 0)
        //{
        //    //check for checkboxes enabled
        //    wzrdAdditional.ActiveStepIndex = -1; //Set to index of conditional branch.

        //}
    }
}
