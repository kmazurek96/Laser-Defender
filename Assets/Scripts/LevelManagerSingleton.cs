using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {

        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberLevelManager = FindObjectsOfType<LevelManager>().Length;
        if (numberLevelManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
