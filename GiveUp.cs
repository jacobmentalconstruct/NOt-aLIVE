using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveUp : MonoBehaviour {

    public void QuitGame() { Application.Quit(); Debug.Log("Player has quit the game."); }
}
