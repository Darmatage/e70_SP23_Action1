using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMoveAround : MonoBehaviour {

      //public Animator anim;
      //public AudioSource WalkSFX;
      public Rigidbody2D rb2D;
      private bool FaceRight = true; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;

      public double orientation; // the angle that the player is facing
      public float ismoving; // is moving or not
      public int ammo; // the current number of bullets
      public int health; // the current amoutn of health

      public int weapon_grade; // the level of the weapon

      void Start(){
           //anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            if(health < 0) isAlive = false;

            // Leo's notes add figure out and modify the player move so we can get angular movements
            orientation = orientation + Input.GetAxis("Horizontal");
            Vector3 hvMove = new Vector3((float)Math.Sin(Math.PI*orientation/18)*Input.GetAxis("Vertical"), (float)Math.Cos(Math.PI*orientation/18)*Input.GetAxis("Vertical"), 0.0f);

            //Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            if (isAlive == true){
                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;
                  //Quaternion target = new Quaternion(0, 0, 10*orientation);
                  transform.rotation = Quaternion.Euler(0, 0, (float)(-10*orientation));

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

                  // Turning. Reverse if input is moving the Player right and Player faces left.
                  if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                        playerTurn();
                  }
            }
      }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }
}