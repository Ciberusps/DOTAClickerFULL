using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Skills : MonoBehaviour
{
    public List<Skill> skills;

    EnemyManager enemyManager;
    EnemyHealth enemyHealth;

	void Start () 
    {
        enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
    }
	
	void Update () 
    {
	    if (!enemyManager.enemyDead)
            enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();


	}

    void FixedUpdate()
    {
        foreach (Skill s in skills)
        {
            if (s.currentTimeOfUsing <= 0)
            {
                s.IsUsing = false;
            }

            if (s.currentCoolDown <= 0)
            {
                s.OnCoolDown = false;
                s.currentCoolDown = s.coolDown;
            }

            if (s.IsUsing)
                s.currentTimeOfUsing -= Time.deltaTime;

            if (s.OnCoolDown)
            {
                s.currentCoolDown -= Time.deltaTime;
                s.Blackout.fillAmount = s.currentCoolDown / s.coolDown;

                Color color = s.Blackout.color;
                color.a = 0.6f;
                s.Blackout.color = color;
                
                color = s.BorderOnCoolDown.color;
                color.a = 1;
                s.BorderOnCoolDown.color = color;

                s.CoolDownText.text = Mathf.RoundToInt(s.currentCoolDown).ToString();
            }
            else
            {
                Color color = s.BorderOnCoolDown.color;
                color.a = 0;
                s.BorderOnCoolDown.color = color;

                s.CoolDownText.text = "";
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
        public Image BorderOnCoolDown;
        public Text CoolDownText;
        public GameObject SkillLog;

        public bool OnCoolDown;
        public bool IsUsing;

        public bool CheckAvailability()
        {
            if (this.OnCoolDown)
            {
                SkillLog.GetComponent<Animator>().Play("AbilityOnCoolDown");
                return false;
            }
            else
            {
                this.OnCoolDown = true;
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
