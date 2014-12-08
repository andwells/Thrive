using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

/// <summary>
/// Summary description for ExerciseManager
/// </summary>
public class ExerciseManager : ISearchableDataManager
{
	private IDbCommand apiConnection;
    private IDbCommand localConnection;
	private SqlDataSource dsAPI;
    private SqlDataSource dsLocal;
    private Guid id;
    	
	public ExerciseManager(SqlDataSource local, SqlDataSource API, Guid userID)
	{
		dsAPI = API;
        dsLocal = local;
        id = userID;
	}

    object ISearchableDataManager.searchByType(string type)
    {
    	DataView x;
        if (type.Equals("aerobic"))
        {
            x = (DataView) dsAPI.Select(DataSourceSelectArguments.Empty);
            
        }
        else
        {
            dsLocal.SelectCommand = "QueryExercisesByType";

            x = (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);
        }
        
        return x;
    }

    object ISearchableDataManager.searchByCategory(string category)
    {
    	if (string.IsNullOrEmpty(category) || string.IsNullOrWhiteSpace(category))
    	{
    		return false;
    	}
    	dsLocal.SelectCommand = "QueryExerciseCategories";
        dsLocal.SelectParameters[0].DefaultValue = category;
        return (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    /// 
    //WARNING!! This method is broken! Needs to be re-written to adjust for checking the range of the ID!
    object IDataManager.Get(object g)
    {
        if (g.GetType().Name.Equals("String"))
        {
            DataView results;
            SqlDataSource tempDS;
            String exerciseID = (string)g;
            int eIDD = Int32.Parse(exerciseID);
            bool isNotOfficialExercise = (eIDD >= 100000);
            if (isNotOfficialExercise)
            {
                tempDS = dsLocal;
                tempDS.SelectCommand = "GetExercise";
                tempDS.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

                ParameterCollection tempCol = new ParameterCollection();
                foreach (Parameter p in tempDS.SelectParameters)
                {
                    tempCol.Add(p);
                }

                tempDS.SelectParameters.Clear();
                tempDS.SelectParameters.Add("ExerciseID", exerciseID);

                results = (DataView)tempDS.Select(DataSourceSelectArguments.Empty);

                tempDS.SelectParameters.RemoveAt(0);

                foreach (Parameter p in tempCol)
                {
                    tempDS.SelectParameters.Add(p);
                }
            }
            else
            {
                tempDS = dsAPI;
                String backQ = tempDS.SelectCommand;
                tempDS.SelectCommand = "SELECT Exercises.ID AS exerciseID, Exercises.Metric AS Metric, Exercises.Category AS Categories, Exercises.Description AS Name, Exercises.Type AS Type FROM Exercises WHERE (((Exercises.ID)=[?])) ORDER BY Exercises.Description;";

                ParameterCollection tempCol = new ParameterCollection();
                foreach (Parameter p in tempDS.SelectParameters)
                {
                    tempCol.Add(p);
                }

                tempDS.SelectParameters.Clear();
                tempDS.SelectParameters.Add("ExerciseID", exerciseID);

                results = (DataView)tempDS.Select(DataSourceSelectArguments.Empty);

                tempDS.SelectParameters.RemoveAt(0);

                foreach (Parameter p in tempCol)
                {
                    tempDS.SelectParameters.Add(p);
                }
                tempDS.SelectCommand = backQ;
            }

            int id = 0;
            double metric = 0.0; // variable to compute calories burned
            List<String> categories = new List<String>();
            String name = ""; // == description
            int caloriesBurned = 0;
            String type = ""; // (an)aerobic

            id = (int)results.Table.Rows[0][0];
            if (isNotOfficialExercise)
            {
                name = (String)results.Table.Rows[0][2];
                caloriesBurned = (int)results.Table.Rows[0][3];
                if ((bool)results.Table.Rows[0][4] == true)
                {
                    type = "Aerobic";
                }
                else
                {
                    type = "Anaerobic";
                }
                categories.AddRange(((String)results.Table.Rows[0][5]).Split(','));
            }
            else
            {
                name = (String)results.Table.Rows[0][3];
                metric = (int)results.Table.Rows[0][1];
                // TODO: Pull user weight. For now, substituting average weight of 150 lbs and minutes of 30
                // (metric * 3.5 * (weight/2.2046) / 200) * minutes burned = total calories burned
                int weight = 150;
                int minutes = 30;
                caloriesBurned = (int)((metric * 3.5 * (weight / 2.2046) / 200) * minutes);
                categories.AddRange(((String)results.Table.Rows[0][2]).Split(','));
                type = (String)results.Table.Rows[0][4];
            }
            return new Exercise(id, caloriesBurned, name, categories, type);
        }
        return null;
    }

    object IDataManager.Add(object a)
    {
        //Add logic to ensure stored procedure is correct
        if (a.GetType().Name.Equals("Exercise"))
        {
            Exercise temp = (Exercise)a;
            dsLocal.InsertParameters[0].DefaultValue = id.ToString();
            dsLocal.InsertParameters[1].DefaultValue = temp.Name;
            dsLocal.InsertParameters[2].DefaultValue = temp.CaloriesBurned.ToString();
            bool isAerobic = true;
            if (temp.Type.CompareTo("Anaerobic") == 0)
            {
                isAerobic = false;
            }
            dsLocal.InsertParameters[3].DefaultValue = isAerobic.ToString();
            dsLocal.InsertParameters[4].DefaultValue = temp.Category.ToString();
            dsLocal.Insert();
            //Add logic to get return value of stored proc

            using (IDbCommand com = ConnectionFactory.GetDBConnection("local"))
            {
                com.CommandText = "SELECT MAX(Exercises.exerciseID) FROM Exercises";
                com.Connection.Open();
                using (IDataReader i = com.ExecuteReader())
                {
                    i.Read();
                    return i.GetInt32(0);
                }
            }
        }
        return -1;
    }

    object IDataManager.Update(object u, string id)
    {
        //Exercise x;
        if (u != null && u.GetType().Name.Equals("Exercise"))
        {
            Exercise temp = (Exercise)u;
            dsLocal.UpdateParameters[0].DefaultValue = id.ToString(); ;
            dsLocal.UpdateParameters[1].DefaultValue = temp.Name;
            dsLocal.UpdateParameters[2].DefaultValue = temp.CaloriesBurned.ToString();
            bool isAerobic = true;
            if (temp.Type.CompareTo("Anaerobic") == 0)
            {
                isAerobic = false;
            }
            dsLocal.UpdateParameters[3].DefaultValue = isAerobic.ToString();
            dsLocal.UpdateParameters[4].DefaultValue = temp.Category.ToString();
            dsLocal.Update();

            return true;
        }
        return false;
    }

    object IDataManager.Remove(object r, string id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return false;
        }
        dsLocal.DeleteParameters[0].DefaultValue = id;
        int numAffected = dsLocal.Delete();
        if (numAffected > 0)
        {
            return true;
        }
        return false;
    }

    object IDataManager.Search(string name)
    {
        dsAPI.SelectParameters[0].DefaultValue = name;
        DataView apiResult = (DataView)dsAPI.Select(DataSourceSelectArguments.Empty);
        dsLocal.SelectCommand = "QueryExercises";
        dsLocal.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        dsLocal.SelectParameters[0].DefaultValue = name;
        DataView result2 = (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);
        if (result2 != null)
        {
            if (result2.Table.Rows.Count != 0)
            {
                result2.Table.Merge(apiResult.Table, true, MissingSchemaAction.Add);
                return result2;
            }
        }
        return apiResult;
    }

    bool IDataManager.Close()
    {
        if (apiConnection != null) { apiConnection.Connection.Close(); apiConnection = null; }
        if (localConnection != null) { localConnection.Connection.Close(); localConnection = null; }
        dsAPI = dsLocal = null;
        return true;
    }
}