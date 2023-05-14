using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestTank : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject[] Destroy_object;
    public GameObject TheMaster;
    public Sprite[] tank_sprite;
    public int health = 5;
    public Boolean alive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TheMaster = GameObject.Find("TheMaster");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vaccine" && health > 0) 
        {
            health--;
            spriteRenderer.sprite = tank_sprite[health];
            StartCoroutine(collideFlash());
            TheMaster.GetComponent<TheMaster>().is_hit();
            if(health <= 0) StartCoroutine(destruct());
        }
    }

    IEnumerator collideFlash() 
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);  
        spriteRenderer.material.color = Color.white;      
    }

    IEnumerator destruct()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(GameObject Todestroy in Destroy_object)
        {
            Destroy(Todestroy);
        }
        alive = false;
        //Destroy(gameObject);
    }
}
