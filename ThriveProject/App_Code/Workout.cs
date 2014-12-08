using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Workout
{
    private int totalCalories;
    private String name;
    private DateTime time;
    private List<Exercise> exercises;
    private List<int> durations;
    private int workoutID;
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
    
    public List<Exercise> Exercises
    {
        get { return exercises; }
        set { exercises = value; }
    }

    public bool addExercise(Exercise e, int duration)
    {
        exercises.Add(e);
        totalCalories += e.CaloriesBurned;
        durations.Add(duration);
        return true;
    }

    public int WorkoutID
    {
        get { return workoutID; }
    }

    public bool deleteExercise(Exercise e)
    {
        if (exercises.Contains(e))
        {
            int i = exercises.IndexOf(e);
            exercises.Remove(e);
            totalCalories -= e.CaloriesBurned;
            durations.Remove(i);
            return true;
        }
        return false;
    }

    public List<int> Durations
    {
        get { return durations; }
    }

	public Workout(String name, DateTime time)
	{
        totalCalories = 0;
        this.name = name;
        this.time = time;
        exercises = new List<Exercise>();
        durations = new List<int>();
	}
    
    public Workout(int anId, Guid uID, int tCal, String name, DateTime time, List<Exercise> exercises, List<int> durations)
    {
        this.workoutID = anId;
        this.userId = uID;
        totalCalories = tCal;
        this.name = name;
        this.time = time;
        this.exercises = exercises;
        this.durations = durations;
    }
}