using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EXPController : MonoBehaviour
{
    //EnemyHealth enemyHealth;
    PlayerController playerController;
    Text currentEXP;
    Text currentLevelText;
    Slider sliderEXP;
    EnemyHealth enemyHealth;
    EnemyManager enemyManager;

    public int levelUpPoints;
    public Button levelUpSkillButton;
    public int[] levelOfLevelUpPoints;
	
    // Use this for initialization
	void Start ()
    {
        currentEXP = GameObject.Find("CurrentEXP").GetComponent<Text>();
        sliderEXP = GameObject.Find("EXPSlider").GetComponent<Slider>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        currentLevelText = GameObject.Find("CurrentLevel").GetComponent<Text>();
        sliderEXP.maxValue = playerController.currentLevel * 10;

        currentEXP.text = sliderEXP.value + "/" + sliderEXP.maxValue;

        levelUpPoints = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentEXP.text = sliderEXP.value + "/" + sliderEXP.maxValue;

        currentLevelUpdate();

        if (levelUpPoints > 0)
        {
            DrawLevelUpButton();
        }
        else
        {
            UndrawLevelUpButton();
        }

        if (!enemyManager.enemyDead && !enemyManager.reborn)
            enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();
    }

    public void ApplyEXP()
    {
        if ((sliderEXP.value + enemyHealth.maxHP / 10) >= sliderEXP.maxValue)
        {
            sliderEXP.value = ((enemyHealth.maxHP / 10) - (sliderEXP.maxValue - sliderEXP.value));
            playerController.currentLevel += 1;
            GiveLevelUpPoints();
            sliderEXP.maxValue = playerController.currentLevel * 10;
        }
        else
        {
            sliderEXP.value += enemyHealth.maxHP / 10;
        }
    }

    public void currentLevelUpdate()
    {
        currentLevelText.text = "Level " + playerController.currentLevel;
    }

    public void DrawLevelUpButton()
    {
        Color color = levelUpSkillButton.image.color;
        color.a = 1f;
        levelUpSkillButton.image.color = color;

        levelUpSkillButton.GetComponentInChildren<Text>().text = "Level + " + levelUpPoints;
        levelUpSkillButton.interactable = true;
    }

    public void UndrawLevelUpButton()
    {
        Color color = levelUpSkillButton.image.color;
        color.a = 0f;
        levelUpSkillButton.image.color = color;

        levelUpSkillButton.GetComponentInChildren<Text>().text = "";
        levelUpSkillButton.interactable = false;
    }

    public void GiveLevelUpPoints()
    {
        for (int i = 0; i < 25; i++)
        {
            if (playerController.currentLevel == levelOfLevelUpPoints[i])
            {
                levelUpPoints += 1;
            }
            
        }
    }
}
