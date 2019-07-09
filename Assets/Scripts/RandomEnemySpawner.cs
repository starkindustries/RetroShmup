using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemies;
    public Vector2 delayBetweenEnemies;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Random enemy spawner initialized!");
        StartCoroutine(SpawnRandomEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnRandomEnemy()
    {
        for (int i=0; i < enemyCount; i++)
        {
            // Choose a random enemy index
            int index = Random.Range(0, enemies.Length);

            // create a random spawn point
            float yCoordinate = Random.Range(-3f, 3f);

            // Random flight path


            // Spawn the enemy
            Instantiate(enemies[index], new Vector3(x: 12, y: yCoordinate, z: transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(delayBetweenEnemies.x, delayBetweenEnemies.y));
        }        
    }
}
