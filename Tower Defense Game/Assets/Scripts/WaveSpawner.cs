using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    //Public variables (visible to editor)
    public WaveType[] waveInfo;
    public Transform spawnPoint; //Where to spawn enemies from;
    public float waveCooldown = 10f; //Variable to control time between each wave. Decrease this during testing!
    public static int enemyAliveCount = 0; //Static so enemies can modify variable on their death/reaching the end
    public static int waveNumber;

    //Private variables
    private float countdownTimer;

    //Method called at the instantiation of the scene
    void Start()
    {
        //Reset variables to starting values
        enemyAliveCount = 0;
        countdownTimer = waveCooldown; //Set the time until the wave begins to whatever set in the editor via the public variable

        waveNumber = 0; //Reset the wave counter.
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

    [System.Serializable]
    //https://docs.unity3d.com/ScriptReference/Serializable.html
    //Serializing a class lets the Unity Editor embed the class properties into the editor.
    //This is done so we can edit each wave within the editor.
    //The class/object should contain the information related to each wave.
    //What enemy spawns, how often, how many
    public class WaveType
    {
        public GameObject enemyTypeForThisWave;
        //Multiplies the number of enemies to spawn by this value. 1 = 100% of normal, 0.5 = 50%.
        public float wavePercentage = 1.0f;
        public float delayBetweenSpawns = 0.8f;
    }

    //Method to spawn a new wave of enemies
    //https://docs.unity3d.com/ScriptReference/WaitForSeconds.html
    //Enumerator is used to pause function for a set amount of time
    IEnumerator SpawnWave()
    {
        if (waveNumber >= waveInfo.Length) //Have we gone through all the waves in the game?
        {
            //TODO declare a victory
        }
        else
        {
            waveNumber++; //Increment the wave number by one at the start of each wave.
        }

        WaveType currentWaveType = waveInfo[waveNumber - 1];

        //Calculate enemy count for this wave.
        int enemyCount = (int) Mathf.Floor((3 * waveNumber + 2) * currentWaveType.wavePercentage);
        Debug.Log("Wave #" + waveNumber + " has begun."); //Debug log to show wave info.

        //Spawn number of enemies based off of the count above
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(currentWaveType.enemyTypeForThisWave);
            yield return new WaitForSeconds(currentWaveType.delayBetweenSpawns); //Wait half a second between each enemy spawn
        }
    }

    //Enemy spawning placed
    void SpawnEnemy(GameObject enemyPrefab)
    {
        enemyAliveCount++;
        //Spawn enemy via prefab at whatever location set by the map's spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
