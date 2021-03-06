﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Meal
{
    private int totalCalories;
    private String name;
    private DateTime time;
    private List<Food> foods;
    private List<double> servings;
    private int mealID;
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

    public bool addFood(Food f, double serving)
    {
        foods.Add(f);
        totalCalories += f.CalorieIntake;
        servings.Add(serving);
        return true;
    }

    public int MealID
    {
        get { return mealID; }
    }

    public bool deleteFood(Food f)
    {
        if (foods.Contains(f))
        {
            int i = foods.IndexOf(f);
            foods.Remove(f);
            totalCalories -= f.CalorieIntake;
            servings.Remove(i);
            return true;
        }
        return false;
    }

    public List<double> Servings
    {
        get { return servings; }
    }

	public Meal(String name, DateTime time)
	{
        totalCalories = 0;
        this.name = name;
        this.time = time;
        foods = new List<Food>();
        servings = new List<double>();
	}
    
    public Meal(int anId, Guid uID, int tCal, String name, DateTime time, List<Food> foods, List<double> servings)
    {
        this.mealID = anId;
        this.userId = uID;
        totalCalories = tCal;
        this.name = name;
        this.time = time;
        this.foods = foods;
        this.servings = servings;
    }
}