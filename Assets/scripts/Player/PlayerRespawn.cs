using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private PlayerHp health;
    // Start is called before the first frame update
    private void Awake()
    {
        health = GetComponent<PlayerHp>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void respawn()
    {
        transform.position = currentCheckpoint.position;
        health.Respawn();

        if(Camera.main.GetComponent<SegmentCamera>() != null)
        {
            Camera.main.GetComponent<SegmentCamera>().MoveToNewPlaceX(currentCheckpoint.parent);
            //Camera.main.GetComponent<SegmentCamera>().MoveToNewPlaceY(currentCheckpoint.parent);
        }
        else if(Camera.main.GetComponent<Level3Camera>() != null)
        {
            Camera.main.GetComponent<Level3Camera>().MoveToNewPlaceY(currentCheckpoint.parent);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag=="Checkpoint" && health.CurrentHealth > 0)
        {
            currentCheckpoint = other.transform;
            other.GetComponent<Animator>().SetBool("Active", true);
        }
    }
}
