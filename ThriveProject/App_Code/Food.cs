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
    private int restaurantFlag;

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
    
    public int RestaurantFlag
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

	public Food(int foodID, int calorieIntake, String name, List<String> category, int restaurantFlag)
	{
        this.foodID = foodID;
        this.calorieIntake = calorieIntake;
        this.name = name;
        this.category = category;
        this.restaurantFlag = restaurantFlag;
	}

}