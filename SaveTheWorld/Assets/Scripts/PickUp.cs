using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;


public class PickUp : MonoBehaviour{

      private GameHandler gameHandler;
      //public playerVFX playerPowerupVFX; 
      public bool isHealthPickUp = true;
      public int healthBoost = 25;

      public bool isAmmoPickUp = false;
      public int ammoBoost = 10;

      public AudioSource SFX;



      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>(); 
      }

      public void OnTriggerEnter2D (Collider2D other){ 
            if (other.gameObject.tag == "Player"){ 

                    // GetComponent<Collider2D>().enabled = false; 
                  StartCoroutine(DestroyThis());

                  //GetComponent<AudioSource>().Play();

                  if (isHealthPickUp == true) {
                    // This can be changed if needed.
                        gameHandler.playerGetHit(healthBoost *-1);

                        //playerPowerupVFX.powerup();
                  }

                  if (isAmmoPickUp == true) {
                    // This can be changed if needed.
                        gameHandler.playerGetAmmo(ammoBoost);

                        //playerPowerupVFX.powerup();
                  }

            }
            gameHandler.updatedisplay();
      } 

      IEnumerator DestroyThis(){
            SFX.Play();
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}