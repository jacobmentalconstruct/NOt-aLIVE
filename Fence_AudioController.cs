using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence_AudioController : MonoBehaviour
{
	[SerializeField] private AudioClip build;
	[SerializeField] private AudioClip repair;
	[SerializeField] private AudioClip hit;
	[SerializeField] private AudioClip die;
	private AudioSource _PLAYER;

	private void Awake(){
		_PLAYER = GetComponent<AudioSource>();
	}

	public void BUILD(){ 
		_PLAYER.clip = build;
		_PLAYER.Play();
	}
	public void REPAIR(){ 
		_PLAYER.clip = repair;
		_PLAYER.Play();
	}
	public void HIT(){ 
		_PLAYER.clip = hit;
		_PLAYER.Play();
	}
	public void DIE(){ 
		_PLAYER.clip = die;
		_PLAYER.Play();
	}
}