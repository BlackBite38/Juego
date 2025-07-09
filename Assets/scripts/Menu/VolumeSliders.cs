using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    //public AudioMixer musicMixer, sfxMixer;

    public Slider musicSlider;
    public Slider sfxSlider;
    public GameObject[] music, sonidos;

    private void Awake()
    {
        music = GameObject.FindGameObjectsWithTag("audioMusica");
        sonidos = GameObject.FindGameObjectsWithTag("audioSFX");

        musicSlider.value = PlayerPrefs.GetFloat("musicSave", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxSave", 1f);
    }
    private void Update()
    {
        foreach (GameObject mus in music) 
        {
            mus.GetComponent<AudioSource>().volume = musicSlider.value;
        }
        foreach (GameObject SFXs in sonidos)
        {
            SFXs.GetComponent<AudioSource>().volume = sfxSlider.value;
        }
    }
    public void SaveMusic()
    {
        PlayerPrefs.SetFloat("musicSave", musicSlider.value);
    }
    public void SaveSFX()
    {
        PlayerPrefs.SetFloat("sfxSave", sfxSlider.value);
    }
    //public void SetVolume()
    //{
    //    PlayerPrefs.musicMixer.SetFloat("Music", musicSlider.value);
    //    PlayerPrefs.sfxMixer.SetFloat("SFX", sfxSlider.value);
    //}
}
