using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TheMaster : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject[] children;
    public GameObject[] Destroy_object;
    public int health = 10;
    public GameObject virus;
    public int counter = 0;
    //private float angle = 0.0f;
    private Transform target;
    private GameObject clone;
    public Sprite[] tank_sprite;
    private int iter = 13;

    private Boolean start = true;
    private Boolean isalive = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(child_count() == 0) 
        {
            StartCoroutine(destruct());
            isalive = false;
            StopCoroutine(spawn());
            clone.transform.GetChild(1).gameObject.GetComponent<ZombieMove>().speed = 3.0f;
            //Destroy(clone.transform.GetChild(0));
        }
        if(start && isalive && Vector3.Distance(transform.position, target.transform.position) <= 15) 
        {
            start = false;
            StartCoroutine(spawn());
        }
    }
        /*
        spriteRenderer.sprite = tank_sprite[iter];
        if(iter <= 0) iter = 0;
        if(child_count() == 0) StartCoroutine(destruct());
        if(Vector3.Distance(transform.position, target.transform.position) <= 15)
        {
            counter++;
            /*
            angle+=0.1f;
            float child_angle = 0.0f;
            float angle_change = 360f / (float)child_count();
            foreach(GameObject child in children)
            {
                if(child != null)
                {
                    GameObject clone = Instantiate(virus) as GameObject;
                    clone.transform.position = transform.position;
                    clone.transform.rotation = Quaternion.Euler(0, 0, angle + child_angle);
                    clone.SetActive(true);
                    child_angle += angle_change;
                }
            }*/
/*
            if(counter == c_min)
            {
                clone = Instantiate(virus) as GameObject;
                clone.transform.position = transform.position;
                //clone.transform.position.renderer.enabled = true;
                clone.SetActive(true);
            }
            if(counter == c_min) 
            {
                counter = 0;
                iter = 13;
                clone.transform.GetChild(1).gameObject.GetComponent<ZombieMove>().killed();
                clone.transform.GetChild(1).gameObject.SetActive(false);
                clone.transform.GetChild(0).gameObject.SetActive(true);
            }

        }**/
    //}

    IEnumerator spawn() 
    {
        spriteRenderer.sprite = tank_sprite[iter];
        yield return new WaitForSeconds(2f);

        clone = Instantiate(virus) as GameObject;
        clone.transform.position = transform.position;
        clone.SetActive(true);

        for(iter = 13; iter >= 0; iter--)
        {
            if(isalive){
                yield return new WaitForSeconds(0.60f);
                spriteRenderer.sprite = tank_sprite[iter];
            }
        }

        if(isalive){
            clone.transform.GetChild(1).gameObject.GetComponent<ZombieMove>().killed();
            clone.transform.GetChild(1).gameObject.SetActive(false);
            clone.transform.GetChild(0).gameObject.SetActive(true);
        }

        iter = 13;
        spriteRenderer.sprite = tank_sprite[iter];

        start = true;
    }

    int child_count()
    {
        int count = 0;
        foreach(GameObject child in children)
        {
            if(child.GetComponent<TestTank>().alive) count ++;
        }
        return count;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vaccine" && child_count() == 0) 
        {
            health--;
            is_hit();
            if(health <= 0) 
            {
                isalive = false;
                StartCoroutine(destruct());
            }
        }
    }

    public void is_hit()
    {
        StartCoroutine(collideFlash());
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
    }
}
