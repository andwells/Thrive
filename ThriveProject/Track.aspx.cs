using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Track : System.Web.UI.Page
{
    private ISearchableDataManager manager;
    private IDataManager mealManager;
    private IDataManager workoutManager;
    private Dictionary<String, Meal> meals;
    private Dictionary<String, Workout> workouts;
    private DataTable table;
    private DateTime currentDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region"Load objects from session"
        if (Session["CurrentDate"] == null)
        {
            currentDate = DateTime.Today;
            Session["CurrentDate"] = currentDate;
        }
        else
        {
            currentDate = (DateTime)Session["CurrentDate"];
        }
        if(currentDate.CompareTo(DateTime.Today) == 0)
        {
            lblFoodDate.Text = "Today";
            lblExerciseDate.Text = "Today";
            btnMoreFoodDate.Enabled = false;
        }
        else
        {
            lblFoodDate.Text = currentDate.ToShortDateString();
            lblExerciseDate.Text = currentDate.ToShortDateString();
        }
        
        if(Session["User"] == null)
        {
            Response.Redirect("~/account/login.aspx");
        }
        if (Session["Manager"] != null)
        {
            manager = (ISearchableDataManager)Session["Manager"];
        }
        else
        {
            if (MultiView1.ActiveViewIndex == 0)
            {
                manager = new FoodManager(sqlDSLocal, sqlDSAccess, ((User)Session["User"]).UserID);
            }
            else
            {
                //Adjust sqlDSLocal
                manager = new ExerciseManager(sqlDSLocalExercise, sqlDSExercises, ((User)Session["User"]).UserID);
            }
            Session["Manager"] = manager;
        }

        if (Session["MealManager"] != null)
        {
            mealManager = (IDataManager)Session["MealManager"];
        }
        else
        {
            mealManager = new MealManager(dsMeals, ((User)Session["User"]).UserID, manager);
            Session["MealManager"] = mealManager;
        }

        if (Session["WorkoutManager"] != null)
        {
            workoutManager = (IDataManager)Session["WorkoutManager"];
        }
        else
        {
            workoutManager = new WorkoutManager(sqlDSWorkouts, ((User)Session["User"]).UserID, manager);
            Session["WorkoutManager"] = workoutManager;
        }

        if(MultiView1.ActiveViewIndex == 0)
        {
            bindMeals();
        }

        if (MultiView1.ActiveViewIndex == 1)
        {
            bindWorkouts();
        }

        #endregion
    }
    private void bindMeals()
    {
        if (Session["Meals"] == null)
        {
            //mealManager = new MealManager(dsMeals, ((User)Session["User"]).UserID, manager);

            this.meals = (Dictionary<String, Meal>)mealManager.Search(currentDate.ToString("yyyy-MM-dd"));

            DataTable table = buildMealsTable();
            gvTodayMeals.DataSource = table;
            gvTodayMeals.DataBind();

            Session["MealsTable"] = table;

            Session["Meals"] = this.meals;
            Session["MealManager"] = mealManager;
        }
        else
        {
            meals = (Dictionary<string, Meal>)Session["Meals"];
            DataTable table = buildMealsTable();
            gvTodayMeals.DataSource = table;
            gvTodayMeals.DataBind();
        }
    }
    private void bindWorkouts()
    {
        if (Session["Workouts"] == null)
        {
            this.workouts = (Dictionary<string, Workout>)workoutManager.Search(currentDate.ToString("yyyy-MM-dd"));
            DataTable table = buildWorkoutsTable();
            gvTodayWorkouts.DataSource = table;
            gvTodayWorkouts.DataBind();

            Session["WorkoutsTable"] = table;
            Session["Workouts"] = this.workouts;
            Session["WorkoutManager"] = workoutManager;
        }
        else
        {
            workouts = (Dictionary<string, Workout>)Session["Workouts"];
            DataTable table = buildWorkoutsTable();
            gvTodayWorkouts.DataSource = table;
            gvTodayWorkouts.DataBind();
        }
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
        if (MultiView1.ActiveViewIndex == 0)
        {
            manager = new FoodManager(sqlDSLocal, sqlDSAccess, ((User)Session["User"]).UserID);
            lblCurrentAction.Text = "Track Food";
            gvResults.DataSource = null;
            gvResults.DataBind();
            bindMeals();
        }
        else
        {
            manager = new ExerciseManager(sqlDSLocalExercise, sqlDSExercises, ((User)Session["User"]).UserID);
            lblCurrentAction.Text = "Track Exercise";
            gvExerciseResults.DataSource = null;
            gvExerciseResults.DataBind();
            bindWorkouts();
        }

        //Clears any saved search results
        Session["DataView"] = null;
        Session["Manager"] = manager;
    }
    protected void btnSearchFood_Click(object sender, EventArgs e)
    {
        pnlCreateFood.Visible = false;
        pnlAddFood.Visible = false;

        //Clears values of controls
        tbFoodName.Text = "";
        tbCalories.Text = "";
        tbCategories.Text = "";
        cbIsRestaurant.Checked = false;
        tbServingSize.Text = "";
        tbServingsEaten.Text = "";
        tbMealEatenIn.Text = "";


        String type = ddlSearchType.SelectedValue;
        String query = tbFood.Text;
        switch (type)
        {
            case "name":
                DataView temp = (DataView)manager.Search(query);
                Session["Dataview"] = temp;

                if (temp == null || temp.Table.Rows.Count == 0)
                {
                    gvResults.Visible = false;
                    pnlFoodError.Visible = true;
                }
                else
                {
                    gvResults.Visible = true;
                    gvResults.DataSource = temp;
                    gvResults.DataBind();
                }
                break;
            case "type":
                //manager.searchByType(query);
                break;
            case "category":
                //manager.searchByCategory(query);
                break;
            default:
                return;
        }
    }
    protected void gvResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvResults.PageIndex = e.NewPageIndex;
        if (Session["Dataview"] != null)
        {
            gvResults.DataSource = (DataView)Session["Dataview"];
        }
        else
        {
            //Response.Write("Error binding DataView");
        }
        gvResults.DataBind();
    }
    protected void gvResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*The reason that there are no checks to see if the DataViewExists in Session
        is because the DataView has to exists for the SelectedIndexChanged Event to fire*/
        DataRow row = ((DataView)Session["DataView"]).Table.Rows[gvResults.SelectedRow.DataItemIndex];

        int id, calories;
        String name;
        List<String> categories = new List<String>();
        bool isRestaurant = false;

        id = (int) row[0];
        calories = (int) row[3];

        if (row.Table.Columns.Count > 5)
        {
            name = (string)row[2];
            isRestaurant = (!row[4].Equals(DBNull.Value)) ? (bool)row[4] : false;
            if (!row[5].Equals(DBNull.Value))
            {
                categories.AddRange(((String)row[5]).Split(','));
            }
        }
        else
        {
            name = (string)row[1];
        }
        
        Food temp = new Food(id, calories, name, categories, isRestaurant, "");
        Session["SelectedFood"] = temp;

        lblFoodDesc.Text = temp.Name;
        //This works
        gvResults.Visible = false;
        pnlAddFood.Visible = true;
    }
    protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count == 4)
        {
            e.Row.Cells[1].Visible = false;
        }

        //if (e.Row.Cells.Count > 6)
        //{
        //    e.Row.Cells[5].Visible = false;
        //    e.Row.Cells[6].Visible = false;
        //}
    }
    protected void btnAddFood_Click(object sender, EventArgs e)
    {
        double servings = Double.Parse(tbServings.Text);
        String mealName = tbEnterMealName.Text;
        Meal temp;
        
        
        //add check to make sure meal for certain day with that name doesn't already exist
        if (meals.ContainsKey(mealName))
        {
            temp = meals[mealName];
            temp.addFood((Food)Session["SelectedFood"], servings);
            mealManager.Update(temp, "id");
            meals[temp.Name] = temp;
        }
        else
        {
            temp = new Meal(mealName, currentDate);
            temp.addFood((Food)Session["SelectedFood"], servings);
            int newID = (int) mealManager.Add(temp);
            temp = new Meal(newID, ((User)Session["User"]).UserID, temp.TotalCalories, temp.Name, temp.Time, temp.Foods, temp.Servings);
            meals.Add(temp.Name, temp);
        }
        Session["CurrentMeal"] = temp;

        //Add logic to add meals to gridview
        DataTable table = buildMealsTable();
        Session["MealsTable"] = table;
        gvTodayMeals.DataSource = table;
        gvTodayMeals.DataBind();
        pnlAddFood.Visible = false;
    }
    private DataTable buildMealsTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Food Name");
        table.Columns.Add("Servings");
        table.Columns.Add("Total Calories");
        table.Columns.Add("Meal Name");
        
        foreach(Meal m in meals.Values)
        {
            object[] rowVals;
            for (int i = 0; i < m.Foods.Count; i++)
            {
                rowVals = new object[4];
                rowVals[0] = m.Foods[i].Name;
                rowVals[1] = m.Servings[i];
                rowVals[2] = m.Foods[i].CalorieIntake * m.Servings[i];
                rowVals[3] = m.Name;
                table.Rows.Add(rowVals);
            }       
        }
        table.AcceptChanges();
        return table;
    }
    private DataTable buildWorkoutsTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Exercise Name");
        table.Columns.Add("Hours Exercised");
        table.Columns.Add("Minutes Exercised");
        table.Columns.Add("Calories Burned");
        table.Columns.Add("Workout Name");

        foreach (Workout w in workouts.Values)
        {
            object[] rowVals;
            for (int i = 0; i < w.Exercises.Count; i++)
            {
                rowVals = new object[5];
                rowVals[0] = w.Exercises[i].Name;
                int time = (int)w.Durations[i];
                table.Rows.Add(rowVals);
            }
        }
        table.AcceptChanges();
        return table;
    }
    protected void btnLessFoodDate_Click(object sender, EventArgs e)
    {
        //Enable moving forward through days
        btnMoreFoodDate.Enabled = true;

        currentDate = currentDate.AddDays(-1);
        lblFoodDate.Text = currentDate.ToShortDateString();
        Session["CurrentDate"] = currentDate;
        Session["Meals"] = null;
        bindMeals();
    }
    protected void btnMoreFoodDate_Click(object sender, EventArgs e)
    {
        if((currentDate.AddDays(1)).CompareTo(DateTime.Today) > 0)
        {
            //If adding a day would go past today's date, do nothing
            return;
        }

        currentDate = currentDate.AddDays(1);
        if (currentDate.CompareTo(DateTime.Today) == 0)
        {
            lblFoodDate.Text = "Today";
            btnMoreFoodDate.Enabled = false;
        }
        else
        {
            lblFoodDate.Text = currentDate.ToShortDateString();
        }

        Session["CurrentDate"] = currentDate;

        //Clear the meals from today, so that bindMeals() can fetch meals for currentDate
        Session["Meals"] = null;
        bindMeals();
    }
    protected void btnLessExerciseDate_Click(object sender, EventArgs e)
    {
        //Enable moving forward through days
        btnMoreExerciseDate.Enabled = true;

        currentDate = currentDate.AddDays(-1);
        lblExerciseDate.Text = currentDate.ToShortDateString();
        Session["CurrentDate"] = currentDate;
        Session["Workouts"] = null;
        bindWorkouts();
    }
    protected void btnMoreExerciseDate_Click(object sender, EventArgs e)
    {
        if ((currentDate.AddDays(1)).CompareTo(DateTime.Today) > 0)
        {
            //If adding a day would go past today's date, do nothing
            return;
        }

        currentDate = currentDate.AddDays(1);
        if (currentDate.CompareTo(DateTime.Today) == 0)
        {
            lblFoodDate.Text = "Today";
            btnMoreFoodDate.Enabled = false;
        }
        else
        {
            lblFoodDate.Text = currentDate.ToShortDateString();
        }

        Session["CurrentDate"] = currentDate;

        //Clear the workouts from today, so that bindWorkouts() can fetch workouts for currentDate
        Session["Workouts"] = null;
        bindWorkouts();
    }
    protected void btnSearchExercise_Click(object sender, EventArgs e)
    {
        String type = ddlSearchTypeExercise.SelectedValue;
        String query = tbExercise.Text;
        switch (type)
        {
            case "name":
                DataView temp = (DataView)manager.Search(query);
                Session["Dataview"] = temp;

                if (temp == null || temp.Table.Rows.Count == 0)
                {
                    gvExerciseResults.Visible = false;
                    pnlExerciseError.Visible = true;
                }
                else
                {
                    gvExerciseResults.Visible = true;
                    gvExerciseResults.DataSource = temp;
                    gvExerciseResults.DataBind();
                }
                break;
            case "type":
               // manager.searchByType(query);
                break;
            case "category":
                //manager.searchByCategory(query);
                break;
            default:
                return;
        }
    }
    protected void lbtnCreate_Click(object sender, EventArgs e)
    {
        pnlCreateFood.Visible = true;
        pnlFoodError.Visible = false;
    }
    protected void btnCreateFood_Click(object sender, EventArgs e)
    {
        Food x;
        String name = tbFoodName.Text;
        int calories = Int32.Parse(tbCalories.Text);
        List<String> catorgies = new List<String>();
        char[] delim = {','};
        catorgies.AddRange(tbCategories.Text.Split(delim, StringSplitOptions.RemoveEmptyEntries));
        bool isRestaurant = cbIsRestaurant.Checked;
        String servSize = tbServingSize.Text;

        x = new Food(0, calories, name, catorgies, isRestaurant, servSize);
        int newID = (int)manager.Add(x);
        x = new Food(newID, x.CalorieIntake, x.Name, x.Category, x.RestaurantFlag, x.ServingSize);

        double servings = Double.Parse(tbServingsEaten.Text);
        String mealName = tbMealEatenIn.Text;
        Meal temp;


        //add check to make sure meal for certain day with that name doesn't already exist
        if (meals.ContainsKey(mealName))
        {
            temp = meals[mealName];
            temp.addFood(x, servings);
            mealManager.Update(temp, "id");
            meals[temp.Name] = temp;
        }
        else
        {
            temp = new Meal(mealName, currentDate);
            temp.addFood(x, servings);
            int anID = (int)mealManager.Add(temp);
            temp = new Meal(anID, ((User)Session["User"]).UserID, temp.TotalCalories, temp.Name, temp.Time, temp.Foods, temp.Servings);
            meals.Add(temp.Name, temp);
        }
        Session["CurrentMeal"] = temp;

        //Add logic to add meals to gridview
        DataTable table = buildMealsTable();
        Session["MealsTable"] = table;
        gvTodayMeals.DataSource = table;
        gvTodayMeals.DataBind();
    }
    protected void gvExerciseResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvExerciseResults.PageIndex = e.NewPageIndex;
        if (Session["Dataview"] != null)
        {
            gvExerciseResults.DataSource = (DataView)Session["Dataview"];
        }
        else
        {
            //Response.Write("Error binding DataView");
        }
        gvExerciseResults.DataBind();
    }
    protected void gvExerciseResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*The reason that there are no checks to see if the DataViewExists in Session
        is because the DataView has to exists for the SelectedIndexChanged Event to fire*/
        DataRow row = ((DataView)Session["DataView"]).Table.Rows[gvExerciseResults.SelectedRow.DataItemIndex];

        int id;
        double caloriesBurned;
        String name, type = "";
        List<String> categories = new List<String>();

        id = (short)row[0];
        

        if (row.Table.Columns.Count > 5)
        {
            name = (string)row[2];
            caloriesBurned = (int)row[3];
            type = (!row[4].Equals(DBNull.Value)) ? (String)row[4] : "";
            if (!row[5].Equals(DBNull.Value))
            {
                categories.AddRange(((String)row[5]).Split(','));
            }
        }
        else
        {
            
            caloriesBurned = (double)row[1];
            categories.Add((string)row[2]);
            name = (string)row[3];
            caloriesBurned = (caloriesBurned * 3.5 * (((User)Session["User"]).Weight / 2.2046) / 200);
        }

        Exercise temp = new Exercise(id, (int)caloriesBurned, name, categories, type);
        
        Session["SelectedExercise"] = temp;

        lblExerciseDesc.Text = temp.Name;
        gvExerciseResults.Visible = false;
        pnlAddExercise.Visible = true;
    }
    protected void gvExerciseResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void lbtnCreateExercise_Click(object sender, EventArgs e)
    {
        pnlCreateExercise.Visible = true;
        pnlExerciseError.Visible = false;
    }
    protected void btnCreateExercise_Click(object sender, EventArgs e)
    {
        Exercise x;
        String name = tbSetExerciseName.Text;
        int caloriesBurned = Int32.Parse(tbSetCaloriesBurned.Text);
        List<String> catorgies = new List<String>();
        char[] delim = { ',' };
        catorgies.AddRange(tbSetExerciseCategories.Text.Split(delim, StringSplitOptions.RemoveEmptyEntries));
        String type = tbSetExerciseType.Text;
        int time = (Int32.Parse(tbSetExerciseHours.Text) * 60) + Int32.Parse(tbSetExerciseMinutes.Text);

        x = new Exercise(0, caloriesBurned, name, catorgies, type);
        int newID = (int)manager.Add(x);
        x = new Exercise(newID, x.CaloriesBurned, x.Name, x.Category, x.Type);

        String workoutName = tbSetWorkoutName.Text;
        Workout temp;


        //add check to make sure meal for certain day with that name doesn't already exist
        if (workouts.ContainsKey(workoutName))
        {
            temp = workouts[workoutName];
            temp.addExercise(x, time);
            workoutManager.Update(temp, "id");
            workouts[temp.Name] = temp;
        }
        else
        {
            temp = new Workout(workoutName, currentDate);
            temp.addExercise(x, time);
            int anID = (int)workoutManager.Add(temp);
            temp = new Workout(anID, ((User)Session["User"]).UserID, temp.TotalCalories, temp.Name, temp.Time, temp.Exercises, temp.Durations);
            workouts.Add(temp.Name, temp);
        }
        Session["CurrentWorkout"] = temp;

        //Add logic to add meals to gridview
        DataTable table = buildWorkoutsTable();
        Session["WorkoutsTable"] = table;
        gvTodayWorkouts.DataSource = table;
        gvTodayWorkouts.DataBind();
    }
    protected void btnAddExercise_Click(object sender, EventArgs e)
    {
        int hours = Convert.ToInt32(tbExerciseHours.Text);
        int mins = Convert.ToInt32(tbExerciseMinutes.Text);
        int time = hours * 60 + mins;
        String workoutName = tbEnterMealName.Text;
        Workout temp;
        
        //checks to make sure workout for certain day with that name doesn't already exist
        if (workouts.ContainsKey(workoutName))
        {
            temp = workouts[workoutName];
            temp.addExercise((Exercise)Session["SelectedExercise"], time);
            workoutManager.Update(temp, "id");
            workouts[temp.Name] = temp;
        }
        else
        {
            temp = new Workout(workoutName, currentDate);
            temp.addExercise((Exercise)Session["SelectedExercise"], time);
            int newID = (int)workoutManager.Add(temp);
            temp = new Workout(newID, ((User)Session["User"]).UserID, temp.TotalCalories, temp.Name, temp.Time, temp.Exercises, temp.Durations);
            workouts.Add(temp.Name, temp);
        }
        Session["CurrentMeal"] = temp;

        //Add logic to add meals to gridview
        DataTable table = buildWorkoutsTable();
        Session["WorkoutsTable"] = table;

        gvTodayWorkouts.DataSource = table;
        gvTodayWorkouts.DataBind();
        pnlAddExercise.Visible = false;
    }
}