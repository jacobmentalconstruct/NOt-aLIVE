using UnityEngine;
using System.Collections;

public class Enemy_Audio : MonoBehaviour
{
    private Enemy_refLIB enemyLib;

    private void Awake()
    {
        enemyLib = GetComponent<Enemy_refLIB>();

        StartCoroutine(Footsteps());
        StartCoroutine(Moan());
    }

    IEnumerator Footsteps()
    {
        enemyLib.walkAudioSource.volume = Random.Range(enemyLib.walkMinVol, enemyLib.walkMaxVol);
        enemyLib.walkAudioSource.pitch = Random.Range(enemyLib.walkMinPitch, enemyLib.walkMaxPitch);
        enemyLib.walkAudioSource.clip = enemyLib.walkClips[Random.Range(0, enemyLib.walkClips.Length)];
        enemyLib.walkAudioSource.Play();
        yield return new WaitForSeconds(enemyLib.movementSpeed);
    }

    IEnumerator Moan()
    {
        while (true)
        {
            if (Random.Range(0, 10) == 2)
            {
                enemyLib.cryAudioSource.pitch = Random.Range(enemyLib.cryMinPitch, enemyLib.cryMaxPitch);
                enemyLib.cryAudioSource.volume = Random.Range(enemyLib.cryMinVol, enemyLib.cryMaxVol);
                enemyLib.cryAudioSource.clip = enemyLib.cry_CLIP[Random.Range(0, enemyLib.cry_CLIP.Length)];
                enemyLib.cryAudioSource.Play();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
