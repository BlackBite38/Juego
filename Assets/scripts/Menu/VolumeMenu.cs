using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
}
