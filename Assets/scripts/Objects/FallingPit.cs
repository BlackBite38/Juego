using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPit : MonoBehaviour
{
    [SerializeField] GameObject player;
    float damage;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        damage = player.GetComponent<PlayerHp>().CurrentHealth;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Player")
        {
            player.GetComponent<PlayerHp>().TakeDamage(damage);
        }
    }
}
