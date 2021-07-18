using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCanvasSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {

        SetUpSingleton();
    }

    private void Update()
    {
        CheckIfIsPlayer();
    }

    private void SetUpSingleton()
    {
        int number = FindObjectsOfType<MobileCanvasSingleton>().Length;
        if (number > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void CheckIfIsPlayer()
    {
        Player player = FindObjectOfType<Player>();
        if(!player)
        {
            Destroy(gameObject);
        }
    }

}
