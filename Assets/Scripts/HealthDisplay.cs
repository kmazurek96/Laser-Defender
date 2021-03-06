using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    GameSession gameSession;
    // Start is called before the first frame update
    void Awake()
    {
        healthText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
        Player player = FindObjectOfType<Player>();
        //FindObjectOfType<GameSession>().ActuallyPlayerHealth(player.health);

    }

    // Update is called once per frame
    void Update()
    {
        HealthDisplayToString();
    }

    public void HealthDisplayToString()
    {
        healthText.text = gameSession.GetPlayerHealth().ToString();
    }
}
