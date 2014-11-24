using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Meal
{

    private int totalCalories;
    private String name;
    private DateTime time;
    private List<Food> foods;
    private int foodId;
    private Guid userId;

    public int TotalCalories
    {
        get { return totalCalories; }
    }
    
    public String Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public DateTime Time
    {
        get { return time; }
        set { time = value; }
    }
    
    public List<Food> Foods
    {
        get { return foods; }
        set { foods = value; }
    }

    public bool addFood(Food f)
    {
        if(!foods.Contains(f))
        {
            foods.Add(f);
            totalCalories += f.CalorieIntake;
            return true;
        }
        return false;
    }

    public bool deleteFood(Food f)
    {
        if (foods.Contains(f))
        {
            foods.Remove(f);
            totalCalories -= f.CalorieIntake;
            return true;
        }
        return false;
    }

	public Meal(String name, DateTime time)
	{
        totalCalories = 0;
        this.name = name;
        this.time = time;
        foods = new List<Food>();
	}
    
    public Meal(String name, DateTime time, List<Food> foods)
    {
        totalCalories = 0;
        this.name = name;
        this.time = time;
        this.foods = foods;
    }
}