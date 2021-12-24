using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float instanceTime;

    //reference authmanager
    private AuthManager AuthManager;

    //reference firebasemanager
    private FirebaseManager FirebaseManager;

    //bool to check if player stats are updated
    public bool isPlayerStatsUpdated;

    //Game Manager Singleton
    private static GameManager gameInstance;


    //declare some stuff for these values first basically can add these in the parts to change the data
    public int antrelationmeter = 0;
    public bool anthillcompletion = false; 
    public int lastcp;
    
    public string UserId;
    public string achievementName;

   


    private void Awake()
    {
        if (gameInstance != null)
        {
            Debug.Log("More than 1 Dialogue Manager in Scene");
        }

        gameInstance = this;


        //gets the auth manager and firebase manager components
        AuthManager = GameObject.Find("AuthManager").GetComponent<AuthManager>();
        FirebaseManager = GameObject.Find("FirebaseManager").GetComponent<FirebaseManager>();



        DontDestroyOnLoad(this);
    }

    //update player stats function
    public void UpdatePlayerStat(int lastcp, int antrelationmeter, bool anthillcompletion, float anthilltiming)
    {
        FirebaseManager.UpdatePlayerStats(AuthManager.auth.CurrentUser.UserId, AuthManager.GetCurrentDisplayName(),lastcp, antrelationmeter, anthillcompletion, anthilltiming);
        Debug.LogFormat("User ID: {0} \n Username: {1} \n Ant Relationship Meter: {2} \n Ant Hill Completion: {3} \n Ant Hill Timing: {4}", AuthManager.auth.CurrentUser.UserId, AuthManager.GetCurrentDisplayName(), antrelationmeter, anthillcompletion, anthilltiming);

    }


    void Start()
    {
        isPlayerStatsUpdated = false;
        UserId = AuthManager.auth.CurrentUser.UserId;
    }

    //update stats testing
    public void upStats()
    {
        if (!isPlayerStatsUpdated)
        {
            UpdatePlayerStat(lastcp, antrelationmeter, anthillcompletion, instanceTime);
        }
        isPlayerStatsUpdated = true;
    }
    // add achievement testing
    public void achievementadd(){
        achievementName = "Run into wall";

        FirebaseManager.UpdateAchievements(UserId, achievementName);
        //Debug.Log(UserId + "Run Into Wall");
    }



    public static GameManager GetInstance()
    {
        return gameInstance;
    }
}
