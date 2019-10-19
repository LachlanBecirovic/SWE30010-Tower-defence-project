using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    //Public variables (visible to editor)
    public GameObject enemyPrefab; //Enemy prefab/gameobject to spawn
    //TODO: Add more enemy types here.
    public Transform spawnPoint; //Where to spawn enemies from;
    public float waveCooldown = 10f; //Variable to control time between each wave. Decrease this during testing!
    public static int enemyAliveCount = 0; //Static so enemies can modify variable on their death/reaching the end

    //Private variables
    private float countdownTimer;
    private int waveNumber;

    //Method called at the instantiation of the scene
    void Start()
    {
        //Reset variables to starting values
        enemyAliveCount = 0;
        countdownTimer = waveCooldown; //Set the time until the wave begins to whatever set in the editor via the public variable
    }

    //Method called each frame from within the game
    void Update()
    {
        //Is a wave in progress? (i.e. are there still enemies alive?)
        if (enemyAliveCount < 1)
        {
            //If there are no enemies alive now, we can start counting down till the next wave.
            countdownTimer -= Time.deltaTime;

            //Check if countdown timer has now finished (coundownTimer <= 0)
            if (countdownTimer <= 0) //Using <= incase it "misses" 0 and therefore == never evaluates true
            {
                //The countdown is 0, the next wave should begin!

                //https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
                //Coroutines should be used with yield returns for the enemy spawn timer
                StartCoroutine(SpawnWave());
                countdownTimer = waveCooldown; //Reset countdown timer
            }

            //TODO: Add a countdown timer string for the player to see time before next wave begins
            //Add on the GUI display or such
        }
    }

    //Method to spawn a new wave of enemies
    //https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
    //Enumerator is used to pause function for a set amount of time
    IEnumerator SpawnWave()
    {
        waveNumber++; //Increment the wave number by one at the start of each wave.

        //Calculate enemy count for this wave.
        int enemyCount = 3 * waveNumber + 4;
        Debug.Log("Wave #" + waveNumber + " has begun -- spawning " + enemyCount + " enemies."); //Debug log to show wave info.

        //Spawn number of enemies based off of the variable above
        for (int i = 1; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); //Wait half a second between each enemy spawn
        }
    }

    //Enemy spawning placed
    void SpawnEnemy()
    {
        enemyAliveCount++;
        //Spawn enemy via prefab at whatever location set by the map's spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
