using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldBagController : MonoBehaviour
{
    public int goldBagValue;
    public int goldBagNum;
    public string goldBagName;
    
    public GameObject receiveGold;
    PlayerController playerController;

    void Start ()
    {
        
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        goldBagName = "gold_bag_" + goldBagNum; //Инициализация имени мешка при создании по кол-ву всех мешков

        StartCoroutine(UnpickedBag()); //через 20 секунд мешок автоматически "поднимается"
    }

    //При наведении мыши собирает мешок
    void OnMouseEnter()
    {
        playerController.GetGold(goldBagValue);

        playGoldReceive();

        Destroy(GameObject.Find(goldBagName));
    }

    //Проигрывает анимацию подбора мешка
    void playGoldReceive()
    {
        GameObject temp = (GameObject)Instantiate(receiveGold, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));

        temp.name = "gold_bag_canvas_" + playerController.numOfGoldBags;
        temp.transform.position = new Vector3(transform.position.x, transform.position.y, -90f);
        temp.GetComponentInChildren<Text>().text = "+" + goldBagValue;

        Destroy(temp, 1.25f);
    }

    //Неподобранный мешок через 20 секунд поднимается автоматически
    IEnumerator UnpickedBag()
    {
        yield return new WaitForSeconds(20f);

        playerController.GetGold(goldBagValue);

        playGoldReceive();

        Destroy(GameObject.Find(goldBagName));
    }
}
