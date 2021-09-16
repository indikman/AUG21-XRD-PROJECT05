using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    public Transform buttonUpPosition;
    public Transform buttonDownPosition;
    public Transform buttonObject;

    [Header("Audio Clips")]
    public AudioClip buttonPressed;
    public AudioClip buttonReleased;

    [Header("Button Events")]
    public UnityEvent OnPressed;
    public UnityEvent OnReleased;

    private AudioSource audioSource;

    void Start()
    {
        buttonObject.position = buttonUpPosition.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            // Button should be pressed
            buttonObject.position = buttonDownPosition.position;
            audioSource.PlayOneShot(buttonPressed);

            OnPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            // Button should be released
            buttonObject.position = buttonUpPosition.position;
            audioSource.PlayOneShot(buttonReleased);

            OnReleased?.Invoke();
        }
    }
}
