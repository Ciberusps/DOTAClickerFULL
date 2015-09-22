using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int gold; //Золото игрока
    public int numOfGoldBags; //Кол-во золотых мешков
    public int currentDPS; //Текущий DPS
    public int currentLevel; //Текущий уровень
    public int EXP;
    public bool currentSide; //Текущая сторона radiant(dire)

//    public Text goldText; //Текст показывающий золото
    public UILabel goldLabel;
    EnemyManager enemyManager;
    ClickDamage clickDamage;

    void Awake ()
    {
        currentLevel = 1;

        enemyManager = GameObject.Find("Enemy").GetComponent<EnemyManager>();
        clickDamage = GameObject.Find("ClickDamageBox").GetComponent<ClickDamage>();

        //        goldText = GameObject.Find("GoldText").GetComponent<Text>();

        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        goldLabel.text = gold.ToString();
    }

    public void GetGold(int goldValue)
    {
        gold += goldValue;
    }
}
