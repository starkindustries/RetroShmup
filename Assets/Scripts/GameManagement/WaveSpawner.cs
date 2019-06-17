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
        public FlightPathName flightPath;
        public int flightParam = 0;
        public int enemyCount;
        public float delayBeforeWave;
        public float delayBetweenEnemies;        
        public float delayAfterWave;
    }

    public Wave[] waves;

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
        // Spawn all enemy waves!
        for(int i=0; i < waves.Length; i++)
        {
            Wave currentWave = waves[i];
            yield return new WaitForSeconds(currentWave.delayBeforeWave);
            Debug.Log("Starting wave: " + currentWave.name);
            for (int j=0; j < currentWave.enemyCount; j++)
            {
                GameObject newEnemy = Instantiate(currentWave.enemy);
                Enemy enemyComponent = newEnemy.GetComponent<Enemy>();

                Vector2[] path = FlightPath.GetFlightPath(currentWave.flightPath, currentWave.flightParam);
                enemyComponent.SetFlightPath(path);
                yield return new WaitForSeconds(waves[i].delayBetweenEnemies);
            }
            yield return new WaitForSeconds(currentWave.delayAfterWave);
        }

        // Wait for all enemies to die (if player survives, that is)
        while(GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            yield return new WaitForSeconds(1f);
        }

        // All enemies cleared! Level completed!
        GameManager.Instance.LevelComplete();
        yield break;        
    }
}
