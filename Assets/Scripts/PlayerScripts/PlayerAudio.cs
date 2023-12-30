using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private CharacterController characterController;

    void Start()
    {
        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();

        // Get the CharacterController component attached to the player
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is moving (based on CharacterController velocity)
        if (characterController.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            // Play the audio if not already playing
            audioSource.Play();
        }
        else if (characterController.velocity.magnitude < 0.1f && audioSource.isPlaying)
        {
            // Stop the audio if the player has stopped moving
            audioSource.Stop();
        }
    }
}
