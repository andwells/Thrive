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

        return null;
    }

    object IDataManager.Get(object g)
    {
        if(g.GetType().Name.Equals("String"))
        {
            SqlDataSource tempDS;
            String[] items = ((String)g).Split(';');
            if (items[0].Equals("local"))
            {
                tempDS = dsLocal;

                tempDS.SelectCommand = "GetFood";
                tempDS.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            }
            else
            {
                tempDS = dsAPI;
            }
            tempDS.SelectParameters[0].DefaultValue = items[1];
            IDataReader results = (IDataReader)tempDS.Select(DataSourceSelectArguments.Empty);

            results.Read();
            return new Food(results.GetInt32(0), results.GetInt32(1), results.GetString(2), new List<String>(results.GetString(3).Split(';')), results.GetInt32(4));            
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
            

            return null;
        }
        else
        {
            return false;

        }
    }

    object IDataManager.Remove(object r, string id)
    {
        throw new NotImplementedException();
    }

    object IDataManager.Search(string name)
    {
        DataView apiResult = (DataView)dsAPI.Select(DataSourceSelectArguments.Empty);
        DataView result2 = (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);
        //Test what happens if we read use DataReader or DataView
        if (result2 != null)
        {
            result2.Table.Merge(apiResult.Table, true, MissingSchemaAction.Add);
            return result2;
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


    object IDataManager.Create(object c)
    {
        throw new NotImplementedException();
        //after Insert, make call to this.Add(c);
    }
}