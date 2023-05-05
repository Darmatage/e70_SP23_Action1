using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHander : MonoBehaviour
{
    public GameObject[] Set_active;
    public GameObject[] Destroy_object;
    public GameObject[] zombify;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(waittime());
    }

    IEnumerator waittime()
    {
        yield return new WaitForSeconds(0.5f); 
        foreach(GameObject civilian in zombify)
        {
            civilian.GetComponent<ZombieMove>().killed();
        }
        yield return new WaitForSeconds(0.5f); 
        foreach(GameObject Todestroy in Destroy_object)
        {
            Destroy(Todestroy);
        }
        foreach(GameObject Toactivate in Set_active)
        {
            Toactivate.SetActive(true);
        }
        Destroy(gameObject);
    }
}
