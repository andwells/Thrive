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
    private SqlDataSource dsAPI;
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
        dsLocal.Select(DataSourceSelectArguments.Empty);

        return null;
    }

    object IDataManager.Add(object a)
    {
        throw new NotImplementedException();
    }

    object IDataManager.Update(object u, string id)
    {
        throw new NotImplementedException();
    }

    object IDataManager.Remove(object r, string id)
    {
        throw new NotImplementedException();
    }

    object IDataManager.Search(string name)
    {
        return null;

    }

    bool IDataManager.Close()
    {
        throw new NotImplementedException();
    }
}