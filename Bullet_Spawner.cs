using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet_Spawner : MonoBehaviour {
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShots;
    private float nextShotTimestamp;
    private AudioSource audioSource;
    private bool isIntro;
    [HideInInspector] public bool isPlayerDead;

    private void Awake()
    {
        isPlayerDead = false;
        nextShotTimestamp = Time.time;
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 0) { isIntro = true; } else { isIntro = false; }
    }

    public void Fire()
    {
        if (Time.time >= nextShotTimestamp && !isIntro && !isPlayerDead) { FiringSequence(); }
    }
    private void FiringSequence()
    {
        nextShotTimestamp = Time.time + timeBetweenShots;
        Firing();
    }
    private void Firing()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        var bulletController_OBJ = bullet.GetComponent<Bullet_Controller>();
        var clip = bulletController_OBJ.shotFired_CLIPS[Random.Range(0, bulletController_OBJ.shotFired_CLIPS.Length - 1)];
        audioSource.clip = clip;
        audioSource.Play();
    }
}
