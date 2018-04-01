using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour {

    /*An integer to store the crabs health*/
    public int health;
    /*An int for the direction*/
    int direction;
    /*A float for the crabs speed*/
    public float speed;
    /*A timer float*/
    float timer = 1.5f;
    /*The crab death particle effect*/
    public GameObject deathParticleEffect;
    /*A reference to the SpriteRenderer allowing the sprites which are drawn to be switched*/
    SpriteRenderer spriteRenderer;
    /*A Sprite for the crab facing each way*/
    public Sprite facingUp;
    public Sprite facingDown;
    public Sprite facingLeft;
    public Sprite facingRight;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = Random.Range(0, 3);
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            /*Setting the direction the crab will move in to a random number between 0 and 3
             * giving it some very basic A.I.*/
            timer = 1.5f;
            direction = Random.Range(0, 3);
        }
        Movement();
	}
    /*Controls the crabs movements*/
    void Movement()
    {
        if (direction == 0)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            spriteRenderer.sprite = facingDown;
        }
        else if (direction == 1)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            spriteRenderer.sprite = facingUp;
        }
        else if (direction == 2)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            spriteRenderer.sprite = facingLeft;
        }
        else if (direction == 3)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            spriteRenderer.sprite = facingRight;
        }
    }
    /*Checking if the sword collides with the crab. If it does the crabs health is reduced by 1
     * and if the health is 0 or less the crab is deleted as is the sword*/
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            health--;
            if (health <= 0)
            {
                /*Playing the particle effect before the crab is destroyed*/
                Instantiate(deathParticleEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                /*Calling the create particle method from the sword script*/
                collision.GetComponent<Sword>().CreateParticle();
                /*Ensuring the player can attack again*/
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
                Destroy(collision.gameObject);
            }
        }
    }
    /*Checking if the crab collides with the player and ensuring health is removed*/
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*Decreasing the crab health by 1*/
            health--;
            /*Decreasing the players health by getting the component of the script*/
            collision.gameObject.GetComponent<Player>().currentHealth--;
            /*Removing the crab if health falls to 0*/
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
