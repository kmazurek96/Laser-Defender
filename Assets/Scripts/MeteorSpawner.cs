using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorSpawner : MonoBehaviour
{

    [SerializeField] List<Meteor> listOfMeteors;
    float xMeteorPosition;
    float spawnTimer = 1f;
    float timer = 0f;
    [SerializeReference] Slider slider;
    bool isSpawning = true;
 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float sliderValue = slider.GetComponent<MeteorSliderDisplay>().GetSliderValue();
        if(sliderValue == slider.maxValue)
        {
            isSpawning = false;
        }

        if (timer > spawnTimer && isSpawning)
        {
            spawnTimer = Random.Range(1.0f, 2.0f);
            SpawnMeteor();
            slider.GetComponent<MeteorSliderDisplay>().AddSliderValue(); 
            timer = 0;
        }
    }

    void SpawnMeteor()
    {

            xMeteorPosition = Random.Range(-4.0f, 3.0f);
            var newMeteor = Instantiate(listOfMeteors[Random.Range(0, listOfMeteors.Count)],
                new Vector2(transform.position.x + (xMeteorPosition), transform.position.y),
                transform.rotation);

        Debug.Log(xMeteorPosition);

    }
}
