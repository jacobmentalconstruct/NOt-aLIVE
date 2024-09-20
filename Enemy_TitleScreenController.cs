using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TitleScreenController : MonoBehaviour {
    [SerializeField] private GameObject[] limbsToColorize;
    public Material[] mats;


    private void Awake()
    {
        for (int i = 0; i <= limbsToColorize.Length - 1; i++)
        {
            limbsToColorize[i].GetComponent<MeshRenderer>().material = mats[Random.Range(0, mats.Length - 1)];
        }
    }
}
