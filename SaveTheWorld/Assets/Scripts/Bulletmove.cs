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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /**
        Leo's notes:
        pseudocode
        if isfired:
            bullet pos += speed* <cos(angle), sin(angle)>
        **/
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
