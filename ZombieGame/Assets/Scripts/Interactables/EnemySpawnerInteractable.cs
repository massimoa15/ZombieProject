using System.Collections;
using Entities;
using Interactables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    public class EnemySpawnerInteractable : Interactable
    {
        public GameObject enemy;        //Enemy prefab to be spawned
        //spawnable positions
        private Vector2[] positions = new []{new Vector2(5,5), new Vector2(5,-5), new Vector2(-5,-5), new Vector2(-5,5)};
        
        private int numToSpawn = 4;         //How many enemies need to be spawned

        private float timeBetweenSpawnsDelay = 0.5f;

        private static int numRemEnemies = 0;

        private SpriteRenderer sRenderer;

        private void Start()
        {
            sRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            //If there are no more enemies remaining, the wave is over. Less than 0 in case something goes wrong with counting
            if (numRemEnemies <= 0)
            {
                //Wave is over
                GlobalData.IsWaveActive = false;
                
                sRenderer.enabled = true;
            }
        }

        //This will be called at the start of each wave when enemies need to be spawned
        public void SpawnWave()
        {
            //Add the number that will be spawned to the number of remaining enemies
            numRemEnemies += numToSpawn;
            
            GlobalData.IncrementWaveNum();
            
            //Only do the spawning if the player exists
            StartCoroutine(WaitThenSpawnWaveCoroutine(timeBetweenSpawnsDelay));
        }

        public void SpawnEnemy()
        {
            int index = Random.Range(0, positions.Length);
            Vector3 spawnPos = positions[index];
            spawnPos.z = 0;
            GameObject enemySpawned = Instantiate(enemy, spawnPos, Quaternion.identity);
            print("Spawned at " + spawnPos);
            
            //Make sure the enemy is visible
            enemySpawned.SetActive(true);
            
        }

        /// <summary>
        /// Spawns a wave of enemies with delay between each enemy
        /// </summary>
        /// <param name="delay">amount of time to wait between spawning each enemy</param>
        /// <returns></returns>
        IEnumerator WaitThenSpawnWaveCoroutine(float delay)
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                yield return new WaitForSeconds(delay);
                SpawnEnemy();
            }
        }

        /// <summary>
        /// Decreases the remaining enemy counter by 1
        /// </summary>
        public static void DecreaseRemEnemies()
        {
            numRemEnemies--;
        }

        /// <summary>
        /// Gets the number of remaining enemies according to the counter
        /// </summary>
        /// <returns></returns>
        public static int GetNumRemEnemies()
        {
            return numRemEnemies;
        }

        public override void Interact(MBPlayer player)
        {
            if (numRemEnemies <= 0)
            {
                //Starting a wave
                GlobalData.IsWaveActive = true;
                
                sRenderer.enabled = false;
                SpawnWave();
            }
        }
    }
}
