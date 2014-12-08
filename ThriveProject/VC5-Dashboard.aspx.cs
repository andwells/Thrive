using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//TO DO:
//
//  Update range changes to stay on same day/year/month if valid to stay there.
//
//

public partial class vc2Dashboard : System.Web.UI.Page
{
    DateTime today = DateTime.Now;
    DateTime previous;
    DateTime previousEnd;

    int[] longMonth    = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
    int[] shortMonth   = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    int[] febLongMonth = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
    int[] febMonth     = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };

    //months with 31 days: 0-based--- 0, 2, 4, 6, 7, 9, 11      1-based--- 1, 3, 5, 7, 8, 10, 12
    //months with 30 days: 0-based--- 3, 5, 8, 10,              1-based--- 4, 6, 9, 11
    //feb with 28 days                 index == 1                              1-based--- 2

    protected void updateDayList()
    {
        
        //the only time we change previous is if we are now at a valid date.
        //we are at a valid date, IFF:
        //  feb selected ? 
        //      leapyear ? day must be less than or = 29 : day must be less than or = 28
        //  feb not selected ? 
        //      is short month selected? true iff:
        //          index == 3 || 5 || 8 || 10
        //          day must be less than or = 30
        //      not short month:
        //          must be at valid date.
        //

        bool changePrevious = true;
        if (ddlMonth.SelectedIndex == 1)
        {
            int leapyear = 0;
            if (ddlYear.SelectedIndex % 4 == 0)
            {
                leapyear = 1;
            }

            if (ddlDay.SelectedIndex > (27 + leapyear))
            {
                changePrevious = false;
            }
        }

        if(changePrevious == true)
        {
            if(ddlMonth.SelectedIndex == 3 ||
               ddlMonth.SelectedIndex == 5 ||
               ddlMonth.SelectedIndex == 8 ||
               ddlMonth.SelectedIndex == 10 )
            {
                if(ddlDay.SelectedIndex >= 30)
                {
                    changePrevious = false;
                }
            }
        }
        
        if(changePrevious == true)
        {
            Session["previous"] = new DateTime(ddlYear.SelectedIndex + 2012, ddlMonth.SelectedIndex + 1, ddlDay.SelectedIndex + 1);
        }
        
        if (ddlMonth.SelectedIndex == 1)
        {
            if ((ddlYear.SelectedIndex + 2012) % 4 == 0)
            {
                ddlDay.DataSource = febLongMonth;
            }
            else
                ddlDay.DataSource = febMonth;
        }
        else
        {
            if (ddlMonth.SelectedIndex == 0 ||
                ddlMonth.SelectedIndex == 2 ||
                ddlMonth.SelectedIndex == 4 ||
                ddlMonth.SelectedIndex == 6 ||
                ddlMonth.SelectedIndex == 7 ||
                ddlMonth.SelectedIndex == 9 ||
                ddlMonth.SelectedIndex == 11)
                {
                    ddlDay.DataSource = longMonth;
                }
            else
            {
                ddlDay.DataSource = shortMonth;
            }
        }
        ddlDay.DataBind();
    }

    protected void updateEndDayList()
    {
        //see logic in above updateDayList() for if we update previous

        bool changePrevious = true;
        if (ddlEndMonth.SelectedIndex == 1)
        {
            int leapyear = 0;
            if (ddlEndYear.SelectedIndex % 4 == 0)
            {
                leapyear = 1;
            }

            if (ddlEndDay.SelectedIndex > (27 + leapyear))
            {
                changePrevious = false;
            }
        }

        if (changePrevious == true)
        {
            if (ddlEndMonth.SelectedIndex == 3 ||
               ddlEndMonth.SelectedIndex == 5 ||
               ddlEndMonth.SelectedIndex == 8 ||
               ddlEndMonth.SelectedIndex == 10)
            {
                if (ddlEndDay.SelectedIndex >= 30)
                {
                    changePrevious = false;
                }
            }
        }

        if (changePrevious == true)
        {
            Session["previousEnd"] = new DateTime(ddlEndYear.SelectedIndex + 2012, ddlEndMonth.SelectedIndex + 1, ddlEndDay.SelectedIndex + 1);
        }


        if (ddlEndMonth.SelectedIndex == 1)
        {
            if ((ddlEndYear.SelectedIndex + 2012) % 4 == 0)
            {
                ddlEndDay.DataSource = febLongMonth;
            }
            else
                ddlEndDay.DataSource = febMonth;
        }
        else
        {
            if (ddlEndMonth.SelectedIndex == 0 ||
                ddlEndMonth.SelectedIndex == 2 ||
                ddlEndMonth.SelectedIndex == 4 ||
                ddlEndMonth.SelectedIndex == 6 ||
                ddlEndMonth.SelectedIndex == 7 ||
                ddlEndMonth.SelectedIndex == 9 ||
                ddlEndMonth.SelectedIndex == 11)
            {
                ddlEndDay.DataSource = longMonth;
            }
            else
            {
                ddlEndDay.DataSource = shortMonth;
            }
        }
        ddlEndDay.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ddlMonth.DataSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            ddlYear.DataSource  = new int[] { 2012, 2013, 2014 };
            ddlDay.DataSource   = longMonth;

            ddlDay.DataBind();
            ddlMonth.DataBind();
            ddlYear.DataBind();

            ddlMonth.SelectedIndex = today.Month - 1;
            ddlYear.SelectedIndex  = today.Year - 2012;
            ddlDay.SelectedIndex   = today.Day - 1;

            ddlEndMonth.DataSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            ddlEndYear.DataSource  = new int[] { 2012, 2013, 2014 };
            ddlEndDay.DataSource   = longMonth;

            ddlEndDay.DataBind();
            ddlEndMonth.DataBind();
            ddlEndYear.DataBind();

            ddlEndMonth.SelectedIndex = today.Month - 1;
            ddlEndYear.SelectedIndex  = today.Year - 2012;
            ddlEndDay.SelectedIndex   = today.Day - 1;

            previous = new DateTime(ddlYear.SelectedIndex + 2012, ddlMonth.SelectedIndex + 1, ddlDay.SelectedIndex + 1);
            previousEnd = new DateTime(ddlEndYear.SelectedIndex + 2012, ddlEndMonth.SelectedIndex + 1, ddlEndDay.SelectedIndex + 1);
            Session["previous"] = previous;
            Session["previousEnd"] = previous;

            updateDayList();
            updateEndDayList();
        }


        Chart1.ImageStorageMode = System.Web.UI.DataVisualization.Charting.ImageStorageMode.UseImageLocation;
        Chart1.Titles.Add("Caloric Intake, Exercise");
        
        Chart1.ChartAreas[0].AxisY.Maximum = 2750;
        Chart1.ChartAreas[0].AxisY.Minimum = 0;
        Chart1.ChartAreas[0].AxisX.Interval = 1;
        Chart1.ChartAreas[0].AxisY.Interval = 250;

        DateTime startDate = new DateTime(2014, 11, 30);
        DateTime maxDate = DateTime.Now;
        Chart1.ChartAreas[0].AxisX.Minimum = startDate.ToOADate();
        Chart1.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();

        int[] foodArray     = { 1400, 2300, 1987, 2100, 1930, 1740, 200, 300, 400, 500, 600, 700, 800, 900, 300, 500, 700, 500, 400, 100, 2000 };
        int[] exerciseArray = { 800, 1300, 700, 1700, 2250, 700, 650, 999, 1249, 900, 1400, 400, 600, 900, 400, 300, 500, 600, 300, 200, 1000 };
        int[] waterArray = { 12, 20, 19, 7, 13, 8, 17, 15, 16, 13, 9, 12, 8, 11 };
        int[] weightArray = { 120, 200, 190, 170, 130, 180, 170, 150, 160, 130, 190, 120, 180, 110 };

        for (int i = 0; i < foodArray.Length; i++)
            Chart1.Series["Food"].Points.AddXY(startDate.AddDays(i - 8), foodArray[i]);
        for (int i = 0; i < exerciseArray.Length; i++)
            Chart1.Series["Exercise"].Points.AddXY(startDate.AddDays(i - 8), exerciseArray[i]);
        for (int i = 0; i < waterArray.Length; i++)
            Chart1.Series["Water"].Points.AddXY(startDate.AddDays(i - 8), waterArray[i]);
        for (int i = 0; i < weightArray.Length; i++)
            Chart1.Series["Weight"].Points.AddXY(startDate.AddDays(i - 8), weightArray[i]);

        if (ddlChoice.SelectedIndex == 0)
        {
            setupFoodandExercise();
        }
        else if (ddlChoice.SelectedIndex == 1)
        {
            setupWater();
        }
        else if (ddlChoice.SelectedIndex == 2)
        {
            setupWeight();
        }

    }

    protected void bMonth_Click(object sender, EventArgs e)
    {
        changeView(today.AddMonths(-1), today);
        if (ddlChoice.SelectedIndex == 0)
            setupFoodandExercise();
        else if (ddlChoice.SelectedIndex == 1)
            setupWater();
        else if (ddlChoice.SelectedIndex == 2)
            setupWeight();
    }

    protected void bDay_Click(object sender, EventArgs e)
    {
        changeView(today, today.AddDays(1));
        if (ddlChoice.SelectedIndex == 0)
            setupFoodandExercise();
        else if (ddlChoice.SelectedIndex == 1)
            setupWater();
        else if (ddlChoice.SelectedIndex == 2)
            setupWeight();
    }


    protected void bWeek_Click(object sender, EventArgs e)
    {
        changeView(today.AddDays(-7), today);
        if (ddlChoice.SelectedIndex == 0)
            setupFoodandExercise();
        else if (ddlChoice.SelectedIndex == 1)
            setupWater();
        else if (ddlChoice.SelectedIndex == 2)
            setupWeight();
    }

    protected void changeView(DateTime start, DateTime end)
    {
        if (end.Subtract(start) < System.TimeSpan.FromDays(0))
            return;

        int interval  = 0;
        int singleDay = 0;

        if (end.Subtract(start) <= System.TimeSpan.FromDays(1))
        {
            interval  = 1;
            singleDay = 1;
        }
        else if (end.Subtract(start) <= System.TimeSpan.FromDays(7))
        {
            interval = 1;
        }
        else if (end.Subtract(start) > System.TimeSpan.FromDays(7) && end.Subtract(start) <= System.TimeSpan.FromDays(31))
            interval = 7;
        else if (end.Subtract(start) > System.TimeSpan.FromDays(31) && end.Subtract(start) <= System.TimeSpan.FromDays(180))
            interval = 14;
        else
            interval = 30;

        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype1 = System.Web.UI.DataVisualization.Charting.SeriesChartType.FastLine;
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype2 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype3 = System.Web.UI.DataVisualization.Charting.SeriesChartType.StepLine;
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype4 = System.Web.UI.DataVisualization.Charting.SeriesChartType.SplineArea;

        if (singleDay == 1)
        {
            ctype1 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            ctype2 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            ctype3 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
        }

        Chart1.Series["Food"].ChartType     = ctype1;
        Chart1.Series["Exercise"].ChartType = ctype2;
        Chart1.Series["Water"].ChartType = ctype3;
        Chart1.Series["Weight"].ChartType = ctype4;

        Chart1.ChartAreas[0].AxisX.Minimum = start.ToOADate();
        Chart1.ChartAreas[0].AxisX.Maximum = end.ToOADate();

        Chart1.ChartAreas[0].AxisX.Interval = interval;

        if (interval == 30)
        {
            Chart1.Series[0].BorderWidth = 2;
            Chart1.Series[1].BorderWidth = 2;
            Chart1.Series[2].BorderWidth = 2;
        }
        else if(interval == 14)
        {
            //
        }
        else
        {
            Chart1.Series[0].BorderWidth = 3;
            Chart1.Series[1].BorderWidth = 3;
            Chart1.Series[2].BorderWidth = 3;
        }

        if (ddlChoice.SelectedIndex == 0)
        {
            setupFoodandExercise();
        }
        else if (ddlChoice.SelectedIndex == 1)
        {
            setupWater();
        }
        else if (ddlChoice.SelectedIndex == 2)
        {
            setupWeight();
        }
        else
        {

        }

    }


    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(2014,1,1);
        try
        {
            date = new DateTime(ddlYear.SelectedIndex + 2012, ddlMonth.SelectedIndex + 1, ddlDay.SelectedIndex + 1);
        }
        catch (ArgumentOutOfRangeException a)
        {
            updateDayList();
        }

        if(needUpdate(date, 1))
            updateDayList();
    }
    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(2014, 1, 1);
        try
        {
            date = new DateTime(ddlYear.SelectedIndex + 2012, ddlMonth.SelectedIndex + 1, ddlDay.SelectedIndex + 1);
        }
        catch (ArgumentOutOfRangeException a)
        {
            updateDayList();
        }

        if (needUpdate(date, 1))
            updateDayList();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(2014, 1, 1);
        try
        {
            date = new DateTime(ddlYear.SelectedIndex + 2012, ddlMonth.SelectedIndex + 1, ddlDay.SelectedIndex + 1);
        }
        catch (ArgumentOutOfRangeException a)
        {
            updateDayList();
        }

        if (needUpdate(date, 1))
            updateDayList();
    }


    protected void ddlEndYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(2014, 1, 1);
        try
        {
            date = new DateTime(ddlEndYear.SelectedIndex + 2012, ddlEndMonth.SelectedIndex + 1, ddlEndDay.SelectedIndex + 1);
        }
        catch (ArgumentOutOfRangeException a)
        {
            updateEndDayList();
        }

        if (needUpdate(date, 1))
            updateEndDayList();
    }
    protected void ddlEndMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(2014, 1, 1);
        try
        {
            date = new DateTime(ddlEndYear.SelectedIndex + 2012, ddlEndMonth.SelectedIndex + 1, ddlEndDay.SelectedIndex + 1);
        }
        catch (ArgumentOutOfRangeException a)
        {
            updateEndDayList();
        }

        if (needUpdate(date, 1))
            updateEndDayList();
    }
    protected void ddlEndDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(2014, 1, 1);
        try
        {
            date = new DateTime(ddlEndYear.SelectedIndex + 2012, ddlEndMonth.SelectedIndex + 1, ddlEndDay.SelectedIndex + 1);
        }
        catch (ArgumentOutOfRangeException a)
        {
            updateEndDayList();
        }

        if (needUpdate(date, 1))
            updateEndDayList();
    }


    protected void bViewRange_Click(object sender, EventArgs e)
    {
        DateTime start = new DateTime(ddlYear.SelectedIndex + 2012, ddlMonth.SelectedIndex + 1, ddlDay.SelectedIndex + 1);
        DateTime end = new DateTime(ddlEndYear.SelectedIndex + 2012, ddlEndMonth.SelectedIndex + 1, ddlEndDay.SelectedIndex + 1);
        changeView(start, end);
    }

    protected bool needUpdate(DateTime date, int start)
    {
        previous    = (DateTime) Session["previous"];
        previousEnd = (DateTime) Session["previousEnd"];

        if (start == 1)
        {
            if (date.Month == previous.Month)
                return false;
            else if (date.Month == 2 || previous.Month == 2)
                return true;
            else
            {
                if (date.Month == 1 ||
                   date.Month == 3 ||
                   date.Month == 5 ||
                   date.Month == 7 ||
                   date.Month == 8 ||
                   date.Month == 10 ||
                   date.Month == 12)
                {
                    if (previous.Month == 0 ||
                       previous.Month == 2 ||
                       previous.Month == 4 ||
                       previous.Month == 6 ||
                       previous.Month == 9 ||
                       previous.Month == 11)
                    {
                        return true;
                    }
                    return false;
                }
                if (date.Month == 0 ||
                   date.Month == 2 ||
                   date.Month == 4 ||
                   date.Month == 6 ||
                   date.Month == 9 ||
                   date.Month == 11)
                {
                    if (previous.Month == 1 ||
                       previous.Month == 3 ||
                       previous.Month == 5 ||
                       previous.Month == 7 ||
                       previous.Month == 8 ||
                       previous.Month == 10 ||
                       previous.Month == 12)
                    {
                        return true;
                    }
                    return false;
                }
            }

        }
        else
        {
            if (date.Month == previousEnd.Month)
                return false;
            else
            {
                if (date.Month == 1 ||
                   date.Month == 3  ||
                   date.Month == 5  ||
                   date.Month == 7  ||
                   date.Month == 8  ||
                   date.Month == 10 ||
                   date.Month == 12)
                    {
                        if (previousEnd.Month == 0 ||
                           previousEnd.Month == 2  ||
                           previousEnd.Month == 4  ||
                           previousEnd.Month == 6  ||
                           previousEnd.Month == 9  ||
                           previousEnd.Month == 11)
                            {
                                return true;
                            }
                        return false;
                    }
                if (date.Month == 0 ||
                   date.Month == 2  ||
                   date.Month == 4  ||
                   date.Month == 6  ||
                   date.Month == 9  ||
                   date.Month == 11)
                    {
                        if (previousEnd.Month == 1 ||
                           previousEnd.Month == 3  ||
                           previousEnd.Month == 5  ||
                           previousEnd.Month == 7  ||
                           previousEnd.Month == 8  ||
                           previousEnd.Month == 10 ||
                           previousEnd.Month == 12)
                            {
                                return true;
                            }
                        return false;
                    }
            }
        }

        return true;
    }


    protected void ddlChoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlChoice.SelectedIndex == 0)
        {
            setupFoodandExercise();
        }
        else if (ddlChoice.SelectedIndex == 1)
        {
            setupWater();
        }
        else if(ddlChoice.SelectedIndex == 2)
        {
            setupWeight();
        }
    }

    protected void setupFoodandExercise()
    {
        Chart1.Titles.RemoveAt(0);
        Chart1.Titles.Add("Caloric Intake, Exercise");

        Chart1.Series["Food"].Enabled = true;
        Chart1.Series["Exercise"].Enabled = true;
        Chart1.Series["Water"].Enabled = false;
        Chart1.Series["Weight"].Enabled = false;

        Chart1.ChartAreas[0].AxisY.Maximum = 3000;
        Chart1.ChartAreas[0].AxisY.Minimum = 0;
        Chart1.ChartAreas[0].AxisY.Interval = 250;
    }

    protected void setupWater()
    {
        Chart1.Titles.RemoveAt(0);
        Chart1.Titles.Add("Water Consumption");

        Chart1.Series["Water"].Enabled = true;
        Chart1.Series["Food"].Enabled = false;
        Chart1.Series["Exercise"].Enabled = false;
        Chart1.Series["Weight"].Enabled = false;

        Chart1.ChartAreas[0].AxisY.Maximum = 25;
        Chart1.ChartAreas[0].AxisY.Minimum = 0;
        Chart1.ChartAreas[0].AxisY.Interval = 5;
        Chart1.Series["Water"].BorderWidth = 3;
    }

    protected void setupWeight()
    {
        Chart1.Titles.RemoveAt(0);
        Chart1.Titles.Add("Weight");

        Chart1.Series["Water"].Enabled = false;
        Chart1.Series["Food"].Enabled = false;
        Chart1.Series["Exercise"].Enabled = false;
        Chart1.Series["Weight"].Enabled = true;

        Chart1.ChartAreas[0].AxisY.Maximum = 300;
        Chart1.ChartAreas[0].AxisY.Minimum = 100;
        Chart1.ChartAreas[0].AxisY.Interval = 25;
        Chart1.Series["Weight"].BorderWidth = 3;
    }
}
