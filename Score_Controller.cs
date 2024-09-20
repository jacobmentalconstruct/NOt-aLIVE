using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Controller : MonoBehaviour {
    [Header("Collect the updatable scoring text boxes.")]
    private int score;
    private UI_Controller uiController;
    

    private void Awake()
    {
        uiController = GetComponentInParent<UI_Controller>();
        score = uiController.score;
        UpdateScore(score);
    }

    public void UpdateScore(int v)
    {
        score = score + v;
        uiController.score_TXT.text = score.ToString();
        uiController.score = score;
    }  
}
