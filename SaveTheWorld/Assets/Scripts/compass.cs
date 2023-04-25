using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class compass : MonoBehaviour
{
    private Transform origin;
    private Transform target;
    private float angle;
    private Vector3 null_vector = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        origin = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        target = GameObject.FindGameObjectWithTag ("Door").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(target.position.y - origin.position.y, target.position.x - origin.position.x) * Mathf.Rad2Deg -90f;
        Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        transform.position = origin.position + front *4.0f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
