using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsTemplate 
{
    
   
    public bool achievementUnlocked;

    //<Summary>
    /// <param name="achievementName"></param>

    //constructor with parameters
    //whenever when we new an object
    public AchievementsTemplate(bool achievementUnlocked)
    {
        this.achievementUnlocked =  achievementUnlocked;
        //this.achievementName = achievementName;
        
    }

    //helper functions, formats data in form of json
    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //printing player details in the form of string
    //returns player details
    public string PrintAchievementDetails()
    {
        //return String.Format("Username: {0} \n Email: {1}\n Last Checkpoint: {2}\n User ID: {3} \n Active: {4}",
        //    this.userName,this.email,this.lastcp,this.UserId,this.active);
        return (this.achievementUnlocked.ToString());
    }
}
