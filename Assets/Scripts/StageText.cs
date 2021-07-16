using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StageText : MonoBehaviour
{
    TMP_Text stageText;
    int levelNumber;
    // Start is called before the first frame update
    void Start()
    {
        stageText = GetComponent<TMP_Text>();
        levelNumber = FindObjectOfType<LevelManager>().ReturnIndex();
        if((levelNumber % 5 == 0))
        {
            stageText.text = "Stage " + levelNumber + "    Bonus Level";
        }
        else { stageText.text = "Stage " + levelNumber; } 
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
