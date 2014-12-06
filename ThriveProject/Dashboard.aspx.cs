using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int[] foodArray =     { 1, 9, 8, 3 };
        int[] exerciseArray = { 2, 4, 1, 8, 6, 3, 2, 9, 1 };
        for (int i = 0; i < foodArray.Length; i++)
            Chart1.Series["Food"].Points.AddXY(0, foodArray[i]);Chart1.Series["Food"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.FastLine;
        

        for (int i = 0; i < exerciseArray.Length; i++)
            Chart1.Series["Exercise"].Points.AddXY(0, exerciseArray[i]);
        
    }
}
