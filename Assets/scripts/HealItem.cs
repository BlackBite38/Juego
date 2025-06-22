using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private float Healing;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHp>().AddHP(Healing);
            gameObject.SetActive(false);
        }
    }
}
