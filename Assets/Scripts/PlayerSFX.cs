using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField]
    AudioClip walk;
    [SerializeField]
    AudioClip hitWall;
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    public float volume;
    public void PlayWalk()
    {
        audioSource.clip = walk;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopWalk()
    {
        audioSource.Stop();
    }

    public void PlayHitWall()
    {
        audioSource.PlayOneShot(hitWall, volume);
    }
}
