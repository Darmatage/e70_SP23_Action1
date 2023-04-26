using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletmove : MonoBehaviour
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
        if(!gameHandler.can_shoot()) Destroy (gameObject);
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        transform.rotation = target.transform.rotation;
        angle = (float)target.transform.eulerAngles.z;
        Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        transform.position = target.transform.position + front *1.0f;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "CheckPoint" && collision.gameObject.tag != "Pickup" && collision.gameObject.tag != "Water") 
        {
            Destroy(gameObject);
        }
    }
}
