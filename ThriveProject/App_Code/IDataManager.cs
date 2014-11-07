using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IDataManager
/// </summary>
public interface IDataManager
{
    object Get(object g);
    object Add(object a);
    object Update(object u, String id);
    object Remove(object r, String id);
    List<String> Search(String name);
    Boolean Close();
}