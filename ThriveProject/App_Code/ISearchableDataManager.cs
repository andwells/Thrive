using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ISearchableDataManager
/// </summary>
public interface ISearchableDataManager : IDataManager
{
    object searchByType(String type);
    object searchByCategory(String category);
}