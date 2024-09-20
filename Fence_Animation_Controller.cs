using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence_Animation_Controller : MonoBehaviour
{
    private Animator _ANIM;

    void Awake(){
    _ANIM = transform.GetComponent<Animator>();
    }

    public void _BUILD(){ _ANIM.SetTrigger("Build"); }
    public void _REPAIR(){ _ANIM.SetTrigger("Repair"); }
    public void _HIT(){ _ANIM.SetTrigger("Hit"); }
    public void _DIE(){ _ANIM.SetTrigger("Die"); }

}
