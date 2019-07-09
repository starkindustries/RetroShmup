using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Random enemy spawner initialized!");
        SpawnRandomEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRandomEnemy()
    {
        // Choose a random enemy index
        int index = Random.Range(0, enemies.Length);

        // create a random spawn point
        float yCoordinate = Random.Range(-3f, 3f);

        // Random flight path

        // Spawn the enemy
        Instantiate(enemies[index], new Vector3(x: 12, y: yCoordinate, z: transform.position.z), Quaternion.identity);
    }
}
