using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private float decay;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, decay);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AI"))
        {
            Debug.Log("Projectile Hit: AI");
        }
        audioSource.Play();
    }
}
