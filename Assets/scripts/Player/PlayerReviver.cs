using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReviver : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerRespawn respawner;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawner = player.GetComponent<PlayerRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Revive()
    {
        respawner.respawn();
    }
}
