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
    EXPController expController;

    void Start () 
    {
        enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        expController = GameObject.Find("EXPSlider").GetComponent<EXPController>();
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

                Color color = skill.Used.color;
                color.a = 0f;
                skill.Used.color = color;
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

            SkillNotLearning();

            if (skill.Learning)
            {
                skill.LearningOn();
            }
            else
            {
                skill.LearningOff();
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
        public Image Used;
        public Image Learn;
        public List<MultiDimensionalImage> level; 
        public Text CoolDownText;
        public GameObject Log;
        public EXPController expController;

        public bool OnCoolDown;
        public bool IsUsing;
        public bool Learning;
        public bool SkillChoosing;

        public void LearningOn()
       {
            if (currentLevel < 4)
            {
                Color color = level[currentLevel + 1].StateImage[0].color;
                color.a = 0f;
                level[currentLevel + 1].StateImage[0].color = color;

                color = level[currentLevel + 1].StateImage[1].color;
                color.a = 1f;
                level[currentLevel + 1].StateImage[1].color = color;

                color = level[currentLevel + 1].StateImage[2].color;
                color.a = 0f;
                level[currentLevel + 1].StateImage[2].color = color;

                color = Learn.color;
                color.a = 1f;
                Learn.color = color;
            }
        }

        public void LearningOff()
        {
            if (currentLevel < 4)
            {
                Color color = level[currentLevel + 1].StateImage[0].color;
                color.a = 1f;
                level[currentLevel + 1].StateImage[0].color = color;

                color = level[currentLevel + 1].StateImage[1].color;
                color.a = 0f;
                level[currentLevel + 1].StateImage[1].color = color;

                color = level[currentLevel + 1].StateImage[2].color;
                color.a = 0f;
                level[currentLevel + 1].StateImage[2].color = color;

                color = Learn.color;
                color.a = 0f;
                Learn.color = color;
            }
        }

        public void LearnChoosed()
        {
            if (currentLevel < 4)
            {
                SkillChoosing = false;
                Learning = false;

                Color color = level[currentLevel + 1].StateImage[0].color;
                color.a = 0f;
                level[currentLevel + 1].StateImage[0].color = color;

                color = level[currentLevel + 1].StateImage[1].color;
                color.a = 0f;
                level[currentLevel + 1].StateImage[1].color = color;

                color = level[currentLevel + 1].StateImage[2].color;
                color.a = 1f;
                level[currentLevel + 1].StateImage[2].color = color;

                expController.levelUpPoints -= 1;
                currentLevel += 1;
            }
            else
            {
                Log.GetComponent<Text>().text = "Max level";
                Log.GetComponent<Animator>().Play("AbilityOnCoolDown");
            }
            
        }

        public bool CheckAvailability()
        {
            if(SkillChoosing)
            {
                if (expController.levelUpPoints > 0)
                    LearnChoosed();

                return false;
            }
            else
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
                    DrawUsedEffect();
                    return true;
                }
            }
        }

        public void DrawUsedEffect()
        {
            Color color = Used.color;
            color.a = 1f;
            Used.color = color;
        }
    }

    [System.Serializable]
    public class MultiDimensionalImage
    {
        public Image[] StateImage;
    }

    public void SkillLearning()
    {
        foreach (Skill skill in skills)
        {
            //skill.LearningOn();
            skill.Learning = true;
            skill.SkillChoosing = true;
        }
    }

    public void SkillNotLearning()
    {
        foreach (Skill skill in skills)
        {
            if (skill.Learning == false && expController.levelUpPoints == 0)
            {
                foreach(Skill skill2 in skills)
                {
                    skill2.Learning = false;
                    skill2.SkillChoosing = false;
                }
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

    /*+++++++++++OVERPOWER++++++++++++*/
    public void Overpower2()
    {
        if (skills[1].CheckAvailability())
        {
            InvokeRepeating("CallTakeDamage2", 0f, 0.1f);
            skills[1].IsUsing = true;
        }
    }

    void CallTakeDamage2()
    {
        if (!enemyManager.enemyDead && skills[1].IsUsing)
        {
            enemyHealth.TakeDamage();
        }
        else if (!skills[1].IsUsing)
        {
            CancelInvoke();

            skills[1].IsUsing = false;
            skills[1].currentTimeOfUsing = skills[1].timeOfUsing;
        }
    }
    /*++++++++OVERPOWER END++++++++++++*/
}
