using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_Controller : MonoBehaviour
{
    // Singleton script to manage global state. We use this to store current scene, highscore, etc.
    private Singleton _singleton;  
    
    // Canvass and UI controller to handle game UI during scene transitions.
    private GameObject _UI_Canvass;  
    private UI_Controller _UI_Controller;  
    private GameObject _Singleton;  
    
    // Cameras and panels for managing UI and gameplay intro state.
    public GameObject mainCamera_OBJ;  // Main camera for gameplay 
    public GameObject IntroCamera;     // Intro camera for cutscenes or intro view 
    public GameObject introPanel_OBJ;  // UI panel for the intro sequence 
    public GameObject mainPanel_OBJ;   // UI panel for the main game UI 
    public bool isIntro;               // Flag to indicate if we are in the intro state 

    // <Initialize the scene. Fetch the Singleton and UI controller, and configure the UI.>
    private void Start() {
        // Get the Singleton component. This keeps track of global game data.
        _singleton = GameObject.FindGameObjectWithTag("Singleton").GetComponent<Singleton>();  
        if(_singleton == null){ Debug.Log("Error there is no Singleton available."); }

        // Get the UI Canvass to access the UI_Controller
        _UI_Canvass = GameObject.FindGameObjectWithTag("UI_Canvass");  
        if(_UI_Canvass == null){ Debug.Log("Error there is no UI_Canvass available."); }

        _UI_Controller = _UI_Canvass.GetComponent<UI_Controller>();  
        if(_UI_Canvass == null){ Debug.Log("Error there is no UI_Canvass script available."); }

        // <Configure the UI and camera for the current game state (intro or gameplay).>
        UpdateUIForIntro(_singleton.isIntro);
    }

    // <Method to load the next level.>
    public void LoadNextLevel() {
        _increment_currentScene();  // Increment the scene count 
        _loadScene();               // Load the scene based on the updated scene count
    }

    // <Method to load the previous level.>
    public void LoadPrevLevel() {
        _deincrement_currentScene();  // Decrement the scene count 
        if (_singleton.currentScene > 0) { _loadScene(); }  // Load the scene if valid
        else { LoadIntro(); }  // Otherwise, load the intro
    }

    // <Method to reload the current scene.>
    public void ReloadCurrentLevel() {
        _loadScene();  // Reload the scene by current scene count
    }

    // <Method to load the intro state.>
    public void LoadIntro() {
        _reload_intro();  // Reset scene count to intro
        _loadScene();     // Load the scene again in intro state
        _UI_Controller.isIntro = false;  // Ensure UI reflects that we're out of the intro
    }

    // <Method to update the UI and game state based on whether we're in the intro.>
    public void UpdateUIForIntro(bool isIntroState) {
        isIntro = isIntroState;  // Set the isIntro flag

        if (isIntro) {
            // <Setup the intro state: activate intro camera and UI, disable main game elements.>
            IntroCamera.SetActive(true);
            mainCamera_OBJ.SetActive(false);
            introPanel_OBJ.SetActive(true);
            mainPanel_OBJ.SetActive(false);
        } else {
            // <Setup the gameplay state: activate main game camera and UI, disable intro elements.>
            IntroCamera.SetActive(false);
            mainCamera_OBJ.SetActive(true);
            introPanel_OBJ.SetActive(false);
            mainPanel_OBJ.SetActive(true);
        }
    }

    // <Helper methods to manage scene count and loading.>
    private void _loadScene(){ SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }  // Load by build index 
    private void _increment_currentScene(){ _singleton.currentScene++; }  // Increment scene index 
    private void _deincrement_currentScene(){ _singleton.currentScene--; }  // Decrement scene index 
    private void _reload_intro(){ _singleton.currentScene = 0; }  // Reset to intro state 
    private void _Set_isIntro(bool _v){ _singleton.isIntro = _v; }  // Set the isIntro flag 
}
