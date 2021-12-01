using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMap : MonoBehaviour
{
    public GameObject map;
    private GameObject temp_map, temp_map_next;
    public float speed = 0.05f;

    float size_x_2;

    float boind_left;

    private void Start() {
        temp_map = Instantiate(map, new Vector3(0f,0f,1f), new Quaternion(0,0,0,0));
        size_x_2 = temp_map.GetComponent<SpriteRenderer>().size.x;
        temp_map_next = Instantiate(map, new Vector3(size_x_2 -0.01f,0f,1f), new Quaternion(0,0,0,0));
    }

    private void FixedUpdate() {
        boind_left = Camera.main.transform.position.x - Camera.main.orthographicSize / Screen.height * Screen.width;
       
        temp_map.transform.localPosition = new Vector3(temp_map.transform.localPosition.x - speed, 0f,1f);
        temp_map_next.transform.localPosition = new Vector3(temp_map_next.transform.localPosition.x - speed, 0f,1f);

        if(temp_map.transform.position.x + size_x_2/2f< boind_left)
            temp_map.transform.localPosition = new Vector3(temp_map_next.transform.localPosition.x + size_x_2, 0f,1f);

        if(temp_map_next.transform.position.x + size_x_2/2f < boind_left)
            temp_map_next.transform.localPosition = new Vector3(temp_map.transform.localPosition.x + size_x_2, 0f,1f);
    }
}
