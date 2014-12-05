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
public class WorkoutManager : IDataManager
{
    private IDbCommand apiConnection;
    private IDbCommand localConnection;
    private SqlDataSource dsLocal;
    private Guid id;

	public WorkoutManager(SqlDataSource local, Guid userID)
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
        dsLocal.SelectCommand = "GetWorkout";
        dsLocal.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        dsLocal.SelectParameters[0].DefaultValue = id;
        
        IDataReader results = (IDataReader)dsLocal.Select(DataSourceSelectArguments.Empty);
        
        results.Read();
        //Incomplete; may not need to be used

        return null;
    }

    object IDataManager.Add(object a)
    {
        if (a.GetType().Name.Equals("Workout"))
        {
            Workout temp = (Workout)a;
            String exercises = "";
            String servings = "";

            foreach(Exercise x in temp.Exercise)
            {
                exercise+= x.getExerciseID() + ",";
            }

            foreach (double d in temp.Servings)
            {
                servings += d + ",";
            }

            dsLocal.InsertCommand = "CreateWorkout";
            dsLocal.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
            dsLocal.InsertParameters[0].DefaultValue = id.ToString();
            dsLocal.InsertParameters[1].DefaultValue = temp.Name;
            dsLocal.InsertParameters[2].DefaultValue = temp.Time.ToShortDateString();
            dsLocal.InsertParameters[3].DefaultValue = "" + temp.TotalCalories;
            dsLocal.InsertParameters[4].DefaultValue = "" + servings;
            dsLocal.Insert();
            return true;
        }
        return false;
    }

    object IDataManager.Update(object u, string id)
    {
        if(u.GetType().Name.Equals("Workout"))
        {
            Workout temp = (Workout)u;
            String exercises = "";
            String servings = "";
            dsLocal.UpdateCommand = "UpdateWorkout";
            dsLocal.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;

            foreach (Food f in temp.Foods)
            {
                foods += f.getFoodID() + ",";
            }

            foreach (Double d in servings)
            {
                servings += d + ",";
            }
            
            dsLocal.UpdateParameters[0].DefaultValue = "id"; //Not working
            dsLocal.UpdateParameters[1].DefaultValue = "" + id;
            dsLocal.UpdateParameters[2].DefaultValue = temp.Name;
            dsLocal.UpdateParameters[3].DefaultValue = temp.Time.ToShortDateString();
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
        dsLocal.SelectCommand = "QueryWorkouts";
        dsLocal.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        dsLocal.SelectParameters[0].DefaultValue = name;
        dsLocal.SelectParameters.Add("userID", "" + id);
        return (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);
    }

    bool IDataManager.Close()
    {
        return true;
    }
}
