using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for ConnectionFactory
/// </summary>
public class ConnectionFactory
{
    public static IDbCommand GetDBConnection(string db)
    {
        IDbConnection con;
        IDbCommand cmd;
        if (db.Equals("local"))
        {
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            cmd = new SqlCommand();

        }
        else
        {
            con = new SqlConnection();
            con.ConnectionString = "";//Add logic for connecting to official food/exercise DB
            cmd = new SqlCommand();
        }
        cmd.Connection = con;
        return cmd;
    }
}