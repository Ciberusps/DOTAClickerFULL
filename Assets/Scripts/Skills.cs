using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Skills : MonoBehaviour
{
    public List<Skill> skills;

    EnemyManager enemyManager;
    EnemyHealth enemyHealth;
    PlayerController playerController;

	void Start () 
    {
        enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
	void Update () 
    {
	    if (!enemyManager.enemyDead)
            enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();
	}

    void FixedUpdate()
    {
        foreach (Skill skill in skills)
        {
            if (skill.currentTimeOfUsing <= 0)
            {
                skill.IsUsing = false;
            }

            if (skill.currentCoolDown <= 0)
            {
                skill.OnCoolDown = false;
                skill.currentCoolDown = skill.coolDown;
            }

            if (skill.IsUsing)
                skill.currentTimeOfUsing -= Time.deltaTime;

            if (skill.OnCoolDown)
            {
                skill.currentCoolDown -= Time.deltaTime;
                skill.Blackout.fillAmount = skill.currentCoolDown / skill.coolDown;

                Color color = skill.Blackout.color;
                color.a = 0.6f;
                skill.Blackout.color = color;
                
                color = skill.Border.color;
                color.r = 0.5f;
                color.g = 0.5f;
                color.b = 0.5f;
                skill.Border.color = color;

                skill.CoolDownText.text = Mathf.RoundToInt(skill.currentCoolDown).ToString();
            }
            else
            {
                Color color = skill.Border.color;
                color.r = 1f;
                color.g = 1f;
                color.b = 1f;
                skill.Border.color = color;

                skill.CoolDownText.text = "";
            }

            if (skill.Learning)
            {
                skill.LearningOn();
            }

        }
        
    }
    
    [System.Serializable]
    public class Skill
    {
        public float coolDown;
        public float currentCoolDown;
        public float timeOfUsing;
        public float currentTimeOfUsing;
        public int currentLevel;
        public Button Button;
        public Image Icon;
        public Image Blackout;
        public Image IconHighlighted;
        public Image Slot;
        public Image Border;
        public List<Image> level1;
        public List<Image> level2;
        public List<Image> level3;
        public List<Image> level4;
        public Text CoolDownText;
        public GameObject Log;

        public bool OnCoolDown;
        public bool IsUsing;
        public bool Learning;

        public void LearningOn()
        {
            //level1
            Color color = level1[0].color;
            color.a = 0f;
            level1[0].color = color;

            color = level1[1].color;
            color.a = 1f;
            level1[1].color = color;

            color = level1[2].color;
            color.a = 0f;
            level1[2].color = color;

            //level2
            color = level2[0].color;
            color.a = 0f;
            level2[0].color = color;

            color = level2[1].color;
            color.a = 1f;
            level2[1].color = color;

            color = level2[2].color;
            color.a = 0f;
            level2[2].color = color;

            //level3
            color = level3[0].color;
            color.a = 0f;
            level3[0].color = color;

            color = level3[1].color;
            color.a = 1f;
            level3[1].color = color;

            color = level3[2].color;
            color.a = 0f;
            level3[2].color = color;

            //level4
            color = level4[0].color;
            color.a = 0f;
            level4[0].color = color;

            color = level4[1].color;
            color.a = 1f;
            level4[1].color = color;

            color = level4[2].color;
            color.a = 0f;
            level4[2].color = color;
        }

        public void LearningOff()
        {
            Color color = level1[0].color;
            color.a = 1f;
            level1[0].color = color;

            color = level1[1].color;
            color.a = 0f;
            level1[1].color = color;

            color = level1[2].color;
            color.a = 0f;
            level1[2].color = color;
        }

        public bool CheckAvailability()
        {
            if (currentLevel == 0)
            {
                Log.GetComponent<Text>().text = "Ability not learned";
                Log.GetComponent<Animator>().Play("AbilityOnCoolDown");
                return false;
            }

            if (OnCoolDown)
            {
                Log.GetComponent<Text>().text = "Ability on cooldown";
                Log.GetComponent<Animator>().Play("AbilityOnCoolDown");
                return false;
            }
            else
            {
                OnCoolDown = true;
                return true;
            }
            
                
        }
    }

    /****************Skills**************/

    /*+++++++++++OVERPOWER++++++++++++*/
    public void Overpower()
    {
        if (skills[0].CheckAvailability())
        {
            InvokeRepeating("CallTakeDamage", 0f, 0.1f);
            skills[0].IsUsing = true;
        }
    }

    void CallTakeDamage()
    {
        if (!enemyManager.enemyDead && skills[0].IsUsing)
        {
            enemyHealth.TakeDamage();
        }
        else if (!skills[0].IsUsing)
        {
            CancelInvoke();

            skills[0].IsUsing = false;
            skills[0].currentTimeOfUsing = skills[0].timeOfUsing;
        }
    }
    /*++++++++OVERPOWER END++++++++++++*/
}
