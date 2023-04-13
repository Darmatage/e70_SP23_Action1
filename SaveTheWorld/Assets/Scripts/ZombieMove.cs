using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieMove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hunt;
    public float target_X = 0;
    public float target_Y = 0;

    public float speed;
    public float attack;
    public float health;

    public float orientation = 10;
    public float num_orient = 360;

    private GameHandler gameHandler;
    private Transform target;
    private int framecount = 0;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        //Random rnd = new Random();
        framecount = (framecount+1)%25;
        double x_calc = Math.Sin(Math.PI*orientation/num_orient);
        double y_calc = Math.Cos(Math.PI*orientation/num_orient);

        double distance = Math.Sqrt(Math.Pow(transform.position.x - target_X,2) + Math.Pow(transform.position.y - target_Y,2));
        double DistToPlayer = Vector3.Distance(transform.position, target.position);

        if(DistToPlayer < 3)
        {
            target_X = target.position.x;
            target_Y = target.position.y;
        }

        if(distance > 2)
        {
            hunt = true;
        }

        if(hunt)
        {
            double x = (target_X - transform.position.x)/distance;
            double y = (target_Y - transform.position.y)/distance;
            
            //Vector3 target = new Vector3((float)x, (float)y, 0.0f);
            //float angle = Vector3.Angle(target, transform.position);
            if((y_calc > y + 0.1 || y_calc < y - 0.1)||(x_calc > x + 0.1 || x_calc < x - 0.1))
                orientation+=5;
            //orientation = (float)(Math.Asin(y));
            speed = 5;
            if(distance < 1)
            {
                hunt = false;
            }
        }
        else
        {
            speed = 0.1f;
            if(framecount == 0) orientation += (Math.Abs(orientation)%11 - 5)*5;
        }

        Vector3 hvMove = new Vector3((float)x_calc, (float)y_calc, 0.0f);
        transform.position = transform.position + hvMove * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, (float)(-180*orientation/num_orient));


        //if(hunt) targeting();
        //tracking();
        
    }

    void targeting()
    {
        /**
        Leo's notes
        target = player position
        targeting will happen in cases like, the player is seen or if the zombie is alerted
        **/
    }

    void tracking()
    {
        /**
        leo's notes
        if zombie is within an epsilon ball of target, it will move in accordance to a brownian motion
        if zombie is within an epsilon ball of an obstacle, it will move in accordance to a brownian motion
        else zombie will move to location
        **/
    }
}
