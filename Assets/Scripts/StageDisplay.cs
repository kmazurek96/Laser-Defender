using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageDisplay : MonoBehaviour
{
    GameObject stageText;
    bool showCompletedText = true;




    void Start()
    {


        StartCoroutine(DisplayStageText());
    }


    IEnumerator DisplayStageText()
    {

        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(0).gameObject.SetActive(false);
        GameSession gameSession;
        gameSession = FindObjectOfType<GameSession>();
        gameSession.GetshowCompletedText(showCompletedText);
        //bool canRespawn = true;

    }

    public IEnumerator DisplayLevelCompletedText()
    {

        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.LoadNextLevel();
        //bool canRespawn = true;
    }
}
