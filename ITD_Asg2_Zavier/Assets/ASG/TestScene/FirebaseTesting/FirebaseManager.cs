using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class FirebaseManager : MonoBehaviour
{

    //references to the firebase database for playerstats and achievement
    DatabaseReference dbPlayerStatsReference;
    DatabaseReference dbAchievementsReference;

    public void Awake()
    {
        InitialiseFirebase();
        DontDestroyOnLoad(this.gameObject);
    }

    public void InitialiseFirebase()
    {
        //getting the default instance of the playerstats and achievements respectively
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
        dbAchievementsReference = FirebaseDatabase.DefaultInstance.GetReference("achievements");
    }


        //<Summary for playerstats>
    /// <param name="antrelationmeter"></param>
    /// <param name="anthillcompletion"></param>
    /// <param name="anthilltiming"></param>

        //<Summary for achievements>
    /// <param name="runintowall"></param>
    /// <param name="explorefor15min"></param>


    //UPDATE ACHIEVEMENTS TO FIREBASE FUNCTION
    public void UpdateAchievements(string UserId, string achievementName){
        Query achievementsQuery = dbAchievementsReference.Child(UserId);
        Debug.Log("firebase manager part works");


        achievementsQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error creating your entry. ERROR: " + task.Exception);
            }
            else if(task.IsCompleted)
            {
                DataSnapshot achievements = task.Result;
                Debug.Log("firebase task completes");

                   
                //update player stats here
                AchievementsTemplate achievementJson = new AchievementsTemplate(true);
                achievementJson.SaveToJson();
                Debug.Log("whats good");
                
                    
                dbAchievementsReference.Child(UserId).Child(achievementName).SetRawJsonValueAsync(achievementJson.SaveToJson());
                Debug.Log("whats good too");
                Debug.Log(achievementJson + achievementName);
                
                
                
            }
        });

    }
    

    //UPDATE PLAYER STATS TO FIREBASE FUNCTION
    public void UpdatePlayerStats(string UserId, string userName,int lastcp, int antrelationmeter, bool anthillcompletion, float anthilltiming)
    {
        Query playerQuery = dbPlayerStatsReference.Child(UserId);

        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error creating your entry. ERROR: " + task.Exception);
            }
            else if(task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
                if (playerStats.Exists)
                {
                    //update player stats here
                    //add xp/game
                    //add time spent
                    SimplePlayerStats sp = JsonUtility.FromJson<SimplePlayerStats>(playerStats.GetRawJsonValue());
                    sp.antrelationmeter += antrelationmeter;
                    sp.updateon = sp.getTimeUnix();

                    //check if theres a new high score 
                    if(anthilltiming < sp.anthilltiming)
                    {
                        sp.anthilltiming = anthilltiming;
                        //UpdatePlayerLeaderBoardEntry(userID, sp.anthilltiming, sp.updateon);
                    }


                    //path playerstats/userID
                    dbPlayerStatsReference.Child(UserId).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                }
                else
                {
                    //create player stats
                    SimplePlayerStats sp = new SimplePlayerStats(UserId, userName,lastcp, antrelationmeter, anthillcompletion,  anthilltiming);
                    //SimpleLeaderboard lb = new SimpleLeaderboard(userName, score);
                    
                    dbPlayerStatsReference.Child(UserId).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                    //dbLeaderboardsReference.Child(userID).SetRawJsonValueAsync(lb.SimpleLeaderboardToJson());
                }
            }
        });
    }

      // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
