using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Overpower : MonoBehaviour
{
    EnemyHealth enemyHealth;
    EnemyManager enemyManager;
    Image cooldownSkillImage;
    Text cooldownSkillText;

    public float abillityOnCooldownTimer;
    public float abillityIsUsingTimer;
    bool abillityIsUsing;
    bool abilityOnCooldown;

    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        cooldownSkillImage = GameObject.Find("SkillOverpowerBorderOnCooldown").GetComponent<Image>();
        cooldownSkillText = GameObject.Find("SkillOverpowerOnCooldownText").GetComponent<Text>();

        abillityIsUsing = false;
        abillityIsUsingTimer = 3f;

        abilityOnCooldown = false;
        abillityOnCooldownTimer = 10f;
    }

    void Update()
    {
        if (!enemyManager.enemyDead)
            enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();

        if (abillityIsUsingTimer <= 0)
        {
            abillityIsUsing = false;
            abilityOnCooldown = true;
        }

        if (abillityOnCooldownTimer <= 0)
        {
            abilityOnCooldown = false;
        }

        if (abillityIsUsing)
            abillityIsUsingTimer -= Time.deltaTime;

        if (abilityOnCooldown)
        {
            abillityOnCooldownTimer -= Time.deltaTime;

            Color color = cooldownSkillImage.color;
            color.a = 1;
            cooldownSkillImage.color = color;

            cooldownSkillText.text = Mathf.RoundToInt(abillityOnCooldownTimer).ToString();
        }
        else
        {
            Color color = cooldownSkillImage.color;
            color.a = 0;
            cooldownSkillImage.color = color;

            cooldownSkillText.text = "";
        }
    }

    public void OverpowerSkill()
    {
        if (!abilityOnCooldown)
        {
            InvokeRepeating("CallTakeDamage", 0f, 0.1f);
            abillityIsUsing = true;
        }
        else
        {

        }
    }

    void CallTakeDamage()
    {
        if (!enemyManager.enemyDead && abillityIsUsing)
        {
            enemyHealth.TakeDamage();
        }
        else if (!abillityIsUsing)
        {
            CancelInvoke();

            abillityIsUsing = false;
            abillityIsUsingTimer = 3f;

            abilityOnCooldown = true;
            abillityOnCooldownTimer = 10f;
        }   
    }
}
