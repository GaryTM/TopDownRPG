using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //A float to control the speed of the players movement
    public float speed;
    /*Creating a reference to the animator attached to the player character which 
     * allows the animations to be controlled from this class*/
    Animator animator;
    /*Creating an array of Images to handle the toggling of player health hearts in the game*/
    public Image[] hearts;
    /*An integer to limit the maximum health achievable by the player*/
    public int maxHealth;
    /*An integer to keep track of the players current health value*/
    int currentHealth;
    void Start()
    {
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        /*Calling the Movement method in Update ensures it will be called every frame
         * meaning the player can move consistently based on user input*/
        Movement();
    }

    void Movement()
    {
        /*Checking for keypresses and moving the player*/
        if (Input.GetKey(KeyCode.W))
        {
            /*transform.Translate takes the player sprites transform and adds the values within
             * the parentheses to it. Using speed * Time.deltaTime ensures the players movement
             * will remain consitent regardless of hardware as deltaTime is the time since the last frame.
             * The integers within the animator are then used to change the way the character is facing
             * based on the values I defined in Unity*/
            transform.Translate(0, speed * Time.deltaTime, 0); animator.SetInteger("Direction", 0); animator.speed = 1;
        }
       else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0); animator.SetInteger("Direction", 1); animator.speed = 1;
        }
       else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0); animator.SetInteger("Direction", 2); animator.speed = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0); animator.SetInteger("Direction", 3); animator.speed = 1;
        }
        else animator.speed = 0;
    }
}
