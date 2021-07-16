using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cruiser : MonoBehaviour
{
    Animator anim;
    int path;
    int attack;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 4f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        path = Random.Range(0, 2);

    }
    public void LaserON()
    {
            transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LaserOFF()
    {
            transform.GetChild(0).gameObject.SetActive(false);
    }

    public  void ChoosePath()
    {
        if (path == 0)
        {
            anim.SetTrigger("toLeft");
        }
        else if (path == 1)
        {
            anim.SetTrigger("toRight");
        }
    }

    public void ChooseAttack()
    {
        attack = Random.Range(0, 7);

        if (attack == 0)
        {
            anim.SetTrigger("LeftAttack1");
        }
        else if (attack == 1)
        {
            anim.SetTrigger("LeftAttack2");
        }
        else if (attack == 2)
        {
            anim.SetTrigger("RightAttack1");
        }
        else if (attack == 3)
        {
            anim.SetTrigger("RightAttack2");
        }
        else if (attack == 4 || attack == 5 || attack == 6)
        {
            anim.SetTrigger("ForwardAttack1");
        }

    }

    public void LaserAttack()
    {
        int randomValue = Random.Range(0,2);
        if(randomValue == 0)
        {
            LaserON();
        }
        else if (randomValue !=0)
        {
          
        }

    }

    public void ProjectilesAttack()
    {

        int randomValue = Random.Range(0, 2);

        if (randomValue == 0)
        {
            for (float i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    float j = i - 0f;
                    //GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
                    // projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(j + 0.2f, -projectileSpeed);


                    // GameObject projectile1 = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
                    // projectile1.GetComponent<Rigidbody2D>().velocity = new Vector2(-j - 0.2f, -projectileSpeed);

                    GameObject projectile2 = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
                    projectile2.GetComponent<Rigidbody2D>().velocity = new Vector2(j, -projectileSpeed);

                }
                else
                {
                    double j = i - (i * 0.9);
                    float k = (float)j;

                    GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(i + k, -projectileSpeed);


                    GameObject projectile1 = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
                    projectile1.GetComponent<Rigidbody2D>().velocity = new Vector2(-i - k, -projectileSpeed);

                }



            }
        }
        else { }
    }
    public void EntranceCompleted()
    {
        anim.SetBool("entranceCompleted", true);
    }

}
