using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    /*Creating a reference to the animator attached to the dragon character which 
    * allows the animations to be controlled from this class*/
    Animator animator;
    /*A float to control the speed of the Dragons movement*/
    public float speed;
    /*A timer for direction changes*/
    float directionTimer = 0.75f;
    /*An int for the dragons health*/
    public int health;
    /*An int that will be used to change the direction of the dragon*/
    int direction;
    void Start ()
    {
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
        /*Randomising the dragons direction when the game starts*/
        direction = Random.Range(0, 3);

    }
	
	void Update ()
    {
        /*Reducing the timer*/
        directionTimer -= Time.deltaTime;
        /*Moving the dragon accordingly*/
        if (directionTimer <= 0)
        {
            directionTimer = 0.75f;
            direction = Random.Range(0, 3);
        }
        Movement();
	}
    /*A method which contains all code for the dragons movement*/
    void Movement()
    {
        /*if the dragon is moving up*/
        if (direction == 0)
        {
            /*transform.Translate takes the dragon sprites transform and adds the values within
             * the parentheses to it. Using speed * Time.deltaTime ensures the players movement
             * will remain consitent regardless of hardware as deltaTime is the time since the last frame.
             * The integers within the animator are then used to change the way the character is facing
             * based on the values I defined in Unity*/
            transform.Translate(0, speed * Time.deltaTime, 0); animator.SetInteger("Direction", direction); animator.speed = 1;
        }
        else if (direction == 1)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0); animator.SetInteger("Direction", direction); animator.speed = 1;
        }
        else if (direction == 2)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0); animator.SetInteger("Direction", direction); animator.speed = 1;
        }
        else if (direction == 3)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0); animator.SetInteger("Direction", direction); animator.speed = 1;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        /*Checking for collisions between the sword and the dragon*/
        if(collision.gameObject.tag == "Sword")
        {
            /*Creating the particle effect from the component of the sword class*/
            collision.gameObject.GetComponent<Sword>().CreateParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            /*Destroy the sword*/
            Destroy(collision.gameObject);
            health--;
            if (health <= 0)
            {
                /*Destroy the dragon*/
                Destroy(gameObject);
            }
        }
    }
}
