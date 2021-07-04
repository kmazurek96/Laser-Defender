using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField]WaveConfig waveConfig;
    // Start is called before the first frame update
    List<Transform> waypoints;
    int waypointIndex = 0;
    void Start()
    {
        waypoints = waveConfig.GetWavepoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }



    // Update is called once per frame
    void Update()
    {

        if (waypointIndex <= waypoints.Count - 1)

        {
            var enemySpeed = waveConfig.GetmoveSpeed() * Time.deltaTime;
            var targetLocation = waypoints[waypointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, enemySpeed);

           if (transform.position == targetLocation)
            {
                waypointIndex++;
            }



        }
      else
        {
            waypointIndex = 0;
        }


    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    
}
