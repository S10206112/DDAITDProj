using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBtn : MonoBehaviour
{
    //list of achievements gameobject for eg level 1, level 2
    public GameObject achievementList;


    public Sprite neutral, highlight;

    private Image sprite;

    //called before start
    void Awake()
    {
        sprite = GetComponent<Image>();
    }

    public void Click()
    {
        if (sprite.sprite == neutral)
        {
            sprite.sprite = highlight;
            //show category button is managing
            achievementList.SetActive(true);
        }
        else
        {
            sprite.sprite = neutral;
            achievementList.SetActive(false);
        }
    }
}
