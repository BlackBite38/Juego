using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCrystal : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerHp playerHealth;
    [Header("Sounds")]
    [SerializeField] private AudioClip ItemGot;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySound(ItemGot);
            playerHealth.MaxHealth += 5;
            playerHealth.AddHP(playerHealth.MaxHealth);
            gameObject.SetActive(false);
        }
    }
}
