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
    private IDataManager exercisesManager;
    private Guid id;

    public WorkoutManager(SqlDataSource local, Guid userID, IDataManager eManager)
	{
        dsLocal = local;
        id = userID;
        exercisesManager = eManager;
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

        DataView results = (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);

        if (results != null)
        {
            int workoutID = Int32.Parse((string)results[0][0]);
            int totalCalories = (int)results[0][1]; 
            String gName = (string)results[0][2];
            DateTime time = (DateTime)results[0][3];
            List<Exercise> exercises = new List<Exercise>();
            String[] exerciseIDs = ((string)results[0][4]).Split(',');
            String[] durStrings = ((string)results[0][5]).Split(',');
            foreach (String eID in exerciseIDs)
            {
                exercises.Add((Exercise)exercisesManager.Get(eID));
            }
            List<int> durations = new List<int>();

            foreach (String dur in durStrings)
            {
                durations.Add(Int32.Parse(dur));
            }
            return new Workout(workoutID, this.id, totalCalories, gName, time, exercises, durations);
        }
        else
        {
            return null;
        }
    }

    object IDataManager.Add(object a)
    {
        if (a.GetType().Name.Equals("Workout"))
        {
            Workout temp = (Workout)a;
            String exercises = "";
            String durations = "";

            foreach (Exercise x in temp.Exercises)
            {
                exercises += x.ExerciseID + ",";
            }

            foreach (double d in temp.Durations)
            {
                durations += d + ",";
            }

            dsLocal.InsertCommand = "CreateWorkout";
            dsLocal.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
            dsLocal.InsertParameters.Add("userID", id.ToString());
            dsLocal.InsertParameters.Add("time", DbType.String, temp.Time.ToString("yyyy-MM-dd"));
            //dsLocal.InsertParameters[0].DefaultValue = id.ToString();
            dsLocal.InsertParameters[0].DefaultValue = temp.Name;
            
            //dsLocal.InsertParameters[2].DefaultValue = temp.Time.ToString("yyyy-MM-dd");
            dsLocal.InsertParameters[1].DefaultValue = "" + temp.TotalCalories;
            dsLocal.InsertParameters[2].DefaultValue = "" + exercises;
            dsLocal.InsertParameters[3].DefaultValue = "" + durations;
            dsLocal.Insert();
            return true;
        }
        return false;
    }

    object IDataManager.Update(object u, string id)
    {
        if (u.GetType().Name.Equals("Workout"))
        {
            Workout temp = (Workout)u;
            String exercises = "";
            String durations = "";
            dsLocal.UpdateCommand = "UpdateWorkout";
            dsLocal.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;

            foreach (Exercise e in temp.Exercises)
            {
                exercises += e.ExerciseID + ",";
            }

            foreach (Double d in temp.Durations)
            {
                durations += d + ",";
            }


            dsLocal.UpdateParameters[0].DefaultValue = temp.Name;
            dsLocal.UpdateParameters[1].DefaultValue = temp.TotalCalories.ToString();
            dsLocal.UpdateParameters[2].DefaultValue = exercises;
            dsLocal.UpdateParameters[3].DefaultValue = durations;
            dsLocal.UpdateParameters[4].DefaultValue = temp.WorkoutID.ToString();
            if (dsLocal.UpdateParameters.Count == 5)
            {
                dsLocal.UpdateParameters.Add("userID", this.id.ToString());
            }
            else
            {
                dsLocal.UpdateParameters[5].DefaultValue = this.id.ToString();
            }
            if (dsLocal.UpdateParameters.Count == 6)
            {
                dsLocal.UpdateParameters.Add("time", temp.Time.ToString("yyyy-MM-dd"));
            }
            else
            {
                dsLocal.UpdateParameters[6].DefaultValue = temp.Time.ToString("yyyy-MM-dd");
            }
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
        if (dsLocal.SelectParameters.Count == 0)//These are dynamically added because the wizard created params were incompatible
        {
            dsLocal.SelectParameters.Add("time", name);
            dsLocal.SelectParameters.Add("userId", id.ToString());
            Parameter param = new Parameter("return");
            param.Direction = ParameterDirection.ReturnValue;
            dsLocal.SelectParameters.Add(param);
        }
        else
        {
            dsLocal.SelectParameters[0].DefaultValue = name;
            dsLocal.SelectParameters[1].DefaultValue = id.ToString();
        }
        DataView x = (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);


        Dictionary<String, Workout> tempWorkouts = new Dictionary<string, Workout>();
        for (int i = 0; i < x.Table.Rows.Count; i++)
        {
            char[] delim = { ',' };
            List<Exercise> exercises = new List<Exercise>();
            string[] exerciseIDs = ((String)x.Table.Rows[i][5]).Split(delim, StringSplitOptions.RemoveEmptyEntries);
            string[] strDurations = ((String)x.Table.Rows[i][6]).Split(delim, StringSplitOptions.RemoveEmptyEntries);
            List<int> durations = new List<int>();

            foreach (String strDur in strDurations)
            {
                durations.Add(Int32.Parse(strDur));
            }

            foreach (String eID in exerciseIDs)
            {
                exercises.Add((Exercise)exercisesManager.Get(eID));
            }

            int exerID = (int)x.Table.Rows[i][0];
            Guid exerG = (Guid)x.Table.Rows[i][1];
            String exerName = (String)x.Table.Rows[i][2];
            int totalCalories = Convert.ToInt32((Double)x.Table.Rows[i][3]);
            DateTime exerDate = (DateTime)x.Table.Rows[i][4];

            Workout w = new Workout(exerID, exerG, totalCalories, exerName, exerDate, exercises, durations);

            tempWorkouts.Add(w.Name, w);
        }

        return tempWorkouts;
    }

    bool IDataManager.Close()
    {
        return true;
    }
}