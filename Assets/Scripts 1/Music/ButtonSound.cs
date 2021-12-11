using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    private GameObject activeObject;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Awake()
    {
        activeObject = transform.gameObject;

        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        
        if (activeObject != null)
        {
            Destroy(activeObject, .5f);
        }
    }
}
