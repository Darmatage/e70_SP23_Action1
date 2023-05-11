using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMaster : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject[] children;
    public GameObject[] Destroy_object;
    public int health = 10;
    public GameObject virus;
    public int counter = 0;
    private float angle = 0.0f;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update()
    {
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
            if(counter == 650) 
            {
                counter = 0;
                GameObject clone = Instantiate(virus) as GameObject;
                clone.transform.position = transform.position;
                //clone.transform.position.renderer.enabled = true;
                clone.SetActive(true);
            }

        }
    }

    int child_count()
    {
        int count = 0;
        foreach(GameObject child in children)
        {
            if(child != null) count ++;
        }
        return count;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vaccine" && child_count() == 0) 
        {
            health--;
            is_hit();
            if(health <= 0) StartCoroutine(destruct());
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
        Destroy(gameObject);
    }
}
