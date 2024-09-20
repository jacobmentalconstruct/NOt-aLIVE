using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroy : MonoBehaviour {
    private void OnTriggerExit (Collider other)
    {
        //Debug.Log(other.tag + " is leaving the boundary");
        Destroy(other.gameObject);
    }
}
