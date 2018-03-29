using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    /*A timer to ensure the sword can't be spammed!*/
    float timer = 0.15f;
    /*And one for the special attack*/
    float specialTimer = 1.0f;
    /*A boolean to check if the sword should become a projectile!*/
    public bool specialAttack;
    /*Declaring the particle effect for the sword destruction*/
    public GameObject swordParticleEffect;
    void Start()
    {
    }

    void Update()
    {
        /*Decreasing the timer over time*/
        timer -= Time.deltaTime;
        /*Setting the directional animation to play when the player cannot move for 0.15 seconds due to timer being <= 0*/
        if (timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetInteger("AttackDirection", 5);
        }
        /*If the special attack is not available then the basic attack code will be executed*/
        if (specialAttack == false)
            if (timer <= 0)
            {
                /*This searches Unity for the game object which has the tag "Player" and gets the component
                 * of the player script allowing the public variables within that script to be accessed here.
                 * By setting canMove to true when the timer is less than or equal to zero this ensures the players 
                 * movement won't be limited when it shouldn't be*/
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
                /*This will remove the sword from the scene when the condition is true*/
                Destroy(gameObject);
            }
        /*Decreasing the timer over time*/
        specialTimer -= Time.deltaTime;
        if (specialTimer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            Instantiate(swordParticleEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    /*A method to ensure particle effects are played during collisions etc*/
    public void CreateParticle()
    {
        Instantiate(swordParticleEffect, transform.position, transform.rotation);
    }
}
