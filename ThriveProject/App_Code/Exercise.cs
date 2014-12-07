using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Exercise
{

    private int exerciseID;
    private int caloriesBurned;
    private String name;
    private List<String> category;
    private String type;

    public int ExerciseID
    {
        get { return exerciseID; }
        set { this.exerciseID = value; }
    }

    public int CaloriesBurned
    {
        get { return caloriesBurned; }
        set { caloriesBurned = value; }
    }
    
    public String Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public List<String> Category
    {
        get { return category; }
        set { category = value; }
    }
    
    public String Type
    {
        get { return type; }
        set { type = value; }
    }

	public Exercise(int exerciseID, int caloriesBurned, String name, List<String> category, String type)
	{
        this.exerciseID = exerciseID;
        this.caloriesBurned = caloriesBurned;
        this.name = name;
        this.category = category;
        this.type = type;
	}

}