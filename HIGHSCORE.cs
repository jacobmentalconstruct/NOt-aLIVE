using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIGHSCORE : MonoBehaviour {
    [HideInInspector] public int highscore;

    private void Awake() { DontDestroyOnLoad(gameObject); }
}
