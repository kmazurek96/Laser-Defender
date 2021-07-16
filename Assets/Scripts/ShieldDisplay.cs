using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldDisplay : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShieldValue();
    }

    void UpdateShieldValue()
    {
        Player player = FindObjectOfType<Player>();
        if (player!= null)
        {
            int value = player.GetShieldCapacity();
            slider.value = value;
        }
    }
}
