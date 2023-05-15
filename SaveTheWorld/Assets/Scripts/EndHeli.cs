using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndHeli : MonoBehaviour
{
    public string NextLevel = "EndWin";
    public GameObject playerArt;
	public GameObject playerGunArt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            StartCoroutine(EndCopter());
        }
    }

    IEnumerator EndCopter(){
		playerArt.SetActive(false);
		playerGunArt.SetActive(false);

		yield return new WaitForSeconds(10f);
		SceneManager.LoadScene(NextLevel);
	}

}
