using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public bool Summoned, bossDead;

    public AudioClip[] songs;
    [SerializeField] AudioSource audio;
    void Awake()
    {
        bossDead = false;
        Summoned = false;
        audio.clip = songs[0];
    }
    void Update()
    {
        if(Summoned==true && !bossDead)
        {
            if (audio.clip != songs[1])
            {
                audio.Stop();
                audio.clip = songs[1];
                audio.Play();
            }
        }
        else
        {
            if(audio.clip != songs[0])
            {
                audio.Stop();
                audio.clip = songs[0];
                audio.Play();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Summoned==false)
        {
            enemy.gameObject.SetActive(true);
            Summoned=true;
        }
    }
}
