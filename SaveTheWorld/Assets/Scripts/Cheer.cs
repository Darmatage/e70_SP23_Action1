using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cheer : MonoBehaviour
{
    private int Identity;

    public Animator anim;

    private String Civilian_walk;
    private String Civilian_idle;
    private String Civilian_cheer;

    
    // Start is called before the first frame update
    void Start()
    {
        
        Identity = UnityEngine.Random.Range(1, 11);
        Civilian_walk = "Civilian1_walk";
        Civilian_idle = "Civilian1_idle";
        Civilian_cheer = "Civilian1_Cheer";

        if(Identity == 2)
        {
            Civilian_walk = "Civilian2_walk";
            Civilian_idle = "Civilian2_idle";
            Civilian_cheer = "Civilian2_Cheer";
        }
        if(Identity == 3)
        {
            Civilian_walk = "Civilian3_walk";
            Civilian_idle = "Civilian3_idle";
            Civilian_cheer = "Civilian3_Cheer";
        }
        if(Identity == 4)
        {
            Civilian_walk = "Civilian4_walk";
            Civilian_idle = "Civilian4_idle";
            Civilian_cheer = "Civilian4_Cheer";
        }
        if(Identity == 5)
        {
            Civilian_walk = "Civilian5_walk";
            Civilian_idle = "Civilian5_idle";
            Civilian_cheer = "Civilian5_Cheer";
        }
        if(Identity == 6)
        {
            Civilian_walk = "Civilian6_walk";
            Civilian_idle = "Civilian6_idle";
            Civilian_cheer = "Civilian6_Cheer";
        }
        if(Identity == 7)
        {
            Civilian_walk = "Civilian7_walk";
            Civilian_idle = "Civilian7_idle";
            Civilian_cheer = "Civilian7_Cheer";
        }
        if(Identity == 8)
        {
            Civilian_walk = "Civilian8_walk";
            Civilian_idle = "Civilian8_idle";
            Civilian_cheer = "Civilian8_Cheer";
        }
        if(Identity == 9)
        {
            Civilian_walk = "Civilian9_walk";
            Civilian_idle = "Civilian9_idle";
            Civilian_cheer = "Civilian9_Cheer";
        }
        if(Identity == 10)
        {
            Civilian_walk = "Civilian10_walk";
            Civilian_idle = "Civilian10_idle";
            Civilian_cheer = "Civilian10_Cheer";
        }
        if(Identity == 11)
        {
            Civilian_walk = "Civilian11_walk";
            Civilian_idle = "Civilian11_idle";
            Civilian_cheer = "Civilian11_Cheer";
        }
        if(Identity == 12)
        {
            Civilian_walk = "Civilian12_walk";
            Civilian_idle = "Civilian12_idle";
            Civilian_cheer = "Civilian12_Cheer";
        }
        if(Identity == 13)
        {
            Civilian_walk = "Civilian13_walk";
            Civilian_idle = "Civilian13_idle";
            Civilian_cheer = "Civilian13_Cheer";
        }
        if(Identity == 14)
        {
            Civilian_walk = "Civilian14_walk";
            Civilian_idle = "Civilian14_idle";
            Civilian_cheer = "Civilian14_Cheer";
        }
        if(Identity == 15)
        {
            Civilian_walk = "Civilian15_walk";
            Civilian_idle = "Civilian15_idle";
            Civilian_cheer = "Civilian15_Cheer";
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play(Civilian_cheer);
    }
}
