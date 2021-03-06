﻿using System;
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
            //Add line to check for correct params
            x = (DataView) dsAPI.Select(DataSourceSelectArguments.Empty);
            
        }
        else
        {
            //Add line to check for correct params
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
    object IDataManager.Get(object g)
    {
        if(g.GetType().Name.Equals("String"))
        {
            DataView results;
            SqlDataSource tempDS;
            String foodID = (string)g;
            int fIDD = Int32.Parse(foodID);
            bool isNotOfficialFood = (fIDD >= 100000);
            if (isNotOfficialFood)
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
                String backQ = tempDS.SelectCommand;
                tempDS.SelectCommand = "SELECT ABBREV.NDB_No AS FoodId, ABBREV.Shrt_Desc AS Name, ABBREV.GmWt_Desc1 AS ServingSize, ABBREV.Energ_Kcal AS Calories FROM ABBREV WHERE (((ABBREV.NDB_No)=[?])) ORDER BY ABBREV.Shrt_Desc;";

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
                tempDS.SelectCommand = backQ;
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
            if (isNotOfficialFood)
            {
                name = (String)results.Table.Rows[0][2];
                servingSize = (String)results.Table.Rows[0][6];
            }
            else
            {
                name = (String)results.Table.Rows[0][1];
            }
            calories = (int)results.Table.Rows[0][3];
            if (isNotOfficialFood && !results.Table.Rows[0][4].Equals(DBNull.Value))
            {
                isRestaurant = (bool)results.Table.Rows[0][4];
            }
            if (isNotOfficialFood && !results.Table.Rows[0][5].Equals(DBNull.Value))
            {
                categories.AddRange(((String)results.Table.Rows[0][5]).Split(','));
            }
            else
            {
                categories.Add("");
            }
            if(isNotOfficialFood)
            {
                
            }
            
            return new Food(id, calories, name, categories, isRestaurant, servingSize);
        }
        return null;
    }

    object IDataManager.Add(object a)
    {
        //Add logic to ensure stored procedure is correct
        if(a.GetType().Name.Equals("Food"))
        {
            String strCat = "";

            
            Food temp = (Food)a;

            foreach(String cat in temp.Category)
            {
                strCat += cat + ",";
            }

            dsLocal.InsertCommand = "CreateFood";
            dsLocal.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
            dsLocal.InsertParameters[0].DefaultValue = temp.Name;
            dsLocal.InsertParameters[1].DefaultValue = temp.CalorieIntake.ToString();
            dsLocal.InsertParameters[2].DefaultValue = temp.RestaurantFlag.ToString();
            dsLocal.InsertParameters[3].DefaultValue = strCat;
            dsLocal.InsertParameters[4].DefaultValue = temp.ServingSize;
            if (dsLocal.InsertParameters.Count == 6)
            {
                dsLocal.InsertParameters.Add("userID", id.ToString());
            }
            else
            {
                dsLocal.InsertParameters[6].DefaultValue = id.ToString();
            }
            dsLocal.Insert();
            //Add logic to get return value of stored proc
            using (IDbCommand com = ConnectionFactory.GetDBConnection("local"))
            {
                com.CommandText = "SELECT MAX(Foods.FoodID) FROM FOODS";
                com.Connection.Open();
                using(IDataReader i = com.ExecuteReader())
                {
                    i.Read();
                    return i.GetInt32(0);
                }
                
            }

        }
        return -1;
    }

    //NOTE: Will not be used; Once custom foods have been created, they cannot be changed
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