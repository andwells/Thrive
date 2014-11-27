using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ExerciseManager
/// </summary>
public class ExerciseManager : ISearchableDataManager
{
	public ExerciseManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    object ISearchableDataManager.searchByType(string type)
    {
        throw new NotImplementedException();
    }

    object ISearchableDataManager.searchByCategory(string category)
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