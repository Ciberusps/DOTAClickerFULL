using UnityEngine;
using System.Collections;

/*Создает "бесконечных врагов"*/

public class EnemyManager : MonoBehaviour
{
    
    public bool enemyDead; //Герой мертв
    public bool reborn; //Герой возраждается
    public bool firstTime; //Возраждение в начале без задержки

    public GameObject[] enemyModels; //Массив стандартных персонажей

    EnemyHealth enemyHealth; //Скрипт текущего перса
    Vector3 lastCoords; //Последние координаты персонажа перед смертью
    Quaternion lastRotation; //Последний угол вращения персонажа
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        lastCoords = new Vector3(2f, 0.8f, -48.2f);
        lastRotation = Quaternion.Euler(new Vector3(15f, 130f, -15f));

        reborn = false;
        enemyDead = true;
        firstTime = true;

        playerController.currentLevel = 1; //Устанавливает начальный уровень игры
    }

    // Update is called once per frame
    void Update()
    {
        //Если персонаж мертв и неперерождается, тогда спавним нового
        if (enemyDead && !reborn)
        {
            StartCoroutine(SpawnEnemy());
            reborn = true;
        }
    }

    void OnMouseDown()
    {
        if (!enemyDead)
            enemyHealth.TakeDamage();
    }

    IEnumerator SpawnEnemy()
    {
        //Задержка на 1 секунду, если спавнится не в первый раз
        if (!firstTime)
            yield return new WaitForSeconds(1f);
        else
            firstTime = false;

        yield return null;

        //Вставляем и инициализируем префаб из массива
        Instantiate(enemyModels[Random.Range(0,2)], lastCoords, lastRotation);
        enemyHealth = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<EnemyHealth>();
        enemyHealth.maxHP = playerController.currentLevel * 10;
        enemyHealth.currentHealth = enemyHealth.maxHP;
        enemyHealth.anim = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<Animator>();

        //Сохраняем последние координаты
        lastCoords = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<Transform>().position;
        lastRotation = GameObject.FindGameObjectWithTag("EnemyModel").GetComponent<Transform>().rotation;

        //Игрок неперерождается и не мертв
        reborn = false;
        enemyDead = false;
    }
}
