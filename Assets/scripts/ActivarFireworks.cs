using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarFireworks : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;
    [SerializeField] private AudioClip unlockSound;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerMove>().activeFireworks=true;
            SoundManager.instance.PlaySound(unlockSound);
            Destroy(gameObject);
        }
    }
}
