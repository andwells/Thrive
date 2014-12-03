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
        if(!name.Equals("String"))
        {
            return null;
        }
        User temp = null;
        using (SqlCommand cmd = (SqlCommand)ConnectionFactory.GetDBConnection("local"))
        {
            cmd.CommandText = "GetUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", (String)g);
            cmd.Connection.Open();
            using (SqlDataReader r = cmd.ExecuteReader())
            {
                r.Read();
                Guid gu = r.GetGuid(0);
                String uName = r.GetString(1);
                int age = r.GetInt32(2);
                String gender = r.GetString(3);
                int height = r.GetInt32(4);
                bool hyFlag = (bool)r[5];
                int hyGoal = r[6] == DBNull.Value ?  -1 : r.GetInt32(6);
                bool slFlag = (bool)r[7];
                int slGoal = r[8] == DBNull.Value ? -1 : r.GetInt32(8);
                bool strFlag = (bool)r[9];
                int strGoal = r[10] == DBNull.Value ? -1 : r.GetInt32(10);
                double w = r.GetInt32(11);
                int weightGoal =r[12] == DBNull.Value ? -1 : r.GetInt32(12);

                temp = new User(gu, uName, age, gender, height, hyFlag, hyGoal, slFlag, slGoal, strFlag,
                    strGoal, w, weightGoal);
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
}