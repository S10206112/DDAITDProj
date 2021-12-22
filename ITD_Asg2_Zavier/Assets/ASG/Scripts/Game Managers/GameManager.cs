using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float instanceTime;

    //Game Manager Singleton
    private static GameManager gameInstance;
    private void Awake()
    {
        if (gameInstance != null)
        {
            Debug.Log("More than 1 Dialogue Manager in Scene");
        }

        gameInstance = this;

        DontDestroyOnLoad(this);
    }
    public static GameManager GetInstance()
    {
        return gameInstance;
    }
}
