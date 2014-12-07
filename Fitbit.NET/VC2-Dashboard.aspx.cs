using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    DateTime today = DateTime.Now;
    int[] longMonth    = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
    int[] shortMonth   = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    int[] febMonth     = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };
    int[] febLongMonth = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };


    protected void updateDayList()
    {
        if (ddlMonth.SelectedIndex == 1)
        {
            if (ddlYear.SelectedIndex % 4 == 0)
            {
                ddlDay.DataSource = febLongMonth;
            }
            else
                ddlDay.DataSource = febMonth;
        }
        else
        {
            if(ddlYear.SelectedIndex == 0 ||
               ddlYear.SelectedIndex == 2 ||
               ddlYear.SelectedIndex == 4 ||
               ddlYear.SelectedIndex == 6 ||
               ddlYear.SelectedIndex == 7 ||
               ddlYear.SelectedIndex == 9 ||
               ddlYear.SelectedIndex == 11)
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

    protected void Page_Load(object sender, EventArgs e)
    {
        ddlMonth.DataSource = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        ddlYear.DataSource = new int[] { 2012, 2013, 2014 };
        
        ddlMonth.DataBind();
        ddlYear.DataBind();


        updateDayList();

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

        int[] foodArray      = { 1400, 2300, 1987, 2100, 1930, 1740, 200, 300, 400,  500, 600,  700, 800, 900, 300, 500, 700, 500, 400, 100, 2000 };
        int[] exerciseArray =  { 800,  1300, 700,  1700, 2250, 700,  650, 999, 1249, 900, 1400, 400, 600, 900, 400, 300, 500, 600, 300, 200, 1000 };

        for (int i = 0; i < foodArray.Length; i++)
            Chart1.Series["Food"].Points.AddXY(startDate.AddDays(i - 8), foodArray[i]);

        for (int i = 0; i < exerciseArray.Length; i++)
            Chart1.Series["Exercise"].Points.AddXY(startDate.AddDays(i - 8), exerciseArray[i]);
        
    }

    protected void bMonth_Click(object sender, EventArgs e)
    {
        changeView(today.AddMonths(-1), today);
    }

    protected void bDay_Click(object sender, EventArgs e)
    {
        changeView(today, today.AddDays(1));
    }


    protected void bWeek_Click(object sender, EventArgs e)
    {
        changeView(today.AddDays(-7), today);
    }

    protected void changeView(DateTime start, DateTime end)
    {
        int interval = 0;
        int singleDay = 0;

        if (end.Subtract(start) <= System.TimeSpan.FromDays(1))
        {
            interval = 1;
            singleDay = 1;
        }
        else if (end.Subtract(start) <= System.TimeSpan.FromDays(7))
        {
            interval = 1;
        }
        else if (end.Subtract(start) > System.TimeSpan.FromDays(7) && end.Subtract(start) <= System.TimeSpan.FromDays(31))
            interval = 7;
        else if (end.Subtract(start) > System.TimeSpan.FromDays(31) && end.Subtract(start) <= System.TimeSpan.FromDays(220))
            interval = 14;
        else
            interval = 30;

        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype1 = System.Web.UI.DataVisualization.Charting.SeriesChartType.FastLine;
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype2 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;

        if (singleDay == 1)
        {
            ctype1 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            ctype2 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
        }

        Chart1.Series["Food"].ChartType = ctype1;
        Chart1.Series["Exercise"].ChartType = ctype2;
        Chart1.ChartAreas[0].AxisX.Minimum = start.ToOADate();
        Chart1.ChartAreas[0].AxisX.Maximum = end.ToOADate();

        Chart1.ChartAreas[0].AxisX.Interval = interval;
        
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(ddlYear.SelectedIndex + 2012, ddlMonth.SelectedIndex + 1, ddlDay.SelectedIndex + 1);
        updateDayList();
    }
    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(ddlYear.SelectedIndex + 1, ddlMonth.SelectedIndex + 2012, ddlDay.SelectedIndex + 1);
        updateDayList();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime date = new DateTime(ddlYear.SelectedIndex + 1, ddlMonth.SelectedIndex + 2012, ddlDay.SelectedIndex + 1);
        updateDayList();
    }
}
