using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*The particle effect used when the bullet is destroyed*/
    public GameObject projectileParticleEffect;
    float timer = 1.5f;
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            CreateParticle();
            Destroy(gameObject);
        }
	}
    /*A method used to create the particle effects when neccessary*/
    public void CreateParticle()
    {
        Instantiate(projectileParticleEffect, transform.position, transform.rotation);
    }
}
