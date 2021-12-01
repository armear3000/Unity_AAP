    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ship;
    public float speed = 1f;
    public float speed_rotate = 0.2f;
    public GameObject[] trails_marsh, trails_left, trails_right, trails_back, trails_rotate_left, trails_rotate_right;
    public GameObject bullet;
    public GameObject[] start_bullet;
    public float reload_delay = 0.2f;
    public float life_over_time = 2f;
    public float bullet_impulse = 30f;


    private bool fire_rate = true;

    private Vector2 target_vector;
    private Vector3 mouse_position;

    private void Start() {
        
    }
    private void FixedUpdate() {
        player_controll();
    }
    
    private void player_controll(){
        mouse_position = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target_vector = mouse_position - ship.transform.position;
        target_vector.Normalize();
        
        float rotation_z = Mathf.Atan2(target_vector.y, target_vector.x) * Mathf.Rad2Deg;
        ship.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90f); 
        
        if(Input.GetMouseButton(0)){
            fire(fire_rate);
        }
        if(Input.GetKey("w")){
            ship.GetComponent<Rigidbody2D>().AddForce(target_vector * speed);
            engine_state(trails_marsh,true);
        } else {
            engine_state(trails_marsh,false);
        }

        if(Input.GetKey("s")){
            ship.GetComponent<Rigidbody2D>().AddForce(-target_vector * speed);
            engine_state(trails_back,true);
        } else {
            engine_state(trails_back,false);
        }

        if(Input.GetKey("a")){
            Vector2 vec = new Vector2(-target_vector.y, target_vector.x);
            ship.GetComponent<Rigidbody2D>().AddForce(vec * speed);
            engine_state(trails_right,true);
            engine_state(trails_rotate_left,true);
        } else {
            engine_state(trails_right,false);
            engine_state(trails_rotate_left,false);
        }

        if(Input.GetKey("d")){
            Vector2 vec = new Vector2(target_vector.y, -target_vector.x);
            ship.GetComponent<Rigidbody2D>().AddForce(vec * speed);
            engine_state(trails_left,true);
            engine_state(trails_rotate_right,true);
        } else {
            engine_state(trails_left,false);
            engine_state(trails_rotate_right,false);
        }
        
        
        float Bs = 0.1f,Bo = 1.1f;

        float boind_left = Camera.main.transform.position.x - Camera.main.orthographicSize / Screen.height * Screen.width - Bo;
        float boind_right  = Camera.main.transform.position.x + Camera.main.orthographicSize / Screen.height * Screen.width + Bo;
        float boind_bottom = Camera.main.transform.position.y - Camera.main.orthographicSize - Bo;
        float boind_top = Camera.main.transform.position.y + Camera.main.orthographicSize + Bo;
            
        if(ship.transform.position.y <= boind_bottom)
            ship.transform.SetPositionAndRotation(new Vector3(ship.transform.position.x, boind_top - Bs, 0f),new Quaternion(0,0,0,0));
        else if(ship.transform.position.y >= boind_top)
            ship.transform.SetPositionAndRotation(new Vector3(ship.transform.position.x, boind_bottom + Bs, 0f),new Quaternion(0,0,0,0));

        if(ship.transform.position.x <= boind_left)
            ship.transform.SetPositionAndRotation(new Vector3(boind_right - Bs, ship.transform.position.y, 0f),new Quaternion(0,0,0,0));
        else if(ship.transform.position.x >= boind_right)
            ship.transform.SetPositionAndRotation(new Vector3(boind_left + Bs, ship.transform.position.y, 0f),new Quaternion(0,0,0,0));
    }
    
    
    private void fire(bool fire){
        if(fire){
            for(int i = 0; i < start_bullet.Length; i++){
                Vector2 spawn_point = start_bullet[i].transform.position;
                Quaternion spawn_rote = start_bullet[i].transform.rotation;
                
                GameObject bullet_shot = Instantiate(bullet, spawn_point, spawn_rote) as GameObject;
                Rigidbody2D bullet_rb = bullet_shot.GetComponent<Rigidbody2D>();
                
                bullet_rb.AddForce(bullet_shot.transform.up * bullet_impulse, ForceMode2D.Impulse);

                var bullet_collide = bullet_shot.GetComponent<Collider2D>();
    
                Destroy(bullet_shot, life_over_time);
            }
            StartCoroutine(bullet_reload(reload_delay));
        }
   }

    private void engine_state(GameObject[] trails, bool state){
        if(state){
            for(int i = 0; i < trails.Length; i++){
                var temp = trails[i];
                temp.GetComponent<SpriteRenderer>().enabled = true;
                var temp2 = temp.transform.Find("System Partycle").gameObject.GetComponent<ParticleSystem>().emission;
                temp2.rateOverTime = 60f;
            }
        } else {
           for(int i = 0; i < trails.Length; i++){
                var temp = trails[i];
                temp.GetComponent<SpriteRenderer>().enabled = false;
                var temp2 = temp.transform.Find("System Partycle").gameObject.GetComponent<ParticleSystem>().emission;
                temp2.rateOverTime = 0f;
            }
        }
    }

    IEnumerator bullet_reload(float time){
        fire_rate = false;
        yield return new WaitForSeconds(time);      
        fire_rate = true;
    }

}

