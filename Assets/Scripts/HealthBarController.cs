using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public float minXHealthBar, maxXHealthBar;
    public float currentXHealthBar;

    EnemyHealth enemyHealth;
    EnemyManager enemyManager;
    RectTransform healthTransform;
    RectTransform topBlickTransform;
    RectTransform leftBlickTransform;
    Text healthText;

    void Start ()
    {
        //Инициализация ХПбара
        enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        //enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();
        healthTransform = GameObject.Find("VisualHealth").GetComponent<RectTransform>();
        topBlickTransform = GameObject.Find("TopBlick").GetComponent<RectTransform>();
        leftBlickTransform = GameObject.Find("LeftBlick").GetComponent<RectTransform>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        
        //Вычисление максимальных, минимальных значений полоски ХПбара
        maxXHealthBar = healthTransform.localPosition.x;
        minXHealthBar = healthTransform.localPosition.x - healthTransform.rect.width;

        currentXHealthBar = 0;
        healthText.text = 0 + " / " + 0;
    }

	void Update ()
    {
        if (!enemyManager.reborn && !enemyManager.enemyDead)
        {
            //Инициализация префаба врага при появлении
            enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();
            
            //Эффект hpBar'а как в дотке блик движущийся с хп
            if (enemyHealth.currentHealth < enemyHealth.maxHP)
            {

            }
            else
            {

            }

            //Вычисление текущих координат ХПбара
            currentXHealthBar = MapValues(enemyHealth.currentHealth, 0, enemyHealth.maxHP, minXHealthBar, maxXHealthBar);

            //Текущие ХП/максХП
            healthText.text = Mathf.RoundToInt(enemyHealth.currentHealth) + " / " + Mathf.RoundToInt(enemyHealth.maxHP);

            //Локальные позиции полоски хп и бликов
            healthTransform.localPosition = new Vector3(currentXHealthBar, 12, 5);
            topBlickTransform.localPosition = new Vector3(currentXHealthBar, 12, 5);
            leftBlickTransform.localPosition = new Vector3(currentXHealthBar, 12, 5);
        }
        else
        {
            healthText.text = 0 + " / " + 0;

            //Локальная позиция всегда равна минимальному значению
            healthTransform.localPosition = new Vector3(minXHealthBar, 6, 5);
            topBlickTransform.localPosition = new Vector3(minXHealthBar, 6, 5);
            leftBlickTransform.localPosition = new Vector3(minXHealthBar, 6, 5);
        }
    }

    //Вычисление координат полоски ХП
    float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return ((x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin); ;
    }
}
