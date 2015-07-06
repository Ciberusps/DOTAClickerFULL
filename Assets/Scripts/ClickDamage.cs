using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickDamage : MonoBehaviour
{
    public int currentLevelClick; //Текущий уровень "клика"
    public int upgradeCost; //Стоимость апгрейда
    public int clickDamageValue; //Дамаг за один клик
    public int doubleClick;

    Text lvlUpText;
    PlayerController playerController;

    void Start ()
    {
        lvlUpText = GameObject.Find("ClickDamageBox_COST").GetComponent<Text>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        currentLevelClick = 1; //Текущий лвл скила
        upgradeCost = 1; //Стоимость апгрейда клика

        //Модификаторы
        doubleClick = 1;                                             

        clickDamageValue = currentLevelClick * doubleClick;
	}
	
	void Update ()
    {
        lvlUpText.text = upgradeCost.ToString(); //Отображает уровень способности
    }

    //При поднятии уровня увеличивает дамаг
    public void lvlUp()
    {
        if (playerController.gold >= upgradeCost)
        {
            playerController.gold -= upgradeCost;
            clickDamageValue = clickDamageValue + 1;
            upgradeCost = upgradeCost + 1;
        }
        else
        {

        }
    }
}
