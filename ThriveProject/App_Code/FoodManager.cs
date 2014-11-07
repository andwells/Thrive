using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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
        if (type.Equals("restaurant"))
        {
            //execute search in API/Official DB
        }
        else
        {
            //execute search in local DB
        }
        return null;
    }

    List<string> ISearchableDataManager.searchByCategory(string category)
    {
        throw new NotImplementedException();
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
            //x = (Food)u;
            //Do Update statement
            return null;
        }
        else
        {
            return null;

        }
    }

    object IDataManager.Remove(object r, string id)
    {
        throw new NotImplementedException();
    }

    List<string> IDataManager.Search(string name)
    {
        throw new NotImplementedException();
    }

    bool IDataManager.Close()
    {
        if (apiConnection != null) { apiConnection.Connection.Close(); apiConnection = null; }
        if (localConnection != null) { localConnection.Connection.Close(); localConnection = null; }
        return true;
    }
}