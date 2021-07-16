using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyJoystick;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    float minX, maxX, minY, maxY;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    bool canShoot = true; // time between shots
    bool canShootOnCurrentlyScene = true;
    [SerializeField] float timeBetweenShot = 0.5f;
    [SerializeField] public float health = 200f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float explosionSoundVolume = 0.75f;
    [SerializeField] AudioClip shotSound;
    [SerializeField] float shotSoundVolume = 0.75f;

    [SerializeField] bool isShieldON = false;
    [SerializeField] bool isPlayerImmortal = false;
    [SerializeField] int shieldCapacity = 100;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    [SerializeField] private static Joystick joystick; //  MOBILE VERSION
    bool joystickIsShieldActivated = false; //  MOBILE VERSION


    


    private void Awake()
    {
        
        SetUpSingleton(); 
        SetUpJoystick(); //  MOBILE VERSION
    }

    private static void SetUpJoystick() //  MOBILE VERSION
    {
        joystick = FindObjectOfType<Joystick>();
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


        //Shield(); //  PC VERSION

    }


    public void Fire()
    {
        //if (Input.GetButton("Fire1") && canShoot && canShootOnCurrentlyScene && PauseDisplay.isNotPaused) //  PC VERSION
        //if (Input.GetKey(KeyCode.Space) && canShoot && canShootOnCurrentlyScene && PauseDisplay.isNotPaused)
        if (canShoot && canShootOnCurrentlyScene && PauseDisplay.isNotPaused) //  MOBILE VERSION
        {
            projectilePrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotSoundVolume);
            StartCoroutine(WaitForNextShoot());


        }
    }

    private void Move()
    {
        //var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; // PC VERSION
        //var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; //  PC VERSION
        var deltaX = joystick.Horizontal() * Time.deltaTime * moveSpeed; //  MOBILE VERSION
        var deltaY = joystick.Vertical() * Time.deltaTime * moveSpeed; //  MOBILE VERSION

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
        if (collision.tag == "EnemyProjectile")
        {
            ProcessHit(damageDealer);
            Destroy(collision.gameObject);
            Debug.Log("Projecttile hit me");
        }
        else if (collision.tag == "Meteor")
        {
            ProcessHit(damageDealer);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            GameOver();
            Debug.Log("Enemy hit me");
        }

        else if (collision.tag == "Boss Laser")
        {

            isPlayerImmortal = false;
            ProcessHit(damageDealer);
        }

    }


    private void ProcessHit(DamageDealer damageDealer)
    {
        if (!isPlayerImmortal)
        {
            health -= damageDealer.GetDamage();
            FindObjectOfType<GameSession>().ActuallyPlayerHealth(health);



            if (health <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.LoadGameOver();
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




    public void Shield()
    {

        ActivateShield();
        if (!isShieldON)
        {

            transform.GetChild(0).gameObject.SetActive(false);
            isPlayerImmortal = false;
            nextActionTime = Time.time;

        }
        else if (isShieldON)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isPlayerImmortal = true;
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                SubShieldCapacityPerSec(1);
            }

        }
    }

    public void ActivateShield()
    {


        //if (Input.GetKeyDown(KeyCode.LeftShift) && shieldCapacity >= 20 && PauseDisplay.isNotPaused) //PC VERSION
        if (shieldCapacity >= 20 && PauseDisplay.isNotPaused) //MOBILE VERSION
        {
            isShieldON = true;
            SubShieldCapacity(20);
        }

        else if (shieldCapacity <= 0)
        {
            isShieldON = false;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isShieldON = false;
        }
    }








    public int GetShieldCapacity() { return shieldCapacity; }

    public void AddShieldCapacity(int value)
    {
        shieldCapacity += value;
    }

    public void SubShieldCapacity(int value)
    {
        shieldCapacity -= value;
    }

    public void SubShieldCapacityPerSec(int value)
    {
        shieldCapacity -= value;
    }

    public void ShieldCapacityIsEqual(int value)
    {
        shieldCapacity = value;
    }

    public void UpdatePlayerHealth(float hp)
    {
        health = hp;
    }

    public void joystickShield() //  MOBILE VERISON
    {


        

        if (shieldCapacity >= 20 && PauseDisplay.isNotPaused && joystickIsShieldActivated == false)
        {

            
            StartCoroutine(JoystickShieldON());
            transform.GetChild(0).gameObject.SetActive(true);


        }


    }

    IEnumerator JoystickShieldON() //  MOBILE VERISON
    {
        joystickIsShieldActivated = true;
        SubShieldCapacity(20);
       
        isShieldON = true;
        isPlayerImmortal = true;
        yield return new WaitForSeconds(3);
        transform.GetChild(0).gameObject.SetActive(false);
        isPlayerImmortal = false;
        isShieldON = false;
        joystickIsShieldActivated = false;



    }

    
}

