using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    /*Creating a reference to the animator attached to the dragon character which 
    * allows the animations to be controlled from this class*/
    Animator animator;
    /*The dragon death particle effect*/
    public GameObject deathParticleEffect;
    /*The dragons projectile for attacking*/
    public GameObject projectile;
    /*A float to control the speed of the Dragons movement*/
    public float speed;
    /*A float for how powerful the dragons attack should travel*/
    public float firePower;
    /*A timer for direction changes*/
    float directionTimer = 0.75f;
    /*A float used to delay attacks, ensuring the dragon can't spam attack*/
    float attackTimer = 1.5f;
    /*An int for the dragons health*/
    public int health;
    /*An int that will be used to change the direction of the dragon*/
    int direction;
    /*A boolean to check if the enemy dragon is currently able to attack*/
    bool canAttack;
  
    void Start ()
    {
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
        /*Randomising the dragons direction when the game starts*/
        direction = Random.Range(0, 3);
        /*The dragon is unable to attack to begin with*/
        canAttack = false;
    }
	
	void Update ()
    {
        /*Reducing the timers*/
        directionTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        /*Moving the dragon accordingly*/
        if (directionTimer <= 0)
        {
            directionTimer = 0.75f;
            direction = Random.Range(0, 3);
        }
        /*Reset the attackTimer if it reaches 0 and set canAttack to true*/
        if (attackTimer <= 0)
        {
            attackTimer = 1.5f;
            canAttack = true;
        }
        Movement();
        Attack();
	}
    /*A method which contains all the code for the dragons attacks*/
    void Attack()
    {
        if (canAttack == false)
        {
            /*If the dragon cannot attack, don't run anymore code in this method*/
            return;
        }
        canAttack = false;
        if (direction == 0) /* 0 = Up */
        {
            /*Creating a new game object using instantiate allows the rigidbody to be manipulated in code*/
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            /*Getting the rigidbody of the projectile and firing it based on direction*/
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * firePower);
        }
        else if (direction == 1) /* 1 = Down*/
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -firePower);
        }
        else if(direction == 2) /* 2 = Left*/
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -firePower);
        }
        else if(direction == 3) /* 3 = Right */
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * firePower);
        }
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;
            /*Destroy the sword*/
            Destroy(collision.gameObject);
            health--;
            if (health <= 0)
            {
                /*Destroy the dragon*/
                Destroy(gameObject);
                /*Show the death particle effect*/
                Instantiate(deathParticleEffect, transform.position, transform.rotation);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*Decreasing the dragon health by 1*/
            health--;
            /*Checking if the players invincibility frames are not active then...*/
            if (collision.gameObject.GetComponent<Player>().invincibilityFrames == false)
            {
                /*Decreasing the players health by getting the component of the script*/
                collision.gameObject.GetComponent<Player>().currentHealth--;
                /*Activate the i frames*/
                collision.gameObject.GetComponent<Player>().invincibilityFrames = true;
            }
            /*Removing the dragon if health falls to 0*/
            if (health <= 0)
            {
                Instantiate(deathParticleEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        /*Checking the crabs collisions with walls and making it turn*/
        if (collision.gameObject.tag == "Wall")
        {
            direction = Random.Range(0, 3);
        }
    }
}
