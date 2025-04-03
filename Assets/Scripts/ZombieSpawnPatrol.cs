using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnPatrol : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] zombies;
    public int zombieSpawnAmt = 3;

    private float respawnTimer = 10f;
    private float resetTimer = 0f;
    private bool canSpawn = true;
    // Update is called once per frame
    void Update()
    {
        if (canSpawn == false)
        {
            resetTimer += 1 * Time.deltaTime;
            if (resetTimer >= respawnTimer) 
            {
                canSpawn = true ;
                resetTimer = 0 ;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canSpawn == true)
        {
            for (int i = 0; i < zombieSpawnAmt; i++) 
            {
                Instantiate(zombies[Random.Range(0, zombies.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.rotation);
            }
            canSpawn = false;   
        }
    }
}
