using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private float Healing;
    [Header("Sounds")]
    [SerializeField] private AudioClip healSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHp>().AddHP(Healing);
            SoundManager.instance.PlaySound(healSound);
            gameObject.SetActive(false);
        }
    }
}
