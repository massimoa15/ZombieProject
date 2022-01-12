using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Environment;
using Global;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Interactables
{
    public class EnemySpawnerInteractable : Interactable
    {
        public EnemyWeight[] enemies;        //Enemy prefab to be spawned
 
        //This will hold all of the enemies and their spawning weights. This will be completely controlled in the inspector
        [Serializable]
        public struct EnemyWeight
        {
            public GameObject enemyObj;
            public int weight;
        }

        //spawnable positions
        private Vector2[] positions = {new Vector2(5,5), new Vector2(5,-5), new Vector2(-5,-5), new Vector2(-5,5)};
        
        private int numToSpawn = 4;         //How many enemies need to be spawned

        private float timeBetweenSpawnsDelay = 0.5f;

        private static int numRemEnemies = 0;

        private SpriteRenderer sRenderer;

        private int totalWeight;

        public GameObject canvas;
        public GameObject healthBarPrefab;

        private void Start()
        {
            sRenderer = GetComponent<SpriteRenderer>();
            //Also determine the total weight by summing the weights in the enemies array
            totalWeight = 0;
            foreach (EnemyWeight item in enemies)
            {
                totalWeight += item.weight;
            }
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
        private void SpawnWave()
        {
            //Update numToSpawn based on the wave num
            Debug.Log("Currently spawning 2*waveNum enemies per wave");
            numToSpawn = 2 * GlobalData.GetWaveNum();

            //Add the number that will be spawned to the number of remaining enemies
            numRemEnemies += numToSpawn;
            
            GlobalData.IncrementWaveNum();
            
            //Only do the spawning if the player exists
            StartCoroutine(WaitThenSpawnWaveCoroutine(timeBetweenSpawnsDelay));
        }

        /// <summary>
        /// Spawns an enemy
        /// </summary>
        private void SpawnEnemy()
        {
            int index = Random.Range(0, positions.Length);
            Vector3 spawnPos = positions[index];
            spawnPos.z = 0;
            //Randomly choose an enemy to spawn from the array of enemy types
            GameObject tempEnemy = ChooseRandomEnemy(enemies);
            GameObject enemySpawned = Instantiate(tempEnemy, spawnPos, Quaternion.identity);
            
            //Make sure the enemy is visible
            enemySpawned.SetActive(true);
            
            //Now, instantiate a health bar for this enemy
            GameObject healthBar = Instantiate(healthBarPrefab, canvas.transform);
            
            //Save the Slider of the healthBar in the enemySpawned MBEnemy script
            enemySpawned.GetComponent<MBEnemy>().SetSlider(healthBar);
            
            //Make sure the healthBar starts as not visible
            healthBar.SetActive(false);
            
            //Set object in the FollowObject script of the healthBar
            healthBar.GetComponent<FollowObject>().SetFollowingObject(enemySpawned);

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

        /// <summary>
        /// Choose an enemy from the list using the weights stored for each in the EnemyWeight struct items
        /// </summary>
        /// <param name="enemies">The list of enemies we are choosing from</param>
        /// <returns>The randomly chosen enemy</returns>
        public GameObject ChooseRandomEnemy(EnemyWeight[] enemies)
        {
            int val = Random.Range(0, totalWeight);
            foreach (EnemyWeight item in enemies)
            {
                //The value is less than the item's weight so this is the enemy to choose
                if (val < item.weight)
                {
                    return item.enemyObj;
                }
                //Value is not less than the item's weight so decrease by that amount
                val -= item.weight;
            }

            //Should never reach here, if it does just return the first enemy in the list which should be the basic enemy
            return enemies[0].enemyObj;
        }
    }
}
