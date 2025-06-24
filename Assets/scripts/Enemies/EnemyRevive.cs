using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRevive : MonoBehaviour
{
    [SerializeField] GameObject Enemy, Player;
    [SerializeField] Transform InitialPos;
    [SerializeField] float distance;

//    [SerializeField] private float testTimer;
    [SerializeField] private bool isDead;
    // Start is called before the first frame update
    void Awake()
    {
        InitialPos.position = Enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.gameObject.activeInHierarchy == false)
        {
            isDead = true;
        }
        float side = Enemy.transform.position.x - Player.transform.position.x;
        float height = Enemy.transform.position.y - Player.transform.position.y;
        if (side < -distance || side > distance || height < -distance || height > distance)
        {   
            Enemy.transform.position = InitialPos.position;
            Enemy.GetComponent<EnemyHP>().Health = Enemy.GetComponent<EnemyHP>().MaxHealth;
            if(isDead==true) 
            {
                Enemy.gameObject.SetActive(true);
                isDead = false;
            }
        }
    }
}
