using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour {
    /*A timer to ensure particle effects stop when they should*/
    public float timer;
	void Start ()
    {
		
	}

	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
	}
}
