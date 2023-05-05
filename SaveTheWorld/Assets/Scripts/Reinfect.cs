using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinfect : MonoBehaviour
{
    public float speed_x;
    public float speed_y;
    private float speed = 10;
    private int counter = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 hvMove = new Vector3(speed_x,speed_y,0);
        transform.position = transform.position + hvMove * speed * Time.deltaTime;
        counter --;
        if(counter <= 0) Destroy(gameObject);
    }
}
