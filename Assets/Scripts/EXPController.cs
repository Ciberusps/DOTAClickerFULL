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
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentEXP.text = sliderEXP.value + "/" + sliderEXP.maxValue;

        currentLevelUpdate();

        if (!enemyManager.enemyDead && !enemyManager.reborn)
            enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();
    }

    public void ApplyEXP()
    {
        if ((sliderEXP.value + enemyHealth.maxHP / 10) >= sliderEXP.maxValue)
        {
            sliderEXP.value = ((enemyHealth.maxHP / 10) - (sliderEXP.maxValue - sliderEXP.value));
            playerController.currentLevel += 1;
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
}
