using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    Text textMoney;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        textMoney = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
        UpdateDisplayMoney();
        
    }

    public void UpdateDisplayMoney()
    {
        textMoney.text = gameSession.GetMoney().ToString() + "$";
    }


    
}
