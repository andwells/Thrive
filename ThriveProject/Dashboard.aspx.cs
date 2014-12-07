using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    DateTime today = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
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
        changeView(0, today.AddMonths(-1), today);
    }

    protected void bDay_Click(object sender, EventArgs e)
    {
        changeView(1, today, today.AddDays(1));
    }


    protected void bWeek_Click(object sender, EventArgs e)
    {
        changeView(0, today.AddDays(-7), today);
    }

    protected void changeView(int display, DateTime start, DateTime end)
    {
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype1 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype2 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;

        if (display == 0)
        {
            ctype1 = System.Web.UI.DataVisualization.Charting.SeriesChartType.FastLine;
            ctype2 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Spline;
        }

        Chart1.Series["Food"].ChartType = ctype1;
        Chart1.Series["Exercise"].ChartType = ctype2;
        Chart1.ChartAreas[0].AxisX.Minimum = start.ToOADate();
        Chart1.ChartAreas[0].AxisX.Maximum = end.ToOADate();
        
    }
}
