using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Food
{

    private int foodID;
    private int calorieIntake;
    private String name;
    private List<String> category;
    private bool restaurantFlag;
    private String servingSize;

    public String ServingSize
    {
        get { return servingSize; }
        set { servingSize = value; }
    }

    public int CalorieIntake
    {
        get { return calorieIntake; }
        set { calorieIntake = value; }
    }
    
    public String Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public List<String> Category
    {
        get { return category; }
    }
    
    public bool RestaurantFlag
    {
        get { return restaurantFlag; }
    }

    public bool addCategory(String category)
    {
        if(!this.category.Contains(category))
        {
            this.category.Add(category);
            return true;
        }
       
        return false;

    }

    public int getFoodID()
    {
        return this.foodID;
    }

	public Food(int foodID, int calorieIntake, String name, List<String> category, bool restaurantFlag, string servingSize)
	{
        this.foodID = foodID;
        this.calorieIntake = calorieIntake;
        this.name = name;
        this.category = category;
        this.restaurantFlag = restaurantFlag;
        this.servingSize = servingSize;
	}

    public override String ToString()
    {
        return String.Format("{0}, {1}, {2}, {3}, {4}", this.foodID, this.name, this.calorieIntake, this.restaurantFlag, this.category);   
    }
}