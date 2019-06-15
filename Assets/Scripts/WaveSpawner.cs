using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public int count;
        public float delayBetweenEnemies;
    }

    public Transform[] spawnLocations;
    public GameObject enemy;
    public Wave[] waves;
    public float delayBetweenWaves;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyWaves()
    {
        for(int i=0; i < waves.Length; i++)
        {
            Wave currentWave = waves[i];
            for(int j=0; j < currentWave.count; j++)
            {
                int spawnLocationIndex = j % spawnLocations.Length;
                GameObject newEnemy = Instantiate(currentWave.enemy, spawnLocations[spawnLocationIndex].position, Quaternion.identity);
                newEnemy.transform.Rotate(0f, 180f, 0f);
                newEnemy.GetComponent<Rigidbody2D>().AddForce(newEnemy.transform.right * 50f);
                yield return new WaitForSeconds(waves[i].delayBetweenEnemies);
            }
            yield return new WaitForSeconds(delayBetweenWaves);
        }        
        yield break;
    }
}
