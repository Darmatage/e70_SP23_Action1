using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    private int str_lvl = 1;
    private float speed = 7.0f;
    private float base_speed;
    private float attack;
    public float health = 20;

    private Transform target;
    public int framecount = 0;
    private Vector3 attack_location;
    private float angle;

    private bool seek;

    private int seek_and_destroy = 250;
    private int seek_and_cough = 350;
    private int cough_max = 450;

    public SpriteRenderer spriteRenderer;
    public GameObject virus;
    public GameObject zombie;

    public Sprite boss_attack;
    public Sprite boss_normal;

    private GameHandler gameHandler;

    //private bool Faceleft = false;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        attack_location = transform.position;
        angle = Mathf.Atan2((transform.position.y - target.transform.position.y) *-1, (transform.position.x - target.transform.position.x)*-1) * Mathf.Rad2Deg -90f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectortest = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        if(vectortest.x > 0) transform.localScale = new Vector3(2f, -2f, 1f);
        else transform.localScale = new Vector3(2f, 2f, 1f);

        transform.rotation = Quaternion.Euler(0, 0, angle-90);
        float dist = Vector3.Distance(attack_location,transform.position);
        float DistToPlayer = Vector3.Distance(target.transform.position,transform.position);

        //Vector3 fronter = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        //pointer.transform.position = transform.position + fronter;
        //pointer.transform.rotation = Quaternion.Euler(0, 0, angle);

        if(DistToPlayer < 10.0f)
        {
            spriteRenderer.sprite = boss_normal;
            if(framecount < 5) attack_location = target.transform.position;
            if(framecount < seek_and_destroy && dist > 0.5f) {
                angle = Mathf.Atan2((transform.position.y - attack_location.y) *-1, (transform.position.x - attack_location.x)*-1) * Mathf.Rad2Deg -90f;
                Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                transform.position = transform.position + hvMove * speed * Time.deltaTime;
                spriteRenderer.sprite = boss_attack;
            }
            else angle = Mathf.Atan2((transform.position.y - target.transform.position.y) *-1, (transform.position.x - target.transform.position.x)*-1) * Mathf.Rad2Deg -90f;
            
            if(framecount > seek_and_cough && framecount <= cough_max)
            {
                spriteRenderer.sprite = boss_attack;
                if(framecount%13 == 0)
                {
                    GameObject clone = Instantiate(virus) as GameObject;
                    Vector3 front = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                    clone.transform.position = transform.position + front * (transform.localScale.x/2) ;
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
            StartCoroutine(collideFlash());

            float temp_angle = collision.gameObject.GetComponent<Bulletmove>().angle;
            Vector3 hvMove = new Vector3((float)Math.Cos((temp_angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((temp_angle + 90)/ Mathf.Rad2Deg), 0.0f);
            transform.position = transform.position + hvMove * 1.0f * Time.deltaTime;

            if(health <= 0)
            {
                //GameObject clone = Instantiate(zombie) as GameObject;
                zombie.SetActive(true);
                zombie.transform.position = transform.position;
                //Destroy(pointer);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            gameHandler.playerGetHit(str_lvl*5);
            framecount = seek_and_destroy;
            Vector3 hvMove = new Vector3((float)Math.Cos((angle + 270) / Mathf.Rad2Deg), (float)Math.Sin((angle + 270)/ Mathf.Rad2Deg), 0.0f);
            transform.position = transform.position + hvMove * 5.0f * Time.deltaTime;
        }
    }

    IEnumerator collideFlash() 
    {
        spriteRenderer.material.color =  Color.red;
        yield return new WaitForSeconds(0.1f);  
        spriteRenderer.material.color = Color.white;         
    }
}
