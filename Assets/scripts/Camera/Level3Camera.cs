using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Camera : MonoBehaviour
{
    public GameObject player;
    private float CurrentPosY;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, CurrentPosY, transform.position.z);
    }
    public void MoveToNewPlaceY(Transform newPlaceY)
    {
        CurrentPosY = newPlaceY.position.y;
    }
}
