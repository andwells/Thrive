﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Workout
{

    private int totalCalories;
    private String name;
    private DateTime time;
    private List<Exercise> exercises;
    private List<double> exerciseTimes;

    public int TotalCalories
    {
        get { return totalCalories; }
        set { totalCalories = value; }
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
    
    public List<Exercise> Exercises
    {
        get { return exercises; }
        set { exercises = value; }
    }

    public bool addExercise(Exercise e)
    {
        if (!exercises.Contains(e))
        {
            exercises.Add(e);
            totalCalories += e.CaloriesBurned;
            return true;
        }
        return false;
    }

    public bool deleteExercise(Exercise e)
    {
        if (exercises.Contains(e))
        {
            exercises.Remove(e);
            totalCalories -= e.CaloriesBurned;
            return true;
        }
        return false;
    }

	public Workout(String name, DateTime time)
	{
        totalCalories = 0;
        this.name = name;
        this.time = time;
        exercises = new List<Exercise>();
        exerciseTimes = new List<double>();
	}

    public Workout(String name, DateTime time, List<Exercise> exercises, List<double> times)
    {
        totalCalories = 0;
        this.name = name;
        this.time = time;
        this.exercises = exercises;
        this.exerciseTimes = times;
    }
}