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

      public static float rescued = 0.0f; // Leo added this
      public int ammo = 10; // Leo added this
      public int StartPlayerAmmo = 10;


      //public static int gotTokens = 0; 
      public GameObject tokensText; 

      public bool isDefending = false; 

      public static bool stairCaseUnlocked = false; 
      //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true; 

      private string sceneName;
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
      void Pause(){
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
                        // player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation 
                  } 
            } 
            
           if (playerHealth > StartPlayerHealth+20){
                  playerHealth = StartPlayerHealth; 
                  updateStatsDisplay();
            }

           if (playerHealth <= 0){
                  playerHealth = 0; 
                  updateStatsDisplay();
                  playerDies();
            }
      } 

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
            StartCoroutine(DeathPause());
      }

      IEnumerator DeathPause(){
            // player.GetComponent<PlayerMove>().isAlive = false;
            // player.GetComponent<PlayerJump>().isAlive = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("End_Lose");
      }

      public void StartGame() {
            SceneManager.LoadScene("Level1");
            updateStatsDisplay();
      }

      public void RestartGame() {
            // Used for PauseMenu
            Time.timeScale = 1f; 

            SceneManager.LoadScene("MainMenu");
                // Please also reset all static variables here, for new games!
            playerHealth = StartPlayerHealth;
            ammo = StartPlayerAmmo;
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

      public void civilian_rescued() // changes the civilian rescue count
      {
            rescued+=0.5f;
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
