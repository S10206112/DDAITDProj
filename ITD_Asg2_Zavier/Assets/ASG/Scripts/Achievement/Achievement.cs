using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{
    public string name;

    public string description;

    public bool unlocked;

    public int spriteIndex;

    public GameObject achievementRef;



    public Achievement(string name, string description, int spriteIndex, GameObject achievementRef)
    {
        this.name = name;
        this.description = description;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        //LoadAchievement();
    }

    //check if achievement unlocked
    public bool EarnAchievement()
    {
        if (!unlocked)
        {
            //change sprite when achievement earned
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockSprite;
            //SaveAchievement(true);

            return true;
        }
        return false;
    }

   /* public void SaveAchievement(bool value)
    {
        //to save players achievement
        unlocked = value;

        PlayerPrefs.Save();

    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

        if (unlocked)
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockSprite;
        }
    }*/
