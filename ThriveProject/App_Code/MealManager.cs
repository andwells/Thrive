using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

/// <summary>
/// Summary description for MealManager
/// </summary>
public class MealManager : IDataManager
{
    private IDbCommand apiConnection;
    private IDbCommand localConnection;
    private SqlDataSource dsLocal;
    private Guid id;

	public MealManager(SqlDataSource local, Guid userID)
	{
        dsLocal = local;
        id = userID;
	}

    object IDataManager.Get(object g)
    {
        String name = g.GetType().Name;
        String id;
        if(!name.Equals("String") && !name.Equals("Int32"))
        {
            return null;
        }

        if (name.Equals("Int32"))
        {
            id = (int)g + "";
        }
        else
        {
            id = (String)g;
        }

        //Change to appropriate Stored Procedure
        dsLocal.SelectCommand = "GetMeal";
        dsLocal.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        dsLocal.SelectParameters[0].DefaultValue = id;
        
        IDataReader results = (IDataReader)dsLocal.Select(DataSourceSelectArguments.Empty);
        
        results.Read();
        //Incomplete; may not need to be used

        return null;
    }

    object IDataManager.Add(object a)
    {
        if (a.GetType().Name.Equals("Meal"))
        {
            Meal temp = (Meal)a;
            String foods = "";
            String servings = "";

            foreach(Food x in temp.Foods)
            {
                foods+= x.getFoodID() + ",";
            }

            foreach (double d in temp.Servings)
            {
                servings += d + ",";
            }

            dsLocal.InsertCommand = "CreateMeal";
            dsLocal.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
            dsLocal.InsertParameters.Add("userID", id.ToString());
            //dsLocal.InsertParameters[0].DefaultValue = id.ToString();
            dsLocal.InsertParameters[0].DefaultValue = temp.Name;
            dsLocal.InsertParameters.Add("time", temp.Time.ToString("yyyy-MM-dd"));
            //dsLocal.InsertParameters[2].DefaultValue = temp.Time.ToString("yyyy-MM-dd");
            dsLocal.InsertParameters[1].DefaultValue = "" + temp.TotalCalories;
            dsLocal.InsertParameters[2].DefaultValue = "" + foods;
            dsLocal.InsertParameters[3].DefaultValue = "" + servings;
            dsLocal.Insert();
            return true;
        }
        return false;
    }

    object IDataManager.Update(object u, string id)
    {
        if(u.GetType().Name.Equals("Meal"))
        {
            Meal temp = (Meal)u;
            String foods = "";
            String servings = "";
            dsLocal.UpdateCommand = "UpdateMeal";
            dsLocal.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;

            foreach (Food f in temp.Foods)
            {
                foods += f.getFoodID() + ",";
            }

            foreach (Double d in servings)
            {
                servings += d + ",";
            }

            dsLocal.UpdateParameters.Add("userID", id.ToString());
            dsLocal.UpdateParameters[1].DefaultValue = "" + id;
            dsLocal.UpdateParameters[2].DefaultValue = temp.Name;
            dsLocal.UpdateParameters.Add("time", temp.Time.ToShortDateString());
            //dsLocal.UpdateParameters[3].DefaultValue = temp.Time.ToShortDateString();
            dsLocal.UpdateParameters[4].DefaultValue = "" + temp.TotalCalories;
            dsLocal.UpdateParameters[5].DefaultValue = foods;
            dsLocal.UpdateParameters[6].DefaultValue = servings;
            dsLocal.Update();
            return true;
        }
        return false;
    }

    object IDataManager.Remove(object r, string id)
    {
        throw new NotImplementedException();
    }

    object IDataManager.Search(string name)
    {
        dsLocal.SelectCommand = "QueryMeals";
        dsLocal.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        dsLocal.SelectParameters.Add("time", name);
        dsLocal.SelectParameters.Add("userId", id.ToString());
        return (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);
    }

    bool IDataManager.Close()
    {
        return true;
    }
}