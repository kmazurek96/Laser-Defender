using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void SetAnimationDone()
    {
        anim.SetBool("Done", true);     
    }

    public void SetAnimationNotDone()
    {
        anim.SetBool("Done", false);
    }

}
