using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider sfxSlider;
    void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0f);

        audioMixer.SetFloat("Music", musicVol);
        audioMixer.SetFloat("SFX", sfxVol);

        if (musicSlider != null)
            musicSlider.value = musicVol;
        if (sfxSlider != null)
            sfxSlider.value = sfxVol;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
}
