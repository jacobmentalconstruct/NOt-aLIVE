using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore_Controller : MonoBehaviour {
    private UI_Controller ui_Controller;
    private Text highscore;
    private Text score;
    private Singleton singleton;

    private void Awake()
    {
        ui_Controller = GetComponentInParent<UI_Controller>();
        highscore = ui_Controller.highscore_TXT;
        score = ui_Controller.score_TXT;
    }

    private void Update()
    {
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        int currentScore = ui_Controller.score;
        int currentHighscore = ui_Controller.highscore;

        if (ui_Controller.score > ui_Controller.highscore)
        {
            highscore.text = currentScore.ToString();
            ui_Controller.singleton_SCRIPT.highscore = currentScore;
        }
    }
}
