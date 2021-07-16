using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyList : MonoBehaviour
{
    bool isBuyAble = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyingItem(int priceItem)
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        int money = gameSession.GetMoney();
        
        if (money >= priceItem)
        {
            isBuyAble = true;
            gameSession.SubMoney(priceItem);
        }
    }

    public void BuyDoubleShots()
    {
        if (isBuyAble)
        {
            GameObject doubleShots = Resources.Load("DoubleProjectile", typeof(GameObject)) as GameObject;
            Player player = FindObjectOfType<Player>();
            player.ChangeWeapon(doubleShots);
            isBuyAble = false;
        }

    }

    public void BuyQuadShots()
    {
        if (isBuyAble)
        {
            GameObject doubleShots = Resources.Load("QuadProjectile", typeof(GameObject)) as GameObject;
            Player player = FindObjectOfType<Player>();
            player.ChangeWeapon(doubleShots);
            isBuyAble = false;
        }
    }

    public void BuyShield()
    {
        if (isBuyAble)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                int shieldValue = player.GetShieldCapacity();
                if (shieldValue < 50)
                {
                    player.AddShieldCapacity(50);
                    isBuyAble = false;
                }
                else if(shieldValue >= 50)
                {
                    player.ShieldCapacityIsEqual(100);
                    isBuyAble = false;
                }
            }


        }
    }

    public void BuyHP()
    {
        if (isBuyAble)
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if (gameSession != null)
            {
                float hp = gameSession.GetPlayerHealth();
                hp += 200;
                gameSession.ActuallyPlayerHealth(hp);
                isBuyAble = false;
            }


        }
    }

}
