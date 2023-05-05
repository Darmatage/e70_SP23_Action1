using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isfired;
    public float speed;
    public float angle;
    public float range;
    private Transform target;
    private Vector3 origin;
    private int life = 500;
    private GameHandler gameHandler;

    void Start()
    {
        angle = (float)transform.eulerAngles.z;
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        //Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        //transform.position = origin + front *1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0) Destroy (gameObject);
        life--;
        Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        transform.position = transform.position + hvMove * 5.5f * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "CheckPoint" && collision.gameObject.tag != "Pickup" && collision.gameObject.tag != "Water" && collision.gameObject.tag != "Zombie") 
        {
            if(collision.gameObject.tag == "Player")
            {
                gameHandler.playerGetHit(1);
            }
            Destroy(gameObject);
        }
    }
}
