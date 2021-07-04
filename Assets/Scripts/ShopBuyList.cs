using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyList : MonoBehaviour
{
    ShopPrice[] shopPrice;
    // Start is called before the first frame update
    void Start()
    {
        shopPrice = FindObjectsOfType<ShopPrice>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyDoubleShots()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        
        int money = gameSession.GetMoney();
        int price = shopPrice[0].GetPrice(); 
        if(money >= price)
        {
            gameSession.SubMoney(price);
            GameObject doubleShots = Resources.Load("DoubleProjectile", typeof(GameObject)) as GameObject;
            Player player = FindObjectOfType<Player>();
            player.ChangeWeapon(doubleShots);
            
        }
    }

    public void BuyQuadShots()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        int money = gameSession.GetMoney();
        int price = shopPrice[1].GetPrice();
        if (money >= price)
        {
            gameSession.SubMoney(price);
            GameObject doubleShots = Resources.Load("QuadProjectile", typeof(GameObject)) as GameObject;
            Player player = FindObjectOfType<Player>();
            player.ChangeWeapon(doubleShots);

        }
    }


}
