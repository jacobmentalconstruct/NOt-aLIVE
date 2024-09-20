using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour {
    // Game objects related to cameras and UI for intro, main game, and endgame phases.
    [Header("Intro/Main/Endgame phase cameras & game objects.")]
    [SerializeField] private GameObject deathCam_OBJ;      // Death camera shown during game over
    [SerializeField] private GameObject endGameUI_OBJ;     // End game UI elements 
    [SerializeField] private GameObject mainCamera_OBJ;    // Main gameplay camera 
    [SerializeField] private GameObject IntroCamera;       // Camera for the intro cutscene 
    public GameObject SINGLETON;                           // Reference to the Singleton object
    public bool isIntro;                                   // Flag to check if we are in the intro state
    [Space(10)]

    // <UI elements related to waves and barrier health.>
    [Header("Collect the wave display")]
    public GameObject waveDisplay_OBJ;     // Wave display UI
    [HideInInspector] public Text waveDisplay_TEXT;  // Text for displaying wave count
    [Space(10)]

    // <References for displaying barrier health.>
    [Header("Collect the updatable text of the barrier hp display.")]
    [SerializeField] private BarrierDisplay barrierDisplay_SCRIPT;  // Script to manage barrier health display

    // <Highscore and score UI elements.>
    [Header("Collect a reference to the Highscore_Current_TXT")]
    public Text highscore_TXT;             // Highscore text display
    [HideInInspector] public int highscore;  // Internal highscore value
    [Space(10)]

    [Header("Collect a reference to the Score_Controller and Score_Current_TXT being used by this gameObject.")]
    private Score_Controller scoreController_SCRIPT;  // Score controller for managing scores
    public Text score_TXT;  // Score text display
    [HideInInspector] public int score;  // Current score
    [HideInInspector] public Singleton singleton_SCRIPT;  // Singleton script reference
    [Space(10)]

    [Header("Reference Intro/Main Scoreboard panels")]
    [SerializeField] private GameObject introPanel_OBJ;  // UI for the intro sequence
    [SerializeField] private GameObject mainPanel_OBJ;   // UI for main gameplay

    // <Initialization method.>
    private void Awake()
    {
        // Get the Singleton and its Script
        _load_Singleton_CS();  // Load the singleton script
        _load_Singleton_OBJ();  // Load the singleton object

        // <Check if we are in the intro state.>
        if (singleton_SCRIPT.isIntro == true) { isIntro = true; } else { isIntro = false; }

        // <Load the score controller and update the UI with the current score.>
        scoreController_SCRIPT = GetComponent<Score_Controller>();
        highscore = SINGLETON.GetComponent<Singleton>().highscore;  // Set initial highscore
        highscore_TXT.text = highscore.ToString();  // Display highscore

        // <Initialize wave display.>
        waveDisplay_TEXT = waveDisplay_OBJ.transform.GetComponent<Text>();
        waveDisplay_OBJ.SetActive(false);

        // <Ensure end game UI elements and death camera are off initially.>
        if (endGameUI_OBJ.activeInHierarchy == true) { endGameUI_OBJ.SetActive(false); }
        if (deathCam_OBJ.activeInHierarchy == true) { deathCam_OBJ.SetActive(false); }

        // <Update UI and cameras based on whether we are in the intro state.>
        if (isIntro == true) {
            IntroCamera.SetActive(true);
            mainCamera_OBJ.SetActive(false);
            introPanel_OBJ.SetActive(true);
            mainPanel_OBJ.SetActive(false);
        }
        else {
            IntroCamera.SetActive(false);
            mainCamera_OBJ.SetActive(true);
            introPanel_OBJ.SetActive(false);
            mainPanel_OBJ.SetActive(true);
}
