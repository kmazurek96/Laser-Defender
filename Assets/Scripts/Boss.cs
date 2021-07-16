using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
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

    bool isImmortal = true;



    private void Awake()
    {
        FindObjectOfType<GameSession>().AddEnemy();
    }

    private void OnDestroy()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            FindObjectOfType<GameSession>().SubEnemy();
            FindObjectOfType<GameSession>().AddScore(scoreValue);

        }
    }


    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity) as GameObject;
        //projectile.transform.parent = transform;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotSoundVolume);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (collision.tag == "PlayerProjectile" && !isImmortal)
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

            Destroy(gameObject);
            EnemyDrop();
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);




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

    public void IsMortal()
    {
        isImmortal = false;
    }


}
