using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    float minX, maxX, minY, maxY;
    [SerializeField]GameObject projectilePrefab;
    [SerializeField]float projectileSpeed = 10f;
    bool canShoot = true; // time between shots
    bool canShootOnCurrentlyScene = true;
    [SerializeField]float timeBetweenShot = 0.5f;
    [SerializeField] public float health = 200f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float explosionSoundVolume = 0.75f;
    [SerializeField] AudioClip shotSound;
    [SerializeField] float shotSoundVolume = 0.75f;



    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<Player>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    


    void Start()
    {
        GameBoundaries();
    }


    void Update()
    {
        Move();
        Fire();
  

    }



    private void Fire()
    {

        if (Input.GetButton("Fire1")&& canShoot && canShootOnCurrentlyScene)
        {
            projectilePrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y + 0.5f),transform.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotSoundVolume);
            StartCoroutine(WaitForNextShoot());
           

        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newPosX = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newPosX, newPosY);
    }

    IEnumerator WaitForNextShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShot);
        canShoot = true;
    }

    private void GameBoundaries()
    {
        Camera gameCamera = Camera.main;

        minX = gameCamera.ViewportToWorldPoint(new Vector3(0.05f, 0, 0)).x;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(0.95f, 0, 0)).x;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.1f, 0)).y;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.3f, 0)).y;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if(collision.tag == "EnemyProjectile")
        {
            ProcessHit(damageDealer);
            Destroy(collision.gameObject);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        FindObjectOfType<GameSession>().ActuallyPlayerHealth(health);



        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.LoadGameOver();
        }
    }


    public GameObject ChangeWeapon(GameObject newWeapon)
    {
        projectilePrefab = newWeapon;
        return newWeapon;
    }

    public void ShopScene()
    {
        canShootOnCurrentlyScene = false;
    }

    public void AnotherScene()
    {
        canShootOnCurrentlyScene = true;
    }


}
