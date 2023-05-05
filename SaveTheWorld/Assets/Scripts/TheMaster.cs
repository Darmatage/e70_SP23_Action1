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
    private int counter = 0;
    private float angle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //counter++;
        //if(counter == 5)
        //{
            angle++;
            float child_angle = 0.0f;
            foreach(GameObject child in children)
            {
                if(child != null)
                {
                    GameObject clone = Instantiate(virus) as GameObject;
                    clone.transform.position = transform.position;
                    clone.transform.rotation = Quaternion.Euler(0, 0, angle + child_angle);
                    clone.SetActive(true);
                    child_angle += 90;
                }
            }
            //counter = 0;
        //}
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vaccine") 
        {
            health--;
            StartCoroutine(collideFlash());
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
        Destroy(gameObject);
    }
}
