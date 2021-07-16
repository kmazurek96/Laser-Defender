using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotButton : MonoBehaviour
{


    public GameObject attackButton;

    public void onPress()
    {
        attackButton.SetActive(true);
    }

    public void onRelease()
    {
        attackButton.SetActive(false);

    }
}
