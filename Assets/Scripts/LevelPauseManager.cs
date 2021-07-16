using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {

        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberPauseMenu = FindObjectsOfType<LevelPauseManager>().Length;
        if (numberPauseMenu > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


}
