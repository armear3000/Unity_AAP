using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroidmove : MonoBehaviour
{
  public GameObject asteroid;
  
  private float boind_left;
  private float boind_right;
  private float boind_bottom;
  private float boind_top;
  
  public float speed = 100f;
  public int life_xp = 10;
  public GameObject[] spawn_after_death;
  public GameObject system_parc_death;
  public Vector3 scaled_sp;

  private float Bs = 0.1f,Bo = 1.1f;

  private void FixedUpdate() {
    
    float boind_left = Camera.main.transform.position.x - Camera.main.orthographicSize / Screen.height * Screen.width - Bo;
    float boind_right  = Camera.main.transform.position.x + Camera.main.orthographicSize / Screen.height * Screen.width + Bo;
    float boind_bottom = Camera.main.transform.position.y - Camera.main.orthographicSize - Bo;
    float boind_top = Camera.main.transform.position.y + Camera.main.orthographicSize + Bo;
    
    if(asteroid.transform.position.y <= boind_bottom)
      asteroid.transform.SetPositionAndRotation(new Vector3(asteroid.transform.position.x, boind_top - Bs, 0f),new Quaternion(0,0,0,0));
    else if(asteroid.transform.position.y >= boind_top)
      asteroid.transform.SetPositionAndRotation(new Vector3(asteroid.transform.position.x, boind_bottom + Bs, 0f),new Quaternion(0,0,0,0));

    if(asteroid.transform.position.x <= boind_left)
      asteroid.transform.SetPositionAndRotation(new Vector3(boind_right - Bs, asteroid.transform.position.y, 0f),new Quaternion(0,0,0,0));
    else if(asteroid.transform.position.x >= boind_right)
      asteroid.transform.SetPositionAndRotation(new Vector3(boind_left + Bs, asteroid.transform.position.y, 0f),new Quaternion(0,0,0,0));
    
    if(life_xp <= 0){
      death_spawn();
    }     

  }
  void Update()
  {
     
  }
  void death_spawn(){
    if(System.Convert.ToBoolean(spawn_after_death.Length)){
      GameObject asteroid_temp;
      for(int i = 0; i < UnityEngine.Random.Range(2, 4); i++){
        asteroid_temp = Instantiate(spawn_after_death[UnityEngine.Random.Range(0, spawn_after_death.Length)], new Vector3(asteroid.transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), asteroid.transform.position.y  + UnityEngine.Random.Range(-0.1f, 0.1f), asteroid.transform.position.z), new Quaternion(0,0,0,0));
        asteroid_temp.GetComponent<Rigidbody2D>().AddForce(asteroid.GetComponent<Rigidbody2D>().velocity * speed * asteroid_temp.GetComponent<Rigidbody2D>().mass / asteroid.GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);
        asteroid_temp.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(-1f, 1f), ForceMode2D.Impulse);
      }
      
    }
    GameObject sp = Instantiate(system_parc_death, asteroid.transform.position, new Quaternion(0,0,0,0)) as GameObject;
    sp.transform.localScale = scaled_sp;
    Destroy(sp, 1f);
    Destroy(asteroid);
  }

  private float random_to_array(float[] array){
      return array[UnityEngine.Random.Range(0,array.Length)];
  }

}
