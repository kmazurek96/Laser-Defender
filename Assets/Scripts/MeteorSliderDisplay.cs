using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorSliderDisplay : MonoBehaviour
{
    float value = 0f;
    Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();


    }

    // Update is called once per frame
    void Update()
    {
        UpdateMeteorSliderValue();
    }

    public void AddSliderValue()
    {
        value++;
        Debug.Log(value);
    }

    public float GetSliderValue() { return value; }

    void UpdateMeteorSliderValue()
    {
       
        
            slider.value = value;
       
    }

   
}
