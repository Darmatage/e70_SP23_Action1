using System.Collections.Generic; 
using System.Collections; 
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using UnityEngine.Audio;


public class GameHandler : MonoBehaviour { 

      // These are for the Pause Menu
      public static bool GameisPaused = false;
      public GameObject pauseMenuUI;
      public AudioMixer mixer;
      public static float volumeLevel = 1.0f;
      private Slider sliderVolumeCtrl;
      // End PauseMenu


      private GameObject player;
      public static int playerHealth = 100;
      public int StartPlayerHealth = 100;
      public GameObject healthText;
      public GameObject rescueText;
      public GameObject ammoText;
      public GameObject ObjectiveTextpause;

      public static int rescued = 0; // Leo added this
      public static float rescuedt = 0.0f; // Leo added this
      public int ammo = 10; // Leo added this
      public int StartPlayerAmmo = 10;


      //public static int gotTokens = 0; 
      public GameObject tokensText; 

      public bool isDefending = false; 

      public static bool stairCaseUnlocked = false; 
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true; 

      private string sceneName;
	  public static string lastLevelDied;  //allows replaying the Level where you died
	  
      private string objectives;

      // Used for PauseMenu
      void Awake (){ 
            SetLevel (volumeLevel); 
            GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider"); 
            if (sliderTemp != null){ 
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>(); 
                        sliderVolumeCtrl.value = volumeLevel; 
            }
      }


      void Start(){
            // Used for PauseMenu
            pauseMenuUI.SetActive(false); 
            GameisPaused = false;

            player = GameObject.FindWithTag("Player");
            sceneName = SceneManager.GetActiveScene().name;
            //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
                  playerHealth = StartPlayerHealth;
            //}
            updateStatsDisplay();
      }

      // Used for PauseMenu
      void Update (){
            if (Input.GetKeyDown(KeyCode.Escape)){
                  if (GameisPaused){
                              Resume();
                  }
                  else{
                              Pause();
                  }
            }
      }

      // Used for PauseMenu
      public void Pause(){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;
      }

      // Used for PauseMenu
      public void Resume(){
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameisPaused = false;
      }

      // Used for PauseMenu
      public void SetLevel (float sliderValue){
            mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20); 
            volumeLevel = sliderValue; 
        } 
/*
      public void playerGetTokens(int newTokens){
            gotTokens += newTokens;
            updateStatsDisplay();
      }
*/

      public void playerGetHit(int damage){
            if (isDefending == false){
                  playerHealth -= damage; 
                  if (playerHealth >=0){ 
                        updateStatsDisplay();
                  } 
                  if (damage > 0){
                        player.GetComponent<PlayerMoveAround>().injured();
                        // player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation 
                  }
            }
            if (damage > 0){
                  player.GetComponent<PlayerMoveAround>().injured();
            }
            //collideFlash();
            if (playerHealth > StartPlayerHealth+20){
                  while(playerHealth > StartPlayerHealth+20){
                        playerHealth -= 1;
                  }
                  // playerHealth = StartPlayerHealth; 
                  updateStatsDisplay();
            }

            if (playerHealth <= 0){
                  playerHealth = 0; 
                  updateStatsDisplay();
                  playerDies();
            }
      }
/*
      IEnumerator collideFlash() 
      {
            //SpriteRenderer sr = player.GetComponent<SpriteRenderer>(); 
            player.GetComponent<PlayerMoveAround>().material.color =  Color.red;
            yield return new WaitForSeconds(0.1f);  
            player.GetComponent<SpriteRenderer>().material.color = Color.white;         
      }*/

      public void playerGetAmmo(int rounds){
            // if (isDefending == false){
            //       ammo += rounds;
            // }
            ammo += rounds;
      }

      public void updateStatsDisplay(){
            Text healthTextTemp = healthText.GetComponent<Text>();
            healthTextTemp.text = "HEALTH: " + playerHealth; 

            Text rescueTextTemp = rescueText.GetComponent<Text>();// i added this in
            rescueTextTemp.text = "Rescued: " + rescued; 

            Text ammoTextTemp = ammoText.GetComponent<Text>();// i added this in
            ammoTextTemp.text = "Ammo: " + ammo; 

            Text tokensTextTemp = tokensText.GetComponent<Text>();
            tokensTextTemp.text = objectives;

            Text ObjectiveTextpauseTemp = ObjectiveTextpause.GetComponent<Text>();
            ObjectiveTextpauseTemp.text = objectives;
      } 

      public void playerDies(){
            // player.GetComponent<PlayerHurt>().playerDead();       //play Death animation 
	      lastLevelDied = sceneName;       //allows replaying the Level where you died
            SceneManager.LoadScene("End_Lose");
            //StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            // player.GetComponent<PlayerMove>().isAlive = false;
            // player.GetComponent<PlayerJump>().isAlive = false;
            SceneManager.LoadScene("End_Lose");
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("End_Lose");
      }

      public void StartGame() {
            SceneManager.LoadScene("Lvl1_cutscene");
            updateStatsDisplay();
      }

      public void RestartGame() {
            // Used for PauseMenu
            Time.timeScale = 1f;
            
			// Reset all static variables here, for new games!
            playerHealth = StartPlayerHealth;
			rescued = 0;
			rescuedt = 0.0f;
			ammo = StartPlayerAmmo;
			
			SceneManager.LoadScene("MainMenu");
      }

	// Replay the Level where you died
      public void ReplayLastLevel() {
            Time.timeScale = 1f;
            
             // Reset all static variables here, for new games:
            playerHealth = StartPlayerHealth;
			rescued = 0;
			ammo = StartPlayerAmmo *2; 
			
			SceneManager.LoadScene(lastLevelDied);
      }

      public void loadNext(string scene) {
            if(sceneName == "Lvl1_cutscene") SceneManager.LoadScene("Level1");
            // if(sceneName == "Lvl1_Objectives") SceneManager.LoadScene("Level1");

            if(sceneName == "Lvl2_cutscene") SceneManager.LoadScene("Level2");

            if(sceneName == "Lvl3_cutscene") SceneManager.LoadScene("Level3");

            if(sceneName == "Lvlf_cutscene") SceneManager.LoadScene("FinalLevel");

            updateStatsDisplay();
      }

      public void QuitGame() {
                #if UNITY_EDITOR 
                UnityEditor.EditorApplication.isPlaying = false; 
                #else 
                Application.Quit(); 
                #endif
      }

      public void Credits() {
            SceneManager.LoadScene("Credits");
      }
	  
	  public void Controls() {
            SceneManager.LoadScene("Controls");
      }

      public void civilian_rescued() // changes the civilian rescue count
      {
            //rescuedt +=0.25f;
            rescued++;
            updateStatsDisplay();
      }

      public void shots_fired() // changes the bullet count
      {
            ammo--;
            updateStatsDisplay();
      }

      public bool can_shoot() // changes the bullet count
      {
            return ammo > 0 && !(GameisPaused);
      }

      public void updatedisplay()
      {
            updateStatsDisplay();
      }

      public void change_objectives(string s)
      {
            objectives = s;
            updateStatsDisplay();
      }
}
