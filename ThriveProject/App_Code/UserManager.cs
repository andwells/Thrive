using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

/// <summary>
/// Summary description for UserManager
/// </summary>
public class UserManager : IDataManager
{
	public UserManager()
	{

	}
    
    object IDataManager.Get(object g)
    {
        String name = g.GetType().Name;
        if(!name.Equals("Guid") && !name.Equals("String"))
        {
            return null;
        }
        User temp = null;
        using (SqlCommand cmd = (SqlCommand)ConnectionFactory.GetDBConnection("local"))
        {
            cmd.CommandText = "GetUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", g.ToString());
            using (SqlDataReader r = cmd.ExecuteReader())
            {
                temp = new User(r.GetGuid(0), r.GetString(1), r.GetInt32(2), r.GetString(3), r.GetInt32(4),
                    (bool)r[5], r.GetInt32(6), (bool)r[7], r.GetInt32(8), (bool)r[9], r.GetInt32(10), r.GetDouble(11), r.GetInt32(12));
            }


        }
        return temp;
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
        throw new NotImplementedException();
    }

    bool IDataManager.Close()
    {
        throw new NotImplementedException();
    }


    object IDataManager.Create(object c)
    {
        throw new NotImplementedException();
    }
}