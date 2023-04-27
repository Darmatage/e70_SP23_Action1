using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI; 

public class compass : MonoBehaviour
{
    private Transform origin;
    private Transform target;
    public GameObject[] nextlocation;
    public GameHandler gameHandler;
    public string[] narrations;
    private int location = 0;
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
        if(nextlocation.Length != 0)
        {
            GameObject temp = nextlocation[location];
            gameHandler.change_objectives(narrations[location]);
            //Text ammoTextTemp = DialogText.GetComponent<Text>();
            //ammoTextTemp.text = narrations[location];

            target = temp.transform;
            if(Vector3.Distance(origin.position, target.position) < 2 && location < nextlocation.Length-1)
            {
                location ++;
            }
        }

        float angle = Mathf.Atan2(target.position.y - origin.position.y, target.position.x - origin.position.x) * Mathf.Rad2Deg -90f;
        Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        transform.position = origin.position + front *2.5f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
