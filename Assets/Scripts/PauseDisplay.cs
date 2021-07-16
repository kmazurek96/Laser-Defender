using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseDisplay : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    public static bool escapeClicked = false;
    public static bool isNotPaused = true;
    private bool isSceneWhereCantPause = false;


    private void Awake()
    {
      
    }

    private void Start()
    {

        pauseCanvas.SetActive(false);
    }


    private void Update()
    {
        CheckingScene();
        Pause();
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escapeClicked && !isSceneWhereCantPause)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escapeClicked)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        escapeClicked = true;
        isNotPaused = false;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        escapeClicked = false;
        isNotPaused = true;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }



    private void CheckingScene()
    {
        Scene sceneName1 = SceneManager.GetActiveScene();
        Scene sceneName = SceneManager.GetSceneByName("Start Menu");
        Scene sceneName2 = SceneManager.GetSceneByName("End Game -Delete");
        Scene sceneName3 = SceneManager.GetSceneByName("Shop Level");
        if (sceneName1 == sceneName || sceneName1 == sceneName2 || sceneName1 == sceneName3)
        {
            isSceneWhereCantPause = true;
        }
        else
        {
            isSceneWhereCantPause = false;
        }
    }

}
