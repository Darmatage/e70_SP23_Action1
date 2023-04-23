using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    private int str_lvl = 1;
    private float speed = 5.0f;
    private float base_speed;
    private float attack;
    public float health = 20;

    public GameHandler gameHandler;
    private Transform target;
    public int framecount = 0;
    private Vector3 attack_location;
    private float angle;

    private bool seek;

    private int seek_and_destroy = 250;
    private int seek_and_cough = 350;
    private int cough_max = 450;

    public GameObject virus;
    public GameObject zombie;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        attack_location = transform.position;
        angle = Mathf.Atan2((transform.position.y - target.transform.position.y) *-1, (transform.position.x - target.transform.position.x)*-1) * Mathf.Rad2Deg -90f;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
        float dist = Vector3.Distance(attack_location,transform.position);
        float DistToPlayer = Vector3.Distance(target.transform.position,transform.position);

        if(DistToPlayer < 10.0f)
        {
            if(framecount < 5) attack_location = target.transform.position;
            if(framecount < seek_and_destroy && dist > 0.5f) {
                angle = Mathf.Atan2((transform.position.y - attack_location.y) *-1, (transform.position.x - attack_location.x)*-1) * Mathf.Rad2Deg -90f;
                Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                transform.position = transform.position + hvMove * speed * Time.deltaTime;
            }
            else angle = Mathf.Atan2((transform.position.y - target.transform.position.y) *-1, (transform.position.x - target.transform.position.x)*-1) * Mathf.Rad2Deg -90f;
            if(framecount > seek_and_cough && framecount <= cough_max)
            {
                if(framecount%11 == 0)
                {
                    GameObject clone = Instantiate(virus) as GameObject;
                    Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                    clone.transform.position = transform.position + front;
                    clone.transform.rotation = Quaternion.Euler(0, 0, angle);
                    clone.SetActive(true);
                }
            }

            framecount %= 500;
            framecount++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vaccine")
        {
            health --;
            if(health <= 0)
            {
                //GameObject clone = Instantiate(zombie) as GameObject;
                zombie.SetActive(true);
                zombie.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            gameHandler.playerGetHit(str_lvl);
            framecount = seek_and_destroy;
            Vector3 hvMove = new Vector3((float)Math.Cos((angle + 270) / Mathf.Rad2Deg), (float)Math.Sin((angle + 270)/ Mathf.Rad2Deg), 0.0f);
            transform.position = transform.position + hvMove * 5.0f * Time.deltaTime;
        }
    }
}
