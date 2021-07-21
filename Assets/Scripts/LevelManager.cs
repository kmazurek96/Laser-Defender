using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    int currentBuildIndex;


    private void Awake()
    {

        SetUpSingleton();
    }


    private void SetUpSingleton()
    {

        int numberLevelManagers = FindObjectsOfType<Player>().Length;
        if (numberLevelManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }



    }



    public int ReturnIndex()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        return currentBuildIndex;
    }

    public void LoadStartMenu()
    {
        
        
        if (FindObjectOfType<Player>())
        {
            GameObject player = FindObjectOfType<Player>().gameObject;
            Destroy(player);
            SceneManager.LoadScene(0);
            FindObjectOfType<GameSession>().GetComponent<PauseDisplay>().ResumeGame();
            FindObjectOfType<GameSession>().DestroySelf();
        }
        else
        {
            SceneManager.LoadScene(0);
            FindObjectOfType<GameSession>().GetComponent<PauseDisplay>().ResumeGame();
            FindObjectOfType<GameSession>().DestroySelf();
        }

  
    }
    public void LoadStartLevel()
    {
        SceneManager.LoadScene(1);
        //FindObjectOfType<GameSession>().ResetGame();
        FindObjectOfType<GameSession>().DestroySelf();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitAndLoadNextLevel());
    }

    public void LoadNextLevelInstantly()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex + 1);
        FindObjectOfType<HealthDisplay>().HealthDisplayToString();

    }

    IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(delayInSeconds);
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex + 1);
        
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void ResumeGame()
    {
        FindObjectOfType<GameSession>().GetComponent<PauseDisplay>().ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }




}
