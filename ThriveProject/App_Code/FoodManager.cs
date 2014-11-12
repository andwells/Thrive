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

	public FoodManager(SqlDataSource local, SqlDataSource API)
	{
        dsAPI = API;
        dsLocal = local;
	}

    List<string> ISearchableDataManager.searchByType(string type)
    {
        DataView x;
        if (type.Equals("restaurant"))
        {
            x = (DataView) dsAPI.Select(DataSourceSelectArguments.Empty);
            
        }
        else
        {
            x = (DataView)dsLocal.Select(DataSourceSelectArguments.Empty);
            
        }
        //Bind display
        //Bind value
        //Bind data
        
        return null;
    }

    List<string> ISearchableDataManager.searchByCategory(string category)
    {

        return null;
    }

    object IDataManager.Get(object g)
    {
        throw new NotImplementedException();
    }

    object IDataManager.Add(object a)
    {
        throw new NotImplementedException();
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

    List<string> IDataManager.Search(string name)
    {
        int totalItems = 0;
        IDataReader apiResult = (IDataReader)dsAPI.Select(DataSourceSelectArguments.Empty);
        IDataReader result2 = (IDataReader)dsLocal.Select(DataSourceSelectArguments.Empty);

        //Test what happens if we read use DataReader or DataView

        while (apiResult.Read())
        {
            
        }

        


        return null; 
    }

    bool IDataManager.Close()
    {
        if (apiConnection != null) { apiConnection.Connection.Close(); apiConnection = null; }
        if (localConnection != null) { localConnection.Connection.Close(); localConnection = null; }
        return true;
    }
}