using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cheer : MonoBehaviour
{
    private int Identity;

    public Animator anim;

    private String Civilian_cheer;

    
    // Start is called before the first frame update
    void Start()
    {
        
        Identity = UnityEngine.Random.Range(1, 11);
        Civilian_cheer = "Civilian1_Cheer";

        if(Identity == 2)
        {
            Civilian_cheer = "Civilian2_Cheer";
        }
        if(Identity == 3)
        {
            Civilian_cheer = "Civilian3_Cheer";
        }
        if(Identity == 4)
        {
            Civilian_cheer = "Civilian4_Cheer";
        }
        if(Identity == 5)
        {
            Civilian_cheer = "Civilian5_Cheer";
        }
        if(Identity == 6)
        {
            Civilian_cheer = "Civilian6_Cheer";
        }
        if(Identity == 7)
        {
            Civilian_cheer = "Civilian7_Cheer";
        }
        if(Identity == 8)
        {
            Civilian_cheer = "Civilian8_Cheer";
        }
        if(Identity == 9)
        {
            Civilian_cheer = "Civilian9_Cheer";
        }
        if(Identity == 10)
        {
            Civilian_cheer = "Civilian10_Cheer";
        }
        if(Identity == 11)
        {
            Civilian_cheer = "Civilian11_Cheer";
        }
        if(Identity == 12)
        {
            Civilian_cheer = "Civilian12_Cheer";
        }
        if(Identity == 13)
        {
            Civilian_cheer = "Civilian13_Cheer";
        }
        if(Identity == 14)
        {
            Civilian_cheer = "Civilian14_Cheer";
        }
        if(Identity == 15)
        {
            Civilian_cheer = "Civilian15_Cheer";
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play(Civilian_cheer);
    }
}
