using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDoorMovement : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] private Transform Area1, Area2, area1Cam, area2Cam;
    [SerializeField] private SegmentCamera Cam;
    [SerializeField] private bool inArea1, inArea2;

    public bool activeDoor;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeDoor==true)
        {
            if (anim != null)
                anim.SetBool("OpenDoor", true);
        }
        else
        {
            if (anim != null)
                anim.SetBool("OpenDoor", false);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && activeDoor==true && Input.GetKeyDown(KeyCode.X))
        {
            if (inArea1==true)
            {
                player.transform.position = Area2.position;
                if (Cam != null) 
                    Cam.MoveToNewPlaceY(area2Cam);
            }
            if (inArea2 == true)
            {
                player.transform.position = Area1.position;
                if (Cam != null)
                    Cam.MoveToNewPlaceY(area1Cam);
            }
        }
    }
}
