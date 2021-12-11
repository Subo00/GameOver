using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameSceneButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
