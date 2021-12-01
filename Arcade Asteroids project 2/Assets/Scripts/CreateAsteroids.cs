using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CreateAsteroids : MonoBehaviour
{
    public GameObject[] asteroids_t1;
    public GameObject[] asteroids_t2;
    public GameObject[] asteroids_t3;

    public float asteroid_spawn_rate = 5f; 

    
    bool asteroid_spawn = true;

 


    
    private void FixedUpdate() {
        if(asteroid_spawn){
            asteroid_random_spawn();
            StartCoroutine(asteroid_spawn_reload());
        }
    }

    IEnumerator asteroid_spawn_reload(){
        asteroid_spawn = false;
        yield return new WaitForSeconds(asteroid_spawn_rate);      
        asteroid_spawn = true;
    }

    private void asteroid_random_spawn(){
        float boind_left = Camera.main.transform.position.x - Camera.main.orthographicSize / Screen.height * Screen.width - 1.1f;
        float boind_right  = Camera.main.transform.position.x + Camera.main.orthographicSize / Screen.height * Screen.width + 1.1f;
        float boind_bottom = Camera.main.transform.position.y - Camera.main.orthographicSize - 1.1f;
        float boind_top = Camera.main.transform.position.y + Camera.main.orthographicSize + 1.1f;
        
        float random_cor_x;
        float random_cor_y;
        
        if(System.Convert.ToBoolean(random_to_array(new float[]{0,1}))){
            random_cor_x = UnityEngine.Random.Range(boind_left, boind_right);
            random_cor_y = random_to_array(new float[]{boind_top,boind_bottom});
        } else {
            random_cor_x = random_to_array(new float[]{boind_left,boind_right}); 
            random_cor_y = UnityEngine.Random.Range(boind_bottom, boind_top);
        }
        

       GameObject asteroid_temp = null;

        switch (UnityEngine.Random.Range(0,3))
        {
            case 0:
                asteroid_temp = Instantiate(asteroids_t1[UnityEngine.Random.Range(0, asteroids_t1.Length)], new Vector3(random_cor_x, random_cor_y, 0f), new Quaternion(0,0,0,0));
                asteroid_temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) * asteroid_temp.GetComponent<Asteroidmove>().speed, ForceMode2D.Impulse);
                asteroid_temp.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(-1f, 1f) * 10f, ForceMode2D.Impulse);
                
            break;
            case 1:
                asteroid_temp = Instantiate(asteroids_t2[UnityEngine.Random.Range(0, asteroids_t2.Length)], new Vector3(random_cor_x, random_cor_y, 0f), new Quaternion(0,0,0,0));
                asteroid_temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) * asteroid_temp.GetComponent<Asteroidmove>().speed, ForceMode2D.Impulse);
                asteroid_temp.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(-1f, 1f) * 50f, ForceMode2D.Impulse);
            break;
            case 2:
                asteroid_temp = Instantiate(asteroids_t3[UnityEngine.Random.Range(0, asteroids_t3.Length)], new Vector3(random_cor_x, random_cor_y, 0f), new Quaternion(0,0,0,0));
                asteroid_temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) * asteroid_temp.GetComponent<Asteroidmove>().speed, ForceMode2D.Impulse);
                asteroid_temp.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(-1f, 1f) * 200f, ForceMode2D.Impulse);
            break;
        }
        
        
    }


    private float random_to_array(float[] array){
        return array[UnityEngine.Random.Range(0,array.Length)];
    }

}


