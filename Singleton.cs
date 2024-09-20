using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [HideInInspector] public int highscore = 0;
    [HideInInspector] public int currentScene;
    [HideInInspector] public bool isIntro;

    private void Awake()
    {
        if (highscore <= 0) { highscore = 0; }
        DontDestroyOnLoad(gameObject);
    }
}
