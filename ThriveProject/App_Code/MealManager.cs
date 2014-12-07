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
    private IDataManager foodsManager;
    private Guid id;

	public MealManager(SqlDataSource local, Guid userID, IDataManager fManager)
	{
        dsLocal = local;
        id = userID;
        foodsManager = fManager;
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
        
        DataView results = (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);

        if (results != null)
        {
            int mealID = Int32.Parse((string)results[0][0]);
            String gName = (string)results[0][2];
            List<Food> foods = new List<Food>();
            String[] foodIDs = ((string)results[0][3]).Split(',');

            foreach (String fID in foodIDs)
            {
                foods.Add((Food)foodsManager.Get(fID));
            }
            List<double> servings = new List<double>();
            int totalCalories = Int32.Parse((String)results[0][4]);
            DateTime time = DateTime.Parse((String)results[0][5]);

            
            return new Meal(mealID, this.id, totalCalories, gName, time, foods, servings);
        }
        else
        {
            return null;
        }
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
            dsLocal.InsertParameters.Add("time", DbType.String, temp.Time.ToString("yyyy-MM-dd"));
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


            dsLocal.UpdateParameters[0].DefaultValue = temp.Name;
            dsLocal.UpdateParameters[1].DefaultValue = temp.TotalCalories.ToString();
            dsLocal.UpdateParameters[2].DefaultValue = foods;
            dsLocal.UpdateParameters[3].DefaultValue = servings;
            dsLocal.UpdateParameters[4].DefaultValue = temp.MealID.ToString();
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
        dsLocal.SelectCommand = "QueryMeals";
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
        DataView x =  (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);


        Dictionary<String, Meal> tempMeals = new Dictionary<string, Meal>();
        for (int i = 0; i < x.Table.Rows.Count; i++ )
        {
            char[] delim = {','};
            List<Food> foods = new List<Food>();
            string[] foodIDs = ((String)x.Table.Rows[i][5]).Split(delim, StringSplitOptions.RemoveEmptyEntries);
            string[] strServings = ((String)x.Table.Rows[i][6]).Split(delim, StringSplitOptions.RemoveEmptyEntries);
            List<double> serv = new List<double>();

            foreach(String strServ in strServings)
            {
                serv.Add(Double.Parse(strServ));
            }

            foreach(String fID in foodIDs)
            {
                foods.Add((Food)foodsManager.Get(fID));
            }
            
            int mID = (int)x.Table.Rows[i][0];
            Guid mG = (Guid)x.Table.Rows[i][1];
            String mName = (String)x.Table.Rows[i][2];
            int totalCalories = Convert.ToInt32((Double)x.Table.Rows[i][3]);
            DateTime mDate = (DateTime)x.Table.Rows[i][4];

            Meal m = new Meal(mID, mG, totalCalories, mName, mDate, foods, serv);
              
            tempMeals.Add(m.Name, m);
        }
        
        return tempMeals;
    }

    bool IDataManager.Close()
    {
        return true;
    }
}