using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieMove : MonoBehaviour
{
    // Start is called before the first frame update
    private bool hunt;
    public bool zombiemode = true;
    public int str_lvl;

    private float speed;
    private float base_speed;
    private float attack;
    public float health;

    public GameHandler gameHandler;
    private Transform target;
    private int framecount = 0;
    private Vector3 attack_location;
    private float angle;
    private float reinfect = 0.0f;
    //private float rate = 0.99f;
    private float daze = 0;

    private int Identity;

    public Sprite Human;
    public Sprite Zombie;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2D;

    public Animator anim;
    private Boolean transformers = false;

    private String Civilian_walk;
    private String Civilian_idle;
    private String Civilian_cheer;

    public AudioSource MedivacSFX;

    private GameObject[] checkpoints;
    private GameObject[] pathnodes;
    private Boolean foundsafety = false;
    private Boolean beingsaved = false;
    private Boolean foundpath = false;

    public bool isLevel4 = false;
    public bool startascivilian = false;

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



		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        if (isLevel4 == true){
            pathnodes = GameObject.FindGameObjectsWithTag("Node");
        }
		
        zombify();
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        attack_location = transform.position;
        angle = Mathf.Atan2((transform.position.y - target.transform.position.y) *-1, (transform.position.x - target.transform.position.x)*-1) * Mathf.Rad2Deg -90f;
        if(!zombiemode) vaxed();
        if(startascivilian) speed = 0.01f;
    }

    void zombify()
    {
        health = str_lvl*str_lvl * 2;
        base_speed = 1 / ((float)(str_lvl + 1));
    }

    // Update is called once per frame
    void Update()
    {
        //health = target.eulerAngles;
        //Random rnd = new Random();
        float dist1 = Vector3.Distance(transform.position, target.transform.position);
        if(reinfect > 0) reinfect--;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if(zombiemode)
        {
            gameObject.layer = 6;
            anim.enabled = true;
            spriteRenderer.sprite = Zombie;
            framecount = (framecount+1)%25;

            double DistToPlayer = Vector3.Distance(transform.position, attack_location);
            
            //if(dist1 > 15) attack_location = seek_victim();

            if(dist1 < 2.5f)
            {
                attack_location = target.position;
                hunt = true;
            }

            if(dist1 < 10 && Input.GetMouseButtonDown(0))
            {
                attack_location = target.position;
                hunt = true;
            }

            if(DistToPlayer > 1.5f)
            {
                hunt = true;
            }

            if(hunt)
            {
                if(!transformers) anim.Play("Zombie_attack");
                angle = Mathf.Atan2((transform.position.y - attack_location.y) *-1, (transform.position.x - attack_location.x)*-1) * Mathf.Rad2Deg -90f;
                speed = 8.0f * base_speed;
                if(DistToPlayer < 1)
                {
                    hunt = false;
                }
            }
            else
            {
                if(!transformers) anim.Play("Zombie_walk");
                speed = base_speed;
                if(framecount == 0) angle += (Math.Abs(angle)%11 - 5)*5;
            }

            if(daze == 0)
            {
                Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                transform.position = transform.position + hvMove * speed * Time.deltaTime;
                //transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                daze --;
            }
        }
        else
        {
            gameObject.layer = 8;
            spriteRenderer.sprite = Human;

            runtosafety();
            //transform.rotation = Quaternion.Euler(0, 0, angle);
            if(!foundsafety)
            {
                attack_location = target.position;
                angle = Mathf.Atan2((transform.position.y - target.transform.position.y) *-1, (transform.position.x - target.transform.position.x)*-1) * Mathf.Rad2Deg -90f;
                if (dist1 > 1.5f)
                {
                    if(!transformers) anim.Play(Civilian_walk);
                    Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                    transform.position = transform.position + hvMove * speed * Time.deltaTime;
                }
                else
                {
                    if(!transformers) anim.Play(Civilian_idle);
                }
            }
            else
            {
                if(!transformers) anim.Play(Civilian_walk);
                angle = Mathf.Atan2((transform.position.y - attack_location.y) *-1, (transform.position.x - attack_location.x)*-1) * Mathf.Rad2Deg -90f;
                Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                transform.position = transform.position + hvMove * speed * Time.deltaTime;
            }

            if(reinfect <= 10) StartCoroutine(sick());
        }

        //if(hunt) targeting();
        //tracking();
        
    }

    void runtosafety()
    {
        foreach(GameObject safety in checkpoints)
        {
            double DistToSafety = Vector3.Distance(transform.position, safety.transform.position);
            if(DistToSafety < 3f)
            {
                foundsafety = true;
                if(!transformers) anim.Play(Civilian_walk);
                angle = Mathf.Atan2((transform.position.y - safety.transform.position.y) *-1, (transform.position.x - safety.transform.position.x)*-1) * Mathf.Rad2Deg -90f;
                Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
                transform.position = transform.position + hvMove * speed * Time.deltaTime;
            }
            if(DistToSafety < 1f) StartCoroutine(cheering());
        }

        //In the level 4 lab, cured civilians run for the exit at the start of the lab
        if (isLevel4 == true){
            foreach(GameObject pnode in pathnodes)
            {
                double DistToSafety = Vector3.Distance(transform.position, pnode.transform.position);
                if(DistToSafety < 10f && !foundsafety)
                {
                    foundsafety = true;
                    foundpath = true;
                    attack_location = pnode.transform.position;
                 }
                if(DistToSafety < 0.75f) 
                {
                    //foundpath = true;
                    foundsafety = true;
                    GameObject nnode = pnode.GetComponent<PathNode>().nextnode;
                    attack_location = nnode.transform.position;
                }
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //float bounce = 6f; //amount of force to apply
        //rb2D.AddForce(collision.contacts[0].normal * bounce);
        //isBouncing = true;

        //angle += (Math.Abs(angle)%3 - 2)*45;
        //Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
        //transform.position = transform.position + hvMove * 2 * Time.deltaTime;

        if (collision.gameObject.tag == "Zombie" && !zombiemode && reinfect < 10) 
        {
            StartCoroutine(transformed());
            foundsafety = false;
            foundpath = false;
            zombiemode = true;
            gameObject.tag = "Zombie";
            zombify();
            reinfect = 0;
        }
        if (collision.gameObject.tag == "Vaccine") 
        {
            health--;
            StartCoroutine(collideFlash());
            if(health <= 0 && zombiemode)
            {
                StartCoroutine(transformed());
                zombiemode = false;
                gameObject.tag = "Civilian";
                speed = 3;
                reinfect = 750;
            }
            //Color32 c = spriteRenderer.material.color;
            //spriteRenderer.material.SetColor("_Color", Color.red);
            //spriteRenderer.material.color = c;
            //collideFlash();
        }
        if (collision.gameObject.tag == "CheckPoint" && !zombiemode) 
        {
            speed = 0.1f;
            StartCoroutine(cheering());
        }
        if (collision.gameObject.tag == "Player" && zombiemode)
        {
            gameHandler.playerGetHit(str_lvl*10);
            daze = 100;
            Vector3 hvMove = new Vector3((float)Math.Cos((angle + 270) / Mathf.Rad2Deg), (float)Math.Sin((angle + 270)/ Mathf.Rad2Deg), 0.0f);
            transform.position = transform.position + hvMove * 5.0f * Time.deltaTime;
        }
        if (collision.gameObject.tag == "Civilian" && !zombiemode)
        {
            angle +=30;
            Vector3 hvMove = new Vector3((float)Math.Cos((angle + 90) / Mathf.Rad2Deg), (float)Math.Sin((angle + 90)/ Mathf.Rad2Deg), 0.0f);
            transform.position = transform.position + hvMove * 1.0f * Time.deltaTime;
        }
    }

    public void vaxed()
    {
        StartCoroutine(transformed());
        zombiemode = false;
        //gameHandler.civilian_rescued();
        gameObject.tag = "Civilian";
        speed = 3;
        reinfect = 0;
    }

    public void killed()
    {
        StartCoroutine(transformed());
        foundsafety = false;
        zombiemode = true;
        gameObject.tag = "Zombie";
        zombify();
        reinfect = 0;
    }

    IEnumerator collideFlash() 
    {
        spriteRenderer.material.color =  Color.red;
        yield return new WaitForSeconds(0.1f);  
        spriteRenderer.material.color = Color.white;         
    }
    
    IEnumerator transformed() 
    {
        transformers = true;
        float rescale = (float)(str_lvl-1)*0.5f + 1f;
        if(zombiemode) transform.localScale = new Vector3(1f, 1f, 1f);
        else transform.localScale = new Vector3(rescale, rescale, 1f);
        anim.Play("Zombie_rescued");
        yield return new WaitForSeconds(1.5f);
        transformers = false; 

    }

    IEnumerator cheering() 
    {
        transformers = true;
        zombiemode = false;
        gameObject.tag = "Civilian";
        anim.Play(Civilian_cheer);
        MedivacSFX.Play();
        //speed = 0;
        yield return new WaitForSeconds(1f);
        if(!beingsaved) gameHandler.civilian_rescued();
        beingsaved = true;
        Destroy(gameObject);   
    }

    IEnumerator sick() 
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.color =  Color.green;
        yield return new WaitForSeconds(0.1f);  
        spriteRenderer.material.color = Color.white;         
    }

    private Vector3 seek_victim()
    {

        Transform respawn = GameObject.FindGameObjectWithTag ("Civilian").GetComponent<Transform> ();
        double dist_temp = Vector3.Distance(transform.position, respawn.transform.position);

        if(dist_temp <= 10) return respawn.transform.position;
        return attack_location;
        
    }
}
