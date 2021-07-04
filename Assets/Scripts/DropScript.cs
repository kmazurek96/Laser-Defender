using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    private void Awake()
    {
        FindObjectOfType<GameSession>().AddDrop();
    }

    private void OnDestroy()
    {
        FindObjectOfType<GameSession>().SubDrop();
    }

    void Start ()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
