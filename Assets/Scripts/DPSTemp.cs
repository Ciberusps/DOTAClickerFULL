using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class DPSTemp : MonoBehaviour
{
    public PlayerController playerController;

    public List<DPSHero> DPSHeroes;

    [System.Serializable]
    public class DPSHero
    {
        public string name;
        public int dps;
        public int level;
        public int cost;
        public Image icon;
        public Button button;
        public Text nameText;
        public Text costText;
        public GameObject Log;
        public PlayerController playerController;

        //Модификаторы DPS'a
        public int doubleDPS;

        public void lvlUp()
        {
            if (playerController.gold >= cost)
            {
                playerController.gold -= cost;
                playerController.currentDPS -= dps;
                dps *= 2; //Увеличение dps'a
                playerController.currentDPS += dps;
                cost += 5; //Увеличение стоимости
            }
            else
            {
                Log.GetComponent<Text>().text = "Not enought gold";
                Log.GetComponent<Animator>().Play("AbilityOnCoolDown");
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        foreach (DPSHero hero in DPSHeroes)
        {
            hero.playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            hero.nameText.text = hero.name;

            hero.doubleDPS = 1;

            hero.dps = hero.level * hero.doubleDPS;

            hero.playerController.currentDPS += hero.dps;

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerController.currentDPS = 0;

	    foreach (DPSHero hero in DPSHeroes)
        {
            hero.costText.text = hero.cost.ToString();

            hero.nameText.text = hero.name;

            hero.dps = hero.level * hero.doubleDPS;

            playerController.currentDPS += hero.dps;
        }
	}

    public void lvlUp(int num)
    {
        if (playerController.gold >= DPSHeroes[num].cost)
        {
            playerController.gold -= DPSHeroes[num].cost;
            playerController.currentDPS -= DPSHeroes[num].dps;

            if (DPSHeroes[num].level == 0)
                DPSHeroes[num].dps = DPSHeroes[num].dps + 1;
            else
                DPSHeroes[num].dps = DPSHeroes[num].dps * 2;

            playerController.currentDPS += DPSHeroes[num].dps;

            DPSHeroes[num].cost = DPSHeroes[num].cost + 1;

            DPSHeroes[num].level += 1;
        }
        else
        {

        }
    }
}
