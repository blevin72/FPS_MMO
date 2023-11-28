using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;

    void Start()
    {
        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();

        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is moving (based on Rigidbody velocity)
        if (rb.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            // Play the audio if not already playing
            audioSource.Play();
        }
        else if (rb.velocity.magnitude < 0.1f && audioSource.isPlaying)
        {
            // Stop the audio if the player has stopped moving
            audioSource.Stop();
        }
    }
}
