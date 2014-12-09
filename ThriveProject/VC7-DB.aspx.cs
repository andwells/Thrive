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


    int[] foodArray = { 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000,
                    3000, 3000, 3000, 3000, 3000, 2997, 2997, 2996, 2996, 3000,
                    2996, 2996, 2996, 2995, 2995, 2994, 2994, 2994, 2994, 2994,
                    2990, 2990, 2990, 2990, 2990, 2989, 2989, 2989, 2985, 2985,
                    2985, 2985, 2985, 2985, 2985, 2980, 2980, 2980, 2980, 2981,
                    2981, 2980, 2981, 2975, 2975, 2975, 2981, 2975, 2965, 2965,
                    2965, 2965, 2965, 2964, 2960, 2960, 2960, 2965, 2965, 2965,
                    2965, 2965, 2950, 2950, 2900, 2900, 2989, 2988, 2985, 2985,
                    2980, 2980, 2980, 2980, 2980, 2980, 2973, 2973, 2973, 2988,
                    2988, 2970, 2969, 2968, 2966, 2940, 2944, 2936, 2936, 2936,
                    2920, 2919, 2910, 2910, 2900, 2900, 2900, 2899, 2865, 2865,
                    2862, 2862, 2862, 2862, 2854, 2854, 2854, 2854, 2855, 2855,
                    2855, 2855, 2855, 2855, 2855, 2855, 2855, 2855, 2855, 2855,
                    2850, 2850, 2850, 2850, 2850, 2850, 2850, 2850, 2850, 2850,
                    2850, 2846, 2847, 2846, 2856, 2856, 2846, 2846, 2842, 2843,
                    2843, 2840, 2844, 2840, 2833, 2833, 2833, 2833, 2833, 2836,
                    2836, 2836, 2820, 2820, 2820, 2820, 2820, 2820, 2814, 2814, 
                    2810, 2810, 2810, 2810, 2810, 2810, 2805, 2805, 2805, 2805,
                    2794, 2794, 2794, 2784, 2784, 2784, 2784, 2999, 2999, 2999,
                    2780, 2780, 2780, 2780, 2780, 2780, 2781, 2785, 2785, 2779,
                    2770, 2770, 2770, 2770, 2765, 2765, 2765, 2765, 2763, 2763,
                    2763, 2762, 2760, 2760, 2760, 2758, 2758, 2758, 2750, 2750,
                    2745, 2745, 2745, 2745, 2740, 2741, 2741, 2741, 2740, 2740, 
                    2740, 2740, 2740, 2740, 2740, 2738, 2738, 2738, 2738, 2745,
                    2745, 2745, 2754, 2740, 2741, 2740, 2736, 2736, 2736, 2736,
                    2736, 2736, 2736, 2736, 2735, 2735, 2734, 2734, 2730, 2730,
                    2730, 2730, 2756, 2756, 2756, 2756, 2729, 2729, 2729, 2725,
                    2724, 2724, 2724, 2724, 2724, 2724, 2726, 2726, 2726, 2726,
                    2724, 2724, 2724, 2724, 2721, 2724, 2724, 2721, 2721, 2721, 
                    2721, 2721, 2721, 2721, 2721, 2718, 2718, 2720, 2715, 2728,
                    2755, 2754, 2722, 2722, 2720, 2715, 2715, 2715, 2710, 2710,
                    2710, 2710, 2710, 2710, 2710, 2710, 2710, 2710, 2710, 2710,
                    2700, 2700, 2700, 2700, 2700, 2700, 2700, 2700, 2700, 2700, 
                    2765, 2765, 2764, 2760, 2760, 2760, 2765, 2761, 2760, 2760, 
                    2700, 2700, 2700, 2700, 2700, 2700, 2765, 2695, 2695, 2695, 
                    2694, 2694, 2690, 2660, 2653, 2653, 2655, 2653, 2650, 2650,
                    2645, 2640, 2640, 2633, 2630};
                    
                    
    int[] exerciseArray = {300, 300, 300, 300, 300, 300, 300, 300, 300, 300,
                       300, 300, 300, 300, 300, 300, 300, 300, 300, 300,
                       305, 305, 307, 306, 300, 305, 305, 307, 307, 308,
                       310, 310, 310, 310, 310, 310, 310, 310, 310, 310,
                       311, 311, 311, 312, 312, 312, 312, 312, 312, 315, 
                       320, 320, 320, 320, 320, 320, 320, 320, 320, 325,
                       330, 331, 325, 330, 334, 336, 320, 334, 335, 340, 
                       325, 356, 325, 345, 340, 400, 400, 400, 400, 400,
                       475, 475, 475, 500, 500, 500, 512, 514, 514, 530,
                       540, 535, 530, 525, 560, 575, 550, 570, 570, 565,
                       530, 530, 530, 550, 550, 550, 575, 575, 578, 580, 
                       590, 590, 590, 600, 600, 600, 650, 606, 645, 650,
                       660, 665 ,670, 675, 675, 675, 680, 540, 540, 680,
                       680, 680, 680, 680, 685, 682, 681, 680, 640, 655,
                       680, 680, 679, 685, 695, 688, 687, 689, 690, 691,
                       685, 685, 685, 685, 690, 690, 690, 690, 690, 695,
                       696, 697, 700, 720, 720, 720, 725, 750, 760, 770, 
                       779, 779, 785, 790, 800, 800, 800, 800, 800, 845,
                       850, 850, 850, 870, 870, 890, 890, 890, 900, 900, 
                      1000, 1000, 1000, 1000, 1000, 1100, 1100, 1100, 1100, 1100,
                      1121, 1121, 1124, 1125, 1121, 1111, 1115, 1143, 1156, 1156,
                      1160, 1165, 1165, 1167, 1175, 1175, 1175, 1182, 1182, 1182, 
                      1190, 1190, 1190, 1185, 1185, 1185, 1192, 1190, 1200, 1200, 
                      1200, 1200, 1225, 1225, 1225, 1235, 1235, 1241, 1241, 1251, 
                      1251, 1251, 1251, 1265, 1265, 1265, 1272, 1270, 1265, 1275, 
                      1278, 1278, 1280, 1281, 1285, 1285, 1283, 1286, 1290, 1288,
                      1275, 1278, 1285, 1285, 1285, 1291, 1290, 1290, 1288, 1288, 
                      1292, 1292, 1295, 1295, 1295, 1299, 1300, 1300, 1300, 1310, 
                      1311, 1321, 1321, 1322, 1325, 1325, 1326, 1337, 1335, 1338, 
                      1340, 1345, 1345, 1355, 1356, 1356, 1365, 1360, 1360, 1365,
                      1365, 1360, 1368, 1368, 1372, 1372, 1375, 1385, 1380, 1392,
                      1377, 1375, 1345, 1391, 1365, 1375, 1377, 1375, 1376, 1385,
                      1386, 1386, 1386, 1375, 1388, 1388, 1388, 1390, 1390, 1390, 
                      1391, 1391, 1391, 1391, 1391, 1395, 1395, 1389, 1389, 1390, 
                      1393, 1393, 1396, 1393, 1395, 1394, 1395, 1396, 1397, 1397,
                      1398, 1398, 1398, 1399, 1400, 1400, 1400, 1410, 1410, 1410,
                      1412, 1400, 1398, 1398, 1410};


    int[] waterArray = { 16, 16, 16, 16, 20, 20, 21, 21, 20, 20,
                     16, 16, 16, 17, 18, 18, 18, 19, 16, 17,
                     17, 17, 18, 19, 18, 17, 21, 22, 22, 22, 
                     20, 22, 22, 24, 18, 16, 20, 25, 26, 19, 
                     19, 19, 18, 17, 17, 18, 19, 20, 22, 21, 
                     25, 21, 17, 18, 20, 21, 22, 20, 19, 18,
                     21, 25, 26, 27, 20, 22, 18, 29, 24, 24,
                     22, 25, 20, 19, 18, 17, 16, 18, 19, 20, 
                     22, 24, 22, 25, 26, 24, 24, 25, 29, 27, 
                     29, 29, 30, 30, 30, 30, 31, 31, 32, 33,
                     35, 35, 36, 37, 38, 39, 34, 40, 45, 45, 
                     34, 40, 45, 45, 45, 45, 43, 42, 40, 41, 
                     41, 39, 38, 37, 36, 35, 34, 32, 50, 51,
                     55, 55, 52, 58, 59, 56, 57, 65, 45, 45,
                     51, 52, 55, 53, 45, 49, 57, 56, 53, 52,
                     50, 51, 52, 53, 58, 57, 56, 54, 54, 54, 
                     58, 57, 57, 56, 59, 60, 61, 61, 62, 63,
                     62, 63, 65, 64, 65, 67, 62, 65, 65, 64, 
                     64, 64, 65, 66, 67, 67, 67, 68, 64, 68, 
                     55, 52, 67, 68, 69, 70, 72, 72, 72, 89, 
                     75, 65, 75, 76, 77, 78, 65, 61, 60, 78,
                     77, 75, 76, 82, 85, 88, 86, 83, 82, 81, 
                     80, 81, 88, 82, 96, 85, 45, 45, 60, 75,
                     70, 71, 72, 72, 72, 72, 75, 74, 74, 62, 
                     70, 70, 71, 72, 75, 76, 77, 77, 79, 80, 
                     81, 81, 81, 82, 85, 85, 85, 85, 87, 87,
                     85, 84, 86, 83, 82, 84, 85, 87, 88, 86,
                     85, 83, 84, 86, 87, 88, 84, 81, 86, 83,
                     90, 91, 91, 91, 95, 94, 95, 95, 97, 98,
                     99, 95, 96, 97, 98, 97, 98, 99, 96, 93, 
                     100, 85, 86, 87, 99, 87, 87, 87, 100, 99,
                     110, 110, 103, 102, 103, 103, 100, 110, 108, 103,
                     103, 103, 105, 105, 105, 106, 107, 107, 108, 109, 
                     110, 102, 110, 102, 110, 111, 111, 111, 112, 113, 
                     113, 113, 114, 115, 115, 116, 118, 118, 118, 120,
                     120, 119, 118, 111, 112, 113, 115, 116, 115, 117,
                     120, 119, 115, 120, 121, 120, 111, 119, 118, 119, 
                     119, 120, 121, 125, 120};
    int[] weightArray = { 230, 228, 231, 227, 228, 224, 221, 226, 223, 220, 219, 216, 214, 215, 219, 213, 210, 205, 207, 209, 215, 219, 225, 227, 233, 231, 235, 240 };


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

            //Here is where chris's logic will goez.

            int indexMonth = today.Month - 1;
            int indexYear = today.Year - 2012;
            int indexDay = today.Day - 1;


            // check over this logic
            if ((indexDay -= 7) < 0)
            {
                if (--indexMonth < 0)
                {
                    indexYear--;
                }
            }

            // index variables used here and only here
            ddlMonth.SelectedIndex = indexMonth;
            ddlYear.SelectedIndex = indexYear;
            ddlDay.SelectedIndex = indexDay;


            /*
             * 
             * 
             *  NEED TO DO ASASPPPPPPP
             *  
             * Also, check above for more logic
             * 
             * Also, ASAP---
             *      For long views, change data set to exclude most points.
             */

            //note that this logic will not worry about if our year goes back too far,
            //because we will start at year 2014, at most it could get pushed back to 2013
            //and 2013 is valid. Only problem would be from 2012 to 2011 since
            //2011 is not in the dropdown box.

            

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

        
        for (int i = 0; i < foodArray.Length; i++)
            Chart1.Series["Food"].Points.AddXY(startDate.AddDays(i - 364), foodArray[i]);

        for (int i = 0; i < exerciseArray.Length; i++)
            Chart1.Series["Exercise"].Points.AddXY(startDate.AddDays(i - 364), exerciseArray[i]);

        for (int i = 0; i < waterArray.Length; i++)
            Chart1.Series["Water"].Points.AddXY(startDate.AddDays(i - 364), waterArray[i]);

        for (int i = 0; i < weightArray.Length; i++)
            Chart1.Series["Weight"].Points.AddXY(startDate.AddDays(i - 364), weightArray[i]);


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
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype3 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
        System.Web.UI.DataVisualization.Charting.SeriesChartType ctype4 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Area;
        

        if (singleDay == 1)
        {
            ctype1 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            ctype2 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            ctype3 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
            ctype4 = System.Web.UI.DataVisualization.Charting.SeriesChartType.Column;
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
            Chart1.Series[3].BorderWidth = 3;
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

        //changing is not working

        //current array is the array that the graph is currently opperating on
        int[] currentArray;

        if (ddlChoice.SelectedIndex == 0 || ddlChoice.SelectedIndex == 1)
        {
            currentArray = new int[foodArray.Length + exerciseArray.Length];
            foodArray.CopyTo(currentArray, 0);
            exerciseArray.CopyTo(currentArray, foodArray.Length);
        }
        else if (ddlChoice.SelectedIndex == 2)
        {
            currentArray = waterArray;
        }
        else
        {
            currentArray = weightArray;
        }

        int startX = (int)Chart1.ChartAreas[0].AxisX.Minimum;
        int endX = (int)Chart1.ChartAreas[0].AxisX.Maximum;

        if ((endX + 1) >= currentArray.Length)
        {
            endX = currentArray.Length - 1;
        }

        // find the min and max values of the current array
        int min, max;
        min = max = currentArray[0];

        for (int i = startX; startX <= endX; startX += interval)
        {
            if (currentArray[i] < min)
            {
                min = currentArray[i];
            }
            if (currentArray[i] > max)
            {
                max = currentArray[i];
            }
        }

        // set the graph y-axis min and max to be relative to the min and max of the data
        min -= (int)(min * .10);
        min -= min % 10;
        max += (int)(max * .10);
        max += 10 - (max % 10);

        Chart1.ChartAreas[0].AxisY.Minimum = min;
        Chart1.ChartAreas[0].AxisY.Maximum = max;



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
        Chart1.ChartAreas[0].AxisY.Minimum = 250;
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

        Chart1.ChartAreas[0].AxisY.Maximum = 120;
        Chart1.ChartAreas[0].AxisY.Minimum = 15;
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
