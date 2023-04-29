using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_HelicopterHandler : MonoBehaviour{
	//add this script to the player to manage the intro and outro copter sequences

	public string NextLevel = "MainMenu";

	private PlayerMoveAround playerMoveScript;
	public GameObject playerArt;
	public GameObject playerGunArt;
	
	public Animator animStartCoptor;
	public Animator animEndCoptor;
	
	//startCopter
	public GameObject startCopter;
	public GameObject startC_ground; 
	public GameObject startC_sky;
	
	//endCopter
	public GameObject endCopter;
	public GameObject endCopterSeat;
	public GameObject endC_sky;
	
	//copter Management
	public float moveSpeed = 1f;
	public bool startCopterDown = false;
	public bool startCopterUp = false;
	public bool endCopterUp = false;
	
	//public AudioSource copterSoundFast;
	//public AudioSource copterSoundSlow;
	
	void Awake(){
		playerMoveScript = GetComponent<PlayerMoveAround>();
	}
	
    void Start(){
        //turn off the player (but keep it there for camera location 
		playerMoveScript.enabled = false;
		playerArt.SetActive(false);
		playerGunArt.SetActive(false);
		animEndCoptor.SetBool("slow", true);
		StartCoroutine(StartCopter());
		
    }

    void FixedUpdate(){
        if (startCopterDown){
			Vector2 posCopter = Vector2.Lerp ((Vector2)startCopter.transform.position, (Vector2)startC_ground.transform.position, moveSpeed * Time.fixedDeltaTime);
            startCopter.transform.position = new Vector3 (posCopter.x, posCopter.y, transform.position.z);
		}
		else if (startCopterUp){
			Vector2 posCopter = Vector2.Lerp ((Vector2)startCopter.transform.position, (Vector2)startC_sky.transform.position, moveSpeed * Time.fixedDeltaTime);
            startCopter.transform.position = new Vector3 (posCopter.x, posCopter.y, transform.position.z);
		}
		else if (endCopterUp){
			Vector2 posCopter = Vector2.Lerp ((Vector2)endCopter.transform.position, (Vector2)endC_sky.transform.position, moveSpeed * Time.fixedDeltaTime);
            endCopter.transform.position = new Vector3 (posCopter.x, posCopter.y, transform.position.z);
		}
    }
	
	void OnTriggerEnter2D(Collider2D other){
		//Activate EndCopter
		if (other.gameObject.tag=="EndCopter"){
			StartCoroutine(EndCopter());
		}
	}
	
	IEnumerator StartCopter(){
		startCopterDown = true;
		//copterSoundFast.Play();
		yield return new WaitForSeconds(moveSpeed *2f);
		startCopterDown = false;
		//copterSoundFast.Stop();
		
		//copterSoundSlow.Play();
		animStartCoptor.SetBool("slow", true);
		yield return new WaitForSeconds(2f);
		playerMoveScript.enabled = true;
		playerArt.SetActive(true);
		playerGunArt.SetActive(true);
		animStartCoptor.SetBool("slow", false);
		//copterSoundSlow.Stop();
		
		startCopterUp = true;
		//copterSoundFast.Play();
		yield return new WaitForSeconds(moveSpeed *2f);
		startCopterUp = false;
		//copterSound.Stop();
		Destroy(startCopter);
	}
	

	IEnumerator EndCopter(){
		//put a copy of slow audio on endCopter, with small radius
		//endCopter.GetComponent<AudioSource>().Stop();
		//copterSoundFast.Play();
		//playerMoveScript.enabled = false;
		transform.position = endCopterSeat.transform.position;
		transform.parent = endCopterSeat.transform;
		gameObject.GetComponent<Collider2D>().enabled = false;
		animEndCoptor.SetBool("slow", false);
		endCopterUp = true;
		yield return new WaitForSeconds(moveSpeed *2f);
		SceneManager.LoadScene(NextLevel);
	}
	
}
