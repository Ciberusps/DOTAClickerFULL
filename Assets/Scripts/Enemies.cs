using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public EnemyClass currentEnemy;

    GameObject temp; //Временный объект для создания мешков

    [System.Serializable]
    public class EnemyClass
    {
        public float maxHP; //Максимальное ХП персонажа
        public float currentHealth; //Текущее ХП персонажа
        public int enemyCoins; //Примерное кол-во монет которое получит игрок за убийство этого персонажа
        public int numOfDropCoins; //Кол-во мешков в которых игрок получит свои монеты
        public Animator anim;
        public GameObject gold_bag; //Префаб мешка с золотом
        public GameObject damage_text;
        public ClickDamage clickDamage;
        public HealthBarController healthBarController = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarController>(); //Скриптовый HealthBar
        public PlayerController playerController;
        public EnemyManager enemyManager;
        public Slider sliderEXP;
        public EXPController expController;

        public EnemyClass()
        {
            //Инициализация анимаций, ХПбара, мэнеджера, контроллера игрока...
            sliderEXP = GameObject.Find("EXPSlider").GetComponent<Slider>();
            enemyManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyManager>();
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            clickDamage = GameObject.Find("ClickDamageBox").GetComponent<ClickDamage>();
            expController = GameObject.Find("EXPSlider").GetComponent<EXPController>();

            maxHP = playerController.currentLevel * 10;
            enemyCoins = playerController.currentLevel * 2;
            currentHealth = maxHP;

            NumOfDropCoinsValue(); //расчет кол-во мешков в которых игрок получит свои монеты взависимости от уровня
        }
        

        public void TakeDamage()
        {
            currentHealth -= clickDamage.clickDamageValue; //От текущего ХП - урон за клик

            anim = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<Animator>();

            //Если не мертв - анимация принятия урона
            if (!enemyManager.enemyDead)
            {
                anim.SetTrigger("isDamaged");

                GameObject temp = (GameObject)Instantiate(damage_text, new Vector3(Input.mousePosition.x, Input.mousePosition.y), Quaternion.identity);
                temp.GetComponentInChildren<Text>().text = "-" + clickDamage.clickDamageValue.ToString();
                Destroy(temp, 0.4f);

                anim.SetTrigger("isIdle");
            }


            DeathCheck(); //Проверка на то умер ли враг или нет
        }

        //Спавнит золотые мешки в зависимости от кол-ва монет выпадающих с врага
        public void DropCoins()
        {
            for (int i = 1; i <= numOfDropCoins; i++)
            {
                SpawnGoldBag();
            }
        }

        //Спавнит золотой мешок
        public void SpawnGoldBag()
        {
            GameObject temp = (GameObject)Instantiate(gold_bag, new Vector3(Random.RandomRange(-6f, 10f), Random.RandomRange(5f, 20f), Random.RandomRange(-50f, -60f)), Quaternion.Euler(15f, 130f, -15f));
            playerController.numOfGoldBags += 1;
            temp.name = "gold_bag_" + playerController.numOfGoldBags;
            temp.GetComponent<Rigidbody>().velocity = new Vector3(Random.RandomRange(1f, 5f), -30f, Random.RandomRange(1f, 5f));
            temp.GetComponent<GoldBagController>().goldBagValue = enemyCoins / numOfDropCoins;
            temp.GetComponent<GoldBagController>().goldBagNum = playerController.numOfGoldBags;
        }

        //Определяет кол-во мешков которое выпадет из врага в зависимости от уровня
        public void NumOfDropCoinsValue()
        {
            if (playerController.currentLevel <= 5)
            {
                numOfDropCoins = 1;
            }
            else if (playerController.currentLevel > 50)
            {
                numOfDropCoins = Random.Range(1, 9);
            }
            else
            {
                numOfDropCoins = Random.Range(1, 5);
            }
        }

        //Проверка на смерть врага
        public void DeathCheck()
        {
            if (currentHealth <= 0 && !enemyManager.enemyDead)
            {
                currentHealth = 0;
                enemyManager.enemyDead = true;

                EnemyDeath();
            }
        }

        public void ApplyDPS()
        {
            currentHealth -= playerController.currentDPS * Time.fixedDeltaTime;
        }

        //Разрушает "врага", проигрывает анимацию смерти
        public void EnemyDeath()
        {
            anim.SetBool("isDead", true); //Анимация смерти
            DropCoins(); //Спавн золотых мешков
            Destroy(GameObject.FindGameObjectWithTag("EnemyModel"), 1f);
            expController.ApplyEXP();
        }
    }    

    void Start()
    {
        currentEnemy.anim = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<Animator>();
        //currentEnemy.maxHP = currentEnemy.playerController.currentLevel * 10;
    }

    void Update()
    {
        currentEnemy.DeathCheck(); //Проверка на то умер ли враг или нет
        currentEnemy.anim.SetTrigger("isIdle");
        currentEnemy.ApplyDPS();
    }

    void FixedUpdate()
    {
        
    }

    public void OverpowerSkill()
    {
        InvokeRepeating("TakeDamage", 2, 0.2f);
    }
}
