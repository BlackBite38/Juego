using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_test : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;

    public void SoundPlay()
    {
        int i = Random.Range(0, sounds.Length);
        SoundManager.instance.PlaySound(sounds[i]);
    }
}
