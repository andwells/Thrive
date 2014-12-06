using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class User
{

    private int height;
    private double weight;
    private int age;
    private String gender;
    private bool sleepFlag;
    private int sleepGoal;
    private bool hydrationFlag;
    private int hydrationGoal;
    private bool stressFlag;
    private int stressGoal;
    private int weightManagementgoal;
    //private String password;
    private String username;
    private Guid userID;
    private List<Group> groups;

    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    public double Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    public int Age
    {
        get { return age; }
        set { age = value; }
    }
    
    public String Gender
    {
        get { return gender; }
        set { gender = value; }
    }
    
    public bool SleepFlag
    {
        get { return sleepFlag; }
        set { sleepFlag = value; }
    }
    
    public int SleepGoal
    {
        get { return sleepGoal; }
        set { sleepGoal = value; }
    }
    
    public bool HydrationFlag
    {
        get { return hydrationFlag; }
        set { hydrationFlag = value; }
    }
    
    public int HydrationGoal
    {
        get { return hydrationGoal; }
        set { hydrationGoal = value; }
    }
    
    public bool StressFlag
    {
        get { return stressFlag; }
        set { stressFlag = value; }
    }
    
    public int StressGoal
    {
        get { return stressGoal; }
        set { stressGoal = value; }
    }
    
    public int WeightManagementgoal
    {
        get { return weightManagementgoal; }
        set { weightManagementgoal = value; }
    }
    
    public String Username
    {
        get { return username; }
    }
    
    //public String Password
    //{
    //    get { return password; }
    //}

    public Guid UserID
    {
        get { return userID; }
    }

    public List<Group> Groups
    {
        get { return groups; }
    }


    public User(Guid UserId, String username, int age, String gender, int height, bool hydrationFlag, int hydrationGoal, bool sleepFlag, int sleepGoal, bool stressFlag, int stressGoal,
    double weight, int weightManagementgoal)//, List<Group> groups), String password)
	{
        this.height = height;
        this.weight = weight;
        this.age = age;
        this.gender = gender;
        this.sleepFlag = sleepFlag;
        this.sleepGoal = sleepGoal;
        this.hydrationFlag = hydrationFlag;
        this.hydrationGoal = hydrationGoal;
        this.stressFlag = stressFlag;
        this.stressGoal = stressGoal;
        this.weightManagementgoal = weightManagementgoal;
        //this.password = password;
        this.username = username;
        this.userID = UserId;
        //this.groups = groups;
	}
}