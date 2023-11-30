using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISFX : MonoBehaviour
{
    [SerializeField]
    AudioClip die;
    [SerializeField]
    AudioClip respawn;
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    public float volume;
    public void PlayDie()
    {
        audioSource.PlayOneShot(die, volume);
    }

    public void PlayRespawn()
    {
        audioSource.PlayOneShot(respawn, volume);
    }
}
