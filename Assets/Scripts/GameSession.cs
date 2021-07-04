using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    int score = 0;
    float playerHealth;
    [SerializeField] int money = 0; // It's text money
    [SerializeField] int numberOfEnemies;
    [SerializeField] int numberOfDrops;



    void Update()
    {
        CheckingCurrentlyScene();
    }


    private void GoToNextLevelIfCompleted()
    {
        Scene sceneName1 = SceneManager.GetActiveScene();
        Scene sceneName = SceneManager.GetSceneByName("Game Over");
        Scene sceneName2 = SceneManager.GetSceneByName("Shop Level");

        if (sceneName != sceneName1 && sceneName1 != sceneName2)

        {

            if (numberOfEnemies <= 0 && numberOfDrops <= 0)
            {
                LevelManager levelManager = FindObjectOfType<LevelManager>();
                levelManager.LoadNextLevel();
            }
            else if (sceneName1 == sceneName2)
                { 

                }
            else
            { }
        }
    }
    private void Awake()
    {
        SetUpSingleton();
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
        Destroy(gameObject);
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

    void CheckingCurrentlyScene()
    {
        Scene sceneName1 = SceneManager.GetActiveScene();
        Scene sceneName = SceneManager.GetSceneByName("Game Over");
        Scene sceneName2 = SceneManager.GetSceneByName("Shop Level");
        Scene sceneName3 = SceneManager.GetSceneByName("Start Menu");
        if (sceneName1 == sceneName2)
        {
            FindObjectOfType<Player>().ShopScene();
        }

        else if(sceneName1 ==sceneName3)
        {
            Debug.Log("Start Scene");
        }
        else
        {
            FindObjectOfType<Player>().AnotherScene();
            GoToNextLevelIfCompleted();
        }

    }

}
