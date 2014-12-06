using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

/// <summary>
/// Summary description for FoodManager
/// </summary>
public class FoodManager : ISearchableDataManager
{
    private IDbCommand apiConnection;
    private IDbCommand localConnection;
    private SqlDataSource dsAPI;
    private SqlDataSource dsLocal;
    private Guid id;

	public FoodManager(SqlDataSource local, SqlDataSource API, Guid userID)
	{
        dsAPI = API;
        dsLocal = local;
        id = userID;
	}

    object ISearchableDataManager.searchByType(string type)
    {
        DataView x;
        if (type.Equals("restaurant"))
        {
            x = (DataView) dsAPI.Select(DataSourceSelectArguments.Empty);
            
        }
        else
        {
            dsLocal.SelectCommand = "QueryFoodsByType";

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
        dsLocal.SelectCommand = "QueryFoodCategories";
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
        if(g.GetType().Name.Equals("String"))
        {
            DataView results;
            SqlDataSource tempDS;
            String foodID = (string)g;
            bool isOfficialFood = (foodID.CompareTo("100000") >= 0);
            if (isOfficialFood)
            {
                tempDS = dsLocal;
                tempDS.SelectCommand = "GetFood";
                tempDS.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

                ParameterCollection tempCol = new ParameterCollection();
                foreach (Parameter p in tempDS.SelectParameters)
                {
                    tempCol.Add(p);
                }

                tempDS.SelectParameters.Clear();
                tempDS.SelectParameters.Add("FoodID", foodID);

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
                tempDS.SelectParameters[0].DefaultValue = foodID;
                results = (DataView)tempDS.Select(DataSourceSelectArguments.Empty);

            }
            //Re-write this section to account for the fact that we may not need to know which DB to search in
            
            //String[] items = ((String)g).Split(';');
            //if (items[0].Equals("local"))
            //{
            //    tempDS = dsLocal;

            //    tempDS.SelectCommand = "GetFood";
            //    tempDS.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            //}
            //else
            //{
            //    tempDS = dsAPI;
            //}
            //Make back up parameters
            
            
            
            int id = 0, calories = 0;
            String name = "", servingSize = "";
            bool isRestaurant = false;
            List<String> categories = new List<String>();
                       
            id = (int)results.Table.Rows[0][0];
            name = (String)results.Table.Rows[0][2];
            calories = (int)results.Table.Rows[0][3];
            if (!results.Table.Rows[0][4].Equals(DBNull.Value))
            {
                isRestaurant = (bool)results.Table.Rows[0][4];
            }
            if (!results.Table.Rows[0][5].Equals(DBNull.Value))
            {
                categories.AddRange(((String)results.Table.Rows[0][5]).Split(','));
            }
            servingSize = (String)results.Table.Rows[0][6];
            
            return new Food(id, calories, name, categories, isRestaurant, servingSize);
        }
        return null;
    }

    object IDataManager.Add(object a)
    {
        //Add logic to ensure stored procedure is correct
        if(a.GetType().Name.Equals("Food"))
        {
            Food temp = (Food)a;
            dsLocal.InsertParameters[0].DefaultValue = id.ToString();
            dsLocal.InsertParameters[1].DefaultValue = temp.Name;
            dsLocal.InsertParameters[2].DefaultValue = temp.CalorieIntake.ToString();
            dsLocal.InsertParameters[3].DefaultValue = temp.RestaurantFlag.ToString();
            dsLocal.InsertParameters[4].DefaultValue = temp.Category.ToString();
            dsLocal.InsertParameters[5].DefaultValue = temp.ServingSize;
            dsLocal.Insert();

            return true;
        }
        return false;
    }

    object IDataManager.Update(object u, string id)
    {
        //Food x;
        if (u != null && u.GetType().Name.Equals("Food"))
        {
            Food temp = (Food)u;
            dsLocal.UpdateParameters[0].DefaultValue = "";
            dsLocal.UpdateParameters[0].DefaultValue = "";
            dsLocal.UpdateParameters[0].DefaultValue = "";
            dsLocal.UpdateParameters[0].DefaultValue = "";
            dsLocal.UpdateParameters[0].DefaultValue = "";
            dsLocal.Update();

            return true;
        }
        return false;
    }

    object IDataManager.Remove(object r, string id)
    {
        if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
        {
            return false;
        }
        dsLocal.DeleteParameters[0].DefaultValue = id;
        int numAffected =  dsLocal.Delete();
        if(numAffected > 0)
        {
            return true;
        }
        return false;
    }

    object IDataManager.Search(string name)
    {
        dsAPI.SelectParameters[0].DefaultValue = name;
        DataView apiResult = (DataView)dsAPI.Select(DataSourceSelectArguments.Empty);
        dsLocal.SelectCommand = "QueryFoods";
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
