using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    float randomBaseRotationSpeedRight;
    float randomBaseRotationSpeedLeft;
    float randomBaseRotationSpeed;


    private void Awake()
    {
        FindObjectOfType<GameSession>().AddMeteor();
    }

    private void OnDestroy()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.SubMeteor();
        }
       
    }
    void Start()
    {
        SetMeteorRotation();
    }

    private void SetMeteorRotation()
    {
        randomBaseRotationSpeedRight = Random.Range(10.0f, 20.0f);
        randomBaseRotationSpeedLeft = Random.Range(-20.0f, -10.0f);
        int x = Random.Range(0, 2);
        if (x == 0)
        {
            randomBaseRotationSpeed = randomBaseRotationSpeedLeft;
        }
        else
        {
            randomBaseRotationSpeed = randomBaseRotationSpeedRight;
        }
    }

    void Update()
    {

        //transform.Translate(Vector2.down * Time.deltaTime);
        gameObject.transform.Rotate(0f, 0f, randomBaseRotationSpeed * rotationSpeed * Time.deltaTime);
    }
}
