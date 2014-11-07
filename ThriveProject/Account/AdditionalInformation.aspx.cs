using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_AdditionalInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["UserString"] == null)
        {
            Response.Redirect("~/");
        }
    }
    protected void wzrdAdditional_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        //Insert into table
        String feet = lbFeet.SelectedValue;
        String inches = lbInches.SelectedValue;
        String weight = tbWeight.Text;
        String age = tbAge.Text;
        String gender = ddlGender.SelectedValue;
        String activity = rblActivityLevel.SelectedValue;
        
    }

    protected void wzrdAdditional_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if(wzrdAdditional.ActiveStepIndex == 0)
        {
            //check for checkboxes enabled
            wzrdAdditional.ActiveStepIndex = -1; //Set to index of conditional branch.

        }
    }
}