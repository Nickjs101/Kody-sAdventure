using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{   
    [SerializeField] private AudioMixer audioMixer;



    public void SetFXVolume(float volume) {
        audioMixer.SetFloat("FX", volume);
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("BG", volume);
    }


    public void ExitSettings() {
        gameObject.SetActive(false);
    }

}
