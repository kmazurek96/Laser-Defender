using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    //[SerializeField] float shieldCapacity = 100;
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "EnemyProjectile")
        {
            Destroy(otherCollider.gameObject);
        }
        else if (otherCollider.tag == "Enemy")
            {
            Destroy(otherCollider.gameObject);
        }

    }


}
