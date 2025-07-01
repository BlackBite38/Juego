using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] GameObject LeftKey, RightKey;
    [SerializeField] GameObject[] LeftLock, RightLock;
    [SerializeField] GameObject[] doors;
    [SerializeField] private SpriteRenderer[] spriteLeftLock, spriteRightLock;
    private bool leftLockOpen, rightLockOpen, activeDoors;
    [SerializeField] private bool leftButtonOn, rightButtonOn;
    [Header("Sounds")]
    [SerializeField] private AudioClip doorOpened;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < LeftLock.Length; i++)
        {
            spriteLeftLock[i] = LeftLock[i].GetComponent<SpriteRenderer>();
        }
        for (int j = 0; j < LeftLock.Length; j++) 
        { 
            spriteRightLock[j] = RightLock[j].GetComponent<SpriteRenderer>();
        }
        for (int e = 0; e < doors.Length; e++)
        {
            doors[e].GetComponent<BasementDoorMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(LeftKey.gameObject.activeInHierarchy==false || leftButtonOn==true)
        {
            for (int i = 0; i < LeftLock.Length; i++)
                spriteLeftLock[i].color = Color.green;
            leftLockOpen = true;
        }
        else
        {
            for (int i = 0; i < LeftLock.Length; i++)
                spriteLeftLock[i].color = Color.red;
            leftLockOpen=false;
        }
        if (RightKey.gameObject.activeInHierarchy == false || rightButtonOn==true)
        {
            for (int j = 0; j < LeftLock.Length; j++)
                spriteRightLock[j].color = Color.green;
            rightLockOpen = true;
        }
        else
        {
            for (int j = 0; j < LeftLock.Length; j++)
                spriteRightLock[j].color = Color.red;
            rightLockOpen=false;
        }
        if (leftLockOpen == true && rightLockOpen == true)
        {
            if (doorOpened != null)
            {
                SoundManager.instance.PlaySound(doorOpened);
            }
            activeDoors = true;
        }
        else
        {
            activeDoors = false;
        }
        if(activeDoors == true)
        {
            for (int e = 0; e < doors.Length; e++)
            {
                doors[e].GetComponent<BasementDoorMovement>().activeDoor=true;
            }
        }
        else
        {
            for (int e = 0; e < doors.Length; e++)
            {
                doors[e].GetComponent<BasementDoorMovement>().activeDoor = false;
            }
        }
    }
}
