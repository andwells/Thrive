using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Group
/// </summary>
public class Group
{

    private int adminID;
    private int numberOfMembers;
    private String name;
    private int groupID;
    private List<Object> recommended;

    public int AdminID
    {
        get { return adminID; }
        set { adminID = value; }
    }
    
    public int NumberOfMembers
    {
        get { return numberOfMembers; }
        set { numberOfMembers = value; }
    }
    
    public String Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public int GroupID
    {
        get { return groupID; }
        set { groupID = value; }
    }
    
	public Group(int adminID, String name)
	{
        this.adminID = adminID;
        this.name = name;
        numberOfMembers = 0;
        recommended = new List<Object>();
	}

    public List<Object> RecommendedItems(int items)
    {
            
        List<Food> recFoods = new List<Food>();
        List<Exercise> recExercises = new List<Exercise>();
        int foodCount = 0, exerciseCount = 0;

        foreach(Object o in recommended)
        {
            if(o.GetType() == typeof(Food))
            {
                if(foodCount < items)
                {
                    recFoods.Add((Food)o);
                    foodCount++;
                }
            }
            else {
                if (exerciseCount < items)
                {
                    recExercises.Add((Exercise)o);
                    exerciseCount++;
                }
            }
            if (foodCount == items && exerciseCount == items)
            {
                break;
            }
        }
    
        List<Object> recommendations = new List<Object>();
        recommendations.Add(recFoods);
        recommendations.Add(recExercises);

        return recommendations;
    }

}