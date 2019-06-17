using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int flightIndex;
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
        for(int i=0; i < waves.Length; i++)
        {
            Wave currentWave = waves[i];
            yield return new WaitForSeconds(currentWave.delayBeforeWave);
            for (int j=0; j < currentWave.enemyCount; j++)
            {
                GameObject newEnemy = Instantiate(currentWave.enemy);
                Enemy enemyComponent = newEnemy.GetComponent<Enemy>();

                Vector2[] path = FlightPath.GetFlightPathByIndex(currentWave.flightIndex, currentWave.flightParam);
                enemyComponent.SetFlightPath(path);
                yield return new WaitForSeconds(waves[i].delayBetweenEnemies);
            }
            yield return new WaitForSeconds(currentWave.delayAfterWave);
        }        
        yield break;
    }
}
