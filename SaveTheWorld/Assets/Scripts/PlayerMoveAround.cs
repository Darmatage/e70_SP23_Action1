using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMoveAround : MonoBehaviour {

      //public Animator anim;
      //public AudioSource WalkSFX;
      public Vector2 mousePosition;
      public Rigidbody2D rb2D;
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;

      public float angle; // the angle that the player is facing
      //public float ismoving; // is moving or not
      //public int ammo; // the current number of bullets
      public int health; // the current amoutn of health

      //public int weapon_grade; // the level of the weapon
      public Rigidbody2D rb;
      public GameObject Bullet;
      private int reload;

      void Start(){
           //anim = gameObject.GetComponentInChildren<Animator>();
           
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
            if(reload != 0) reload--;
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            if(health < 0) isAlive = false;

            // Leo's notes add figure out and modify the player move so we can get angular movements
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            //Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            if (isAlive == true)
            {
                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;
                  //Quaternion target = new Quaternion(0, 0, 10*orientation);
                  //transform.rotation = Quaternion.Euler(0, 0, (float)(-10*orientation));
                  if (Input.GetAxis("Horizontal") != 0)
                  {
                        //transform.rotation = Quaternion.Euler(0, 0, (float)(-180*orientation/num_orient));
                  }
                  if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                  //     anim.SetBool ("Walk", true);
                  //     if (!WalkSFX.isPlaying){
                  //           WalkSFX.Play();
                  //     }
                  } else {
                  //     anim.SetBool ("Walk", false);
                  //     WalkSFX.Stop();
                  }

                  //Vector3 newPosition = new Vector3(0, 0, 10);
                  //transform.position = newPosition;
            }
            
            if (Input.GetMouseButtonDown(0)&& reload <= 0)
            {
                  reload = 2;
                  GameObject clone = Instantiate(Bullet) as GameObject;
                  clone.SetActive(true);
            }

            mousePosition = Input.mousePosition;
            Vector2 mouse = new Vector2(mousePosition.x - Screen.width/2, mousePosition.y- Screen.height/2);
            float angle = Mathf.Atan2(mouse.y, mouse.x) * Mathf.Rad2Deg -90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
      }
}