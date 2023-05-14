using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour
{
    public GameHandler gameHandler;
    private Transform target;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameHandler.playerGetHit(1);
            Vector3 hvMove = new Vector3((float)Math.Cos((angle) / Mathf.Rad2Deg), (float)Math.Sin((angle)/ Mathf.Rad2Deg), 0.0f);
            target.transform.position = target.transform.position + hvMove * 50.0f * Time.deltaTime;
        }
    }
}
