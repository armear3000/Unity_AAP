using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject system_parc;
    

    private void OnCollisionEnter2D(Collision2D other) {
        
        
        if(other.gameObject.tag == "Asteroid"){
          
            bullet.GetComponent<Renderer>().enabled = false;
            bullet.GetComponent<Collider2D>().enabled = false;

            var temp = bullet.GetComponent<ParticleSystem>().emission;
            temp.rateOverTime = 0f;

            Destroy(bullet, 0.5f);
            
            GameObject sp = Instantiate(system_parc, bullet.transform.position, bullet.transform.rotation) as GameObject;
            
            Destroy(sp, 1f);

            other.gameObject.GetComponent<Asteroidmove>().life_xp--;

        }

    }
   

}
