using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AchievementManager : MonoBehaviour
{
    //achievement prefab
    public GameObject achievementPrefab;

    //array of sprites to pick whichever sprite to change it in the achievementprefab
    public Sprite[] sprites;

    /* tells achievementmanager which button user is clicking and is "active"
    to switch category */
    private AchievementBtn activeButton;

    // scrollrect content of achievement mask
    public ScrollRect scrollRect;

    //controls on off of menu
    public GameObject achievementMenu;

    //show on screen when achievement earned
    public GameObject visualAchievement;

    //dictionary to hold our achievements
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public Sprite unlockSprite;

    private static AchievementManager instance;

    public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
            {
                //access instance from other class
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }
            return AchievementManager.instance;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //ensures one button is clicked when game is started
        activeButton = GameObject.Find("Level 1Btn").GetComponent<AchievementBtn>();
        CreateAchievement("Level 1", "Run into wall", "Run into wall to unlock", 0);

        CreateAchievement("Level 2", "Hi", "say hi", 1);

        activeButton.Click();

        //toggle menu off on start
        achievementMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            achievementMenu.SetActive(!achievementMenu.activeSelf);
        }


    }



    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {

            //instantiate achievement on screen
            GameObject achievement = GameObject.Instantiate(visualAchievement);
            SetAchievementInfo("EarnCanvas", achievement, title);
            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3); //waits for 3 seconds to destroy achievement(remove it from screen)

        Destroy(achievement);
    }

    //function to create achievement
    public void CreateAchievement(string parent, string title, string description, int spriteIndex)
    {
        //instantiate a new achievement prefab into the mask area
        GameObject achievement = GameObject.Instantiate(achievementPrefab);

        //these new achievment has reference to achievement we instantiated above
        Achievement newAchievement = new Achievement(name, description, spriteIndex, achievement);

        //adding achievement to dictionary
        achievements.Add(title, newAchievement);

        //calling setachievement function
        SetAchievementInfo(parent, achievement, title);
    }

    //finding specific category and setting the achievement for parent
    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        //find category to set parent
        achievement.transform.SetParent(GameObject.Find(parent).transform);

        //sets scale of achievementprefab back to 1,1,1 since it changes inside unity
        achievement.transform.localScale = new Vector3(1, 1, 1);

        //getting child of achievement prefabs to change the values, for eg
        //child 0 is title, child 1 is description , child 2 is sprite image
        achievement.transform.GetChild(0).GetComponent<Text>().text = title;
        achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].description;
        achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].spriteIndex];

    }

    //changing categories with button
    public void ChangeCategory(GameObject button)
    {
        //reference AchievementButton script allowing button to get component inside script
        AchievementBtn achievementButton = button.GetComponent<AchievementBtn>();

        //take the achievement mask and pulls category(level1, level2) into the scroll content of the scroll rect 
        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();

        //calling click function in AchievementButton script
        achievementButton.Click();
        activeButton.Click();
        activeButton = achievementButton;
    }
}
