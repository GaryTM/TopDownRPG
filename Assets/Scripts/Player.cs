using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /*A float to control the speed of the players movement*/
    public float speed;
    /*A float to control the speed of the players sword thrust*/
    public float thrustAmount;
    /*Creating a reference to the animator attached to the player character which 
     * allows the animations to be controlled from this class*/
    Animator animator;
    /*Creating an array of Images to handle the toggling of player health hearts in the game*/
    public Image[] hearts;
    /*An integer to limit the maximum health achievable by the player*/
    public int maxHealth;
    /*An integer to keep track of the players current health value*/
    int currentHealth;
    /*Creating a reference to the sword the player will use*/
    public GameObject sword;
    /*A boolean to check if they player can move or not..*/
    public bool canMove;
    /*A boolean to ensure there can only be one sword active at a time*/
    public bool canAttack;
    /*The start method is used to initialise everything required at the START of the game (mind = blown)*/
    void Start()
    {
        /*This is how we get the reference to the Animator*/
        animator = GetComponent<Animator>();
        /*Setting the players current health to equal the maximum at the start of the game*/
        currentHealth = maxHealth;
        /*Calling GetHealth in case the players health is changed in the inspector*/
        GetHealth();
        /*Setting canMove to true when the game starts*/
        canMove = true;
        /*Setting canAttack to true when the game starts*/
        canAttack = true;
    }
    /*A method to get the players current health and draw the correct amount of hearts to the screen*/
    void GetHealth()
    {
        /*Looping through and disabling all of the heart sprites*/
        for (int i = 0; i <= hearts.Length - 1; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
        /*Looping through and drawing the correct amount of hearts based on the players health*/
        for (int i = 0; i <= currentHealth - 1; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }
    void Update()
    {
        /*Calling the Movement method in Update ensures it will be called every frame
         * meaning the player can move consistently based on user input*/
        Movement();
        GetHealth();
        /*Ensuring the players health can never go above the max health value by
         * checking if it has and if so, setting it to equal max health*/
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Attack();
        }
    }
    /*A method which contains the code for the players attacks*/
    void Attack()
    {
        /*Checking if canAttack is false and if so, ensuring the Attack code doesn't execute*/
        if (canAttack == false)
            return;
        /*The player should not be able to move when they are attacking*/
        canMove = false;
        /*Setting canAttack to be false as soon as the player has attacked*/
        canAttack = false;
        /*Creating a new GameObject when the player attacks by instantiating the sword,
         * setting its position and then setting its rotation*/
        GameObject newSword = Instantiate(sword, transform.position, sword.transform.rotation);
        /*Checking if the players health is at the maximum available and if so allowing the special attack to be used*/
        if (currentHealth == maxHealth)
        {
            newSword.GetComponent<Sword>().specialAttack = true;
            canMove = true;
            /*Also making the sword faster when using the special attack*/
            thrustAmount = 400;
        }
        #region /*SwordRotation*/
        /*swordDirection is a temporary variable only available within the Attack method and is used
         * to handle the rotation of the sword when it is spawned in the game.*/
        int swordDirection = animator.GetInteger("Direction");
        /*Setting the animators attack direction integer to equal the swordDirection since the player
         * can only attack in the way it is facing*/
        animator.SetInteger("AttackDirection", swordDirection);
        /*Using the same Direction integer I created previously for the players movement animations*/
        if (swordDirection == 0) /* 0 = Up*/
        {
            newSword.transform.Rotate(0, 0, 0);
            /*Getting the RigidBody2D component which is attached to the sword in Unity
             * this allows control of all kinds of physics which can be applied to the sword.
             * In this case I am using .AddForce() to thrust the sword in a direction.*/
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustAmount);
        }
        else if (swordDirection == 1) /* 1 = Down */
        {
            newSword.transform.Rotate(0, 0, 180);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustAmount);
        }
        else if (swordDirection == 2) /* 2 = Left */
        {
            newSword.transform.Rotate(0, 0, 90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustAmount);
        }
        else if (swordDirection == 3) /* 3 = Right */
        {
            newSword.transform.Rotate(0, 0, -90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustAmount);
        }
        #endregion
    }
    /*A method which contains the code required for the players movement*/
    void Movement()
    {
        /*Ensures all code stops executing if canMove is false*/
        if (canMove == false)
            return;
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
