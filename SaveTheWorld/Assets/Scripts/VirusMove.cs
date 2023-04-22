using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virusmove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isfired;
    public float speed;
    public float angle;
    public float range;
    private Transform target;
    private Vector3 origin;
    private int life = 100;
    public GameHandler gameHandler;

    void Start()
    {
        Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        transform.position = origin + front *1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 99) gameHandler.shots_fired();
        if(life <= 0) Destroy (gameObject);
        life--;
        Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        transform.position = transform.position + hvMove * 15.0f * Time.deltaTime;

    }

    public void zombie_spit(Vector3 start_location, float orientation)
    {
        origin = start_location;
        angle = orientation;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "CheckPoint" && collision.gameObject.tag != "Pickup") 
        {
            Destroy(gameObject);
        }
    }
}
