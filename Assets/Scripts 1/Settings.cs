using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle hardGameToggle, helpTextToggle;
    public AudioSource audioSource;
    public AudioClip audioClip;
    private bool sound;

    void Start()
    {
        sound = false;
        hardGameToggle.isOn = PlayerPrefs.GetString("gameDifficulty") != "EasyGame";
        helpTextToggle.isOn = PlayerPrefs.GetString("helpTextVisibilty") == "true";
    }

    public void UpdateGameDifficulty(bool hard)
    {
        PlayerPrefs.SetString("gameDifficulty", hard ? "HardGame" : "EasyGame");
    }

    public void UpdateHelpTextVisibilty(bool visible)
    {
        PlayerPrefs.SetString("helpTextVisibilty", visible ? "true" : "false");
    }

    void Update()
    {
        sound = true;
    }

    public void onToggleSound ()
    {
        if (sound)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
