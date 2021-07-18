using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShip : MonoBehaviour
{

    [SerializeField] List<GameObject> dropList;
    // Start is called before the first frame update
    private void OnDestroy()
    {
        int chosenDrop = Random.Range(0, dropList.Count);
        Instantiate(dropList[chosenDrop], transform.position, transform.rotation);
        Instantiate(dropList[chosenDrop], transform.position, transform.rotation);
        Instantiate(dropList[chosenDrop], transform.position, transform.rotation);
        Instantiate(dropList[chosenDrop], transform.position, transform.rotation);
        Instantiate(dropList[chosenDrop], transform.position, transform.rotation);
    }
}
