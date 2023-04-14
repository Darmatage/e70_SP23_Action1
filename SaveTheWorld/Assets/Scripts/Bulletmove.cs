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

    void Start()
    {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        transform.rotation = target.transform.rotation;
        angle = (float)target.transform.eulerAngles.z;
        Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        transform.position = target.transform.position + front *1.0f;
        origin = transform.position;
        isfired = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.rotation = target.transform.rotation;
        //angle = (float)target.transform.eulerAngles.z;
        //Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        //transform.position = transform.position + hvMove * 5.0f * Time.deltaTime;

        /**
        if (Input.GetMouseButtonDown(0) && !isfired)
        {
            isfired = true;
            transform.rotation = target.transform.rotation;
            angle = (float)target.transform.eulerAngles.z;
            Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
            transform.position = target.transform.position + front *1.0f;
            origin = transform.position;
        }**/

        float dist = Vector3.Distance(origin, transform.position);
        if(dist > 15.0f) 
        {
            isfired = false;
            Destroy (gameObject);
        }

        //if(isfired)
        //{
            Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
            transform.position = transform.position + hvMove * 10.0f * Time.deltaTime;
        //}


        /**
        Leo's notes:
        pseudocode
        if isfired:
            bullet pos += speed* <cos(angle), sin(angle)>
        **/
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy (gameObject);
    }

    void fired()
    {
        /** Leo's notes
        if !isfired && can fire:
            bullet position = player position
            bullet angle = player angle
            bullet speed depends on the player
        **/
    }
}
