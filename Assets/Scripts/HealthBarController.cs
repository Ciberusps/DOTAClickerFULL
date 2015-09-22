using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public static HealthBarController instance;
    private EnemyHealth _enemyHealth;
    private EnemyManager _enemyManager;
    public UISlider _healthSlider;
    public UILabel _healthLabel;

    void OnEnable()
    {
        instance = this;
    }

    void Start ()
    {
        //Инициализация ХПбара
        _enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
        _healthLabel = GameObject.Find("HealthLabel").GetComponent<UILabel>();
       // _healthSlider = gameObject.GetComponent<UISlider>();
    }

	void Update ()
    {
        if (!_enemyManager.reborn && !_enemyManager.enemyDead)
        {
            //Инициализация префаба врага при появлении
            _enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();
            
            SetCurrentValue();
            //SetCurrentEnemyHealth();
            
            //Эффект hpBar'а как в дотке блик движущийся с хп
            if (_enemyHealth.currentHealth < _enemyHealth.maxHP)
            {

            }
            else
            {

            }

      
        }
        else
        {
       
        }
    }

    public void SetCurrentValue()
    {
        if (!_enemyManager.enemyDead && !_enemyManager.reborn)
        {
            _healthSlider.value = _enemyHealth.currentHealth/_enemyHealth.maxHP;
//            print(_enemyHealth.currentHealth + " / " +  _enemyHealth.maxHP);
            SetCurrentEnemyHealth();
        }

    }

    public void SetCurrentEnemyHealth()
    {
        _healthLabel.text = _enemyHealth.currentHealth + " / " + _enemyHealth.maxHP;
    }
}
