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
                manager = new ExerciseManager(sqlDSLocal, sqlDSExercises, ((User)Session["User"]).UserID);
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
            workoutManager = new WorkoutManager(sqlDSLocalExercise, ((User)Session["User"]).UserID, manager);
            Session["WorkoutManager"] = workoutManager;
        }

        if(MultiView1.ActiveViewIndex == 0)
        {
            bindMeals();
        }

        if (MultiView1.ActiveViewIndex == 1)
        {
            //Do exercise stuff
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

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
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
                manager.searchByType(query);
                break;
            case "category":
                manager.searchByCategory(query);
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
        table.Columns.Add("MealName");
        
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
        //bindMeals();
    }
    protected void btnMoreExerciseDate_Click(object sender, EventArgs e)
    {
        currentDate.AddDays(1);
        lblFoodDate.Text = currentDate.ToShortDateString();
        Session["CurrentDate"] = currentDate;
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
                    // need to replace logic for exercise instead of food
                    gvResults.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "The exercise you are looking for does not exist. Do you want to create it?";
                    lbtnCreate.Visible = true;
                }
                else
                {
                    gvResults.Visible = true;
                    gvResults.DataSource = temp;
                    gvResults.DataBind();
                }
                break;
            case "type":
                manager.searchByType(query);
                break;
            case "category":
                manager.searchByCategory(query);
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
            mealManager.Add(temp);
            meals.Add(temp.Name, temp);
        }
        Session["CurrentMeal"] = temp;

        //Add logic to add meals to gridview
        DataTable table = buildMealsTable();
        Session["MealsTable"] = table;
        gvTodayMeals.DataSource = table;
        gvTodayMeals.DataBind();
    }
}