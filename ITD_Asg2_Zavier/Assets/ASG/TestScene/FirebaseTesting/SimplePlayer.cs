
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//classname in PascalCase
//for each word the first letter is capital letter
public class SimplePlayer 
{
    //bunch of properties
    public bool active;
    public long createdon;
    public long lastLoggedIn;
    public long updatedon;
    public int lastcp;
    public string email;
    public string UserId;
    public string userName;

    
    //<Summary>
    /// <param name="createdon"></param>
    /// <param name="lastLoggedIn"></param>
    /// <param name="updatedon"></param>
    /// <param name="lastcp"></param>
    /// <param name="email"></param>
    /// <param name="UserId"></param>
    /// <param name="userName"></param>
    /// <param name="active"></param>

    //constructor with parameters
    //whenever when we new an object
    public SimplePlayer(string UserId,  
        string userName, string email, int lastcp = 0, bool active = true)
    {
        this.userName = userName;
        this.email = email;
        this.lastcp = lastcp;
        this.UserId = UserId;
        this.active = active;

        //timestamped properties
        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        this.lastLoggedIn = timestamp;
        this.createdon = timestamp;
        this.updatedon = timestamp;
        
    }

    //helper functions, formats data in form of json
    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //printing player details in the form of string
    //returns player details
    public string PrintPlayerDetails()
    {
        //return String.Format("Username: {0} \n Email: {1}\n Last Checkpoint: {2}\n User ID: {3} \n Active: {4}",
        //    this.userName,this.email,this.lastcp,this.UserId,this.active);
        return String.Format("User ID: {0} \n Active: {1} \n Email: {2} \n Last Checkpoint: {3} \n Username: {4}",
        this.UserId, this.active, this.email, this.lastcp, this.userName);
    }
}