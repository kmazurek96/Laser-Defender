using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 100f;
    [SerializeField] bool isRotating = false;
    [SerializeField] float rotationSpeed  = 10f;


    public float GetDamage() { return damage; }

    public void Hit()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if(isRotating == true)
        {
            gameObject.transform.Rotate(0f, 0f, 10f * rotationSpeed * Time.deltaTime);
        }
    }
}
