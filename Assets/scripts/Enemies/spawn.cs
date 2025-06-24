using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    bool Summoned;

    void Awake()
    {
        Summoned = false;
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
