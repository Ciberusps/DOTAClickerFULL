using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DPS : MonoBehaviour
{
    public int currentDPSHeroLevel;
    public int currentDPSHero; 
    public int upgradeCost; //Стоимость апгрейда
    public int doubleDPS;

    Text lvlUpText;
    PlayerController playerController;

    void Start()
    {
        lvlUpText = GameObject.Find("DPS").GetComponentInChildren<Text>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        currentDPSHeroLevel = 0; //Текущий лвл скила
        //upgradeCost = 1; //Стоимость апгрейда клика

        //Модификаторы
        doubleDPS = 1;

        currentDPSHero = currentDPSHeroLevel * doubleDPS;

        playerController.currentDPS += currentDPSHero;
    }

    void Update()
    {
        lvlUpText.text = upgradeCost.ToString(); //Отображает уровень способности
    }

    //При поднятии уровня увеличивает дамаг
    public void lvlUp()
    {
        if (playerController.gold >= upgradeCost)
        {
            playerController.gold -= upgradeCost;
            playerController.currentDPS -= currentDPSHero;
            currentDPSHero = currentDPSHero + 1;
            playerController.currentDPS += currentDPSHero;
            upgradeCost = upgradeCost + 1;
        }
        else
        {

        }
    }

}
