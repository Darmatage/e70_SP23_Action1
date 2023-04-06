using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hunt;
    public float target_X;
    public float target_Y;

    public float speed;
    public float attack;
    public float health;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hunt) targeting();
        tracking();
        
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
