using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShot = 0.2f;
    [SerializeField] float maxTimeBetweenShot = 2f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 2f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float explosionSoundVolume = 0.75f;
    [SerializeField] AudioClip shotSound;
    [SerializeField] float shotSoundVolume = 0.75f;

    [Header("Drop")]
    [SerializeField] int chanceForDrop;
    [SerializeField] List<GameObject> dropList;



    private void Awake()
    {
        FindObjectOfType<GameSession>().AddEnemy(); 
    }

    private void OnDestroy()
    {
        FindObjectOfType<GameSession>().SubEnemy();
    }

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotSoundVolume);
        shotCounter = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (collision.tag== "PlayerProjectile")
        {
            ProcessHit(damageDealer);
            Destroy(collision.gameObject);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        if (health <= 0)
        {
            FindObjectOfType<GameSession>().AddScore(scoreValue);
            Destroy(gameObject);
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
            EnemyDrop();



        }
    }

    private void EnemyDrop()
    {
        chanceForDrop = Random.Range(1, 100);
        if (chanceForDrop <= 15)
        {
            int chosenDrop = Random.Range(0, dropList.Count);
            Instantiate(dropList[chosenDrop], transform.position, transform.rotation);

        }
    }

}
