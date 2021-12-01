using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aion1 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Asteroid"){   
            other.gameObject.GetComponent<Asteroidmove>().death_spawn();
            transform.parent.GetComponent<PlayerController>().CollisionDetected(this);
        }
    }

}
