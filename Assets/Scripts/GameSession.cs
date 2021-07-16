using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    int score = 0;
    [SerializeField] float  playerHealth;
    [SerializeField] int money = 0; // It's text money
    [SerializeField] int numberOfEnemies;
    [SerializeField] int numberOfDrops;
    [SerializeField] int numberOfMeteors;
    bool showCompletedText = true;


    private void Awake()
    {
        SetUpSingleton();
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            playerHealth = player.health;
        }
        else if (!player)
        {
            Debug.Log("Don't find a player");
        }
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }





    void Update()
    {
        //Debug.Log("Time Since Loaded : " + Time.timeSinceLevelLoad);
        CheckingCurrentlyScene();
    }


    private void GoToNextLevelIfCompleted()
    {
        Scene sceneName1 = SceneManager.GetActiveScene();
        Scene sceneName = SceneManager.GetSceneByName("Game Over");
        Scene sceneName2 = SceneManager.GetSceneByName("Shop Level");
        Scene sceneName3 = SceneManager.GetSceneByName("Meteor Level");
        if (sceneName != sceneName1 && sceneName1 != sceneName2 &&  sceneName1 != sceneName3)

        {

            if (numberOfEnemies <= 0 && numberOfDrops <= 0 && Time.timeSinceLevelLoad >= 10)
            {
                if (showCompletedText == true)
                {
                    StageDisplay stageDisplay = FindObjectOfType<StageDisplay>();
                    StartCoroutine(stageDisplay.GetComponent<StageDisplay>().DisplayLevelCompletedText());
                    showCompletedText = false;
                }
            }
                
          }
        else if (sceneName1 == sceneName2)
                { 

                }

            else if(sceneName1 == sceneName3 && Time.timeSinceLevelLoad >= 10)
            {
            Debug.Log("You are on Meteor Level");
                if (numberOfMeteors <= 0)
                {
                    if (showCompletedText == true)
                    {
                        AddMoney(300);
                        StageDisplay stageDisplay = FindObjectOfType<StageDisplay>();
                        StartCoroutine(stageDisplay.GetComponent<StageDisplay>().DisplayLevelCompletedText());
                        showCompletedText = false;
                    }

                }

            }
            else
            { }
        
    }

    public bool GetshowCompletedText(bool value)
    {
        showCompletedText = value;
        return showCompletedText;
    }



    public int GetScore()
    {
        return score;
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
    }
    
    public void ActuallyPlayerHealth(float ActuallyPlayerHealth)
    {
        playerHealth = ActuallyPlayerHealth;
        FindObjectOfType<Player>().UpdatePlayerHealth(playerHealth);

    }

    public void AddMoney(int value)
    {
        money += value;
        FindObjectOfType<MoneyDisplay>().UpdateDisplayMoney();
    
    }

    public void SubMoney(int value)
    {
        money -= value;
        FindObjectOfType<MoneyDisplay>().UpdateDisplayMoney();

    }

    public int GetMoney()
    {
        return money;
    }


    public void ResetGame()
    {

        //Destroy(gameObject);
    }

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public int GetNumberOfDrops()
    {
        return numberOfDrops;
    }

    public void AddEnemy()
    {
        numberOfEnemies++;
    }

    public void SubEnemy()
    {
        numberOfEnemies--;
    }

    public void AddDrop()
    {
        numberOfDrops++;
    }

    public void SubDrop()
    {
        numberOfDrops--;
    }

    public void AddMeteor()
    {
        numberOfMeteors++;
    }

    public void SubMeteor()
    {
        numberOfMeteors--;
    }

    void CheckingCurrentlyScene()
    {
        Player player = FindObjectOfType<Player>();
        Scene sceneName1 = SceneManager.GetActiveScene();
        Scene sceneName = SceneManager.GetSceneByName("Game Over");
        Scene sceneName2 = SceneManager.GetSceneByName("Shop Level");
        Scene sceneName3 = SceneManager.GetSceneByName("Start Menu");
        Scene sceneName4 = SceneManager.GetSceneByName("Meteor Level");
        if (sceneName1 == sceneName2)
        {
            FindObjectOfType<Player>().ShopScene();
        }

        else if(sceneName1 ==sceneName3)
        {
            Debug.Log("Start Scene");
        }

        else if (player != null)
        {
            FindObjectOfType<Player>().AnotherScene();
            GoToNextLevelIfCompleted();
        }

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
