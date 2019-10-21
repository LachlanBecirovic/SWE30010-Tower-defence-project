using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Public variables
    public float startingSpeed = 6f;
    public float startingHealth = 100f;
    public int moneyValue = 10;

    //Hidden Variables
    [HideInInspector] public float healthValue;
    [HideInInspector] public float moveSpeed;

    //Private variables
    private bool deathState; //Bool here to make sure that the enemy can't activate the death method twice (and award twice the money to the player)
    private Color currentColour;

    // Start is called before the first frame update
    void Start()
    {
        //Replace the starting speed value with the speed set by the wave.
        //Health increases by 1% per wave. Wave 1 = 100% health and speed, Wave 50 = 150% health and speed, etc.
        float healthScalingMultiplier = 1f + ((float) WaveSpawner.waveNumber - 1f) / 100f;
        moveSpeed = startingSpeed;
        healthValue = startingHealth * healthScalingMultiplier;

        //The enemy is alive!!!
        deathState = true;

        //Get the base colour for the enemy object.
        currentColour = GetCurrentSpriteColour();
    }

    //Method to call if enemy is hit with a projectile from an enemy tower.
    public void TakeDamage(float damageNum)
    {
        
        //Subtract the current health value with the damage inflicted by the projectile.
        healthValue -= damageNum; 

        if (healthValue <= 0 && !deathState)
        {
            Die();
        }
        else
        {
            //Since they haven't died, they instead should flash a colour to indicate they've taken some damage.
            StartCoroutine(damageFlash());
        }
    }

    void SetSpriteColour(Color c)
    {
        GetComponentInChildren<SpriteRenderer>().color = c;
    }

    Color GetCurrentSpriteColour()
    {
        return GetComponentInChildren<SpriteRenderer>().color;
    }

    //Credit for method:
    //https://www.reddit.com/r/Unity2D/comments/8xcw8g/how_can_i_make_a_sprite_blink_in_white_when/e233laz/
    IEnumerator damageFlash()
    {
        for (int i = 0; i < 3; i++)
        {
            SetSpriteColour(Color.white);
            yield return new WaitForSeconds(0.1f);
            SetSpriteColour(currentColour);
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected virtual void Die()
    {
        deathState = true;

        //TODO add money to player.

        //Reduce the alive enemy count by one for the wave spawner to know when all enemies are dead/present.
        WaveSpawner.enemyAliveCount--;

        Destroy(gameObject); //Destroys the current enemy object.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
