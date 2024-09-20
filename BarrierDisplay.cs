using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierDisplay : MonoBehaviour {
    public Text current;
    public Text max;

    private void Awake()
    {
        current.text = max.text;
    }

    public void SetCurrent(int hp){
        current.text = hp.ToString();
    }  
    public void SetMax(int hp)
    {
        max.text = hp.ToString();
    }
    
}
