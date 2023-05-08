using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTank : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject[] Destroy_object;
    public GameObject TheMaster;
    public int health = 5;

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
        if (collision.gameObject.tag == "Vaccine") 
        {
            health--;
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
        Destroy(gameObject);
    }
}
