using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float doorSpeed, elevatorSpeed;
    [SerializeField] GameObject button, doors, elevator;
    [SerializeField] Transform doorsOpen, doorsClosed;
    [SerializeField] Transform floorUp, floorDown;
    [SerializeField] GameObject Explo;
    [SerializeField] private bool explodable, activeTimer;
    [SerializeField] public bool active, onBottomFloor;
    [SerializeField] private bool closedDoors, goingUp, goingDown;
    [SerializeField] private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        active = false;
        closedDoors = false;
        onBottomFloor= false;
        goingUp = false;
        goingDown = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active==true)
        {
            if(closedDoors == false)
                doors.transform.position = Vector2.MoveTowards(doors.transform.position, doorsClosed.position, doorSpeed * Time.deltaTime);
        }
        else
        {
            doors.transform.position = Vector2.MoveTowards(doors.transform.position, doorsOpen.position, doorSpeed * Time.deltaTime);
        }
        if (doors.transform.position==doorsClosed.transform.position)
        {
            closedDoors =true;
        }
        else if(doors.transform.position == doorsOpen.transform.position)
        {
            closedDoors =false;
        }
        if(closedDoors == true && onBottomFloor==false && active)
        {
            elevator.transform.position = Vector2.MoveTowards(elevator.transform.position, floorDown.position, elevatorSpeed * Time.deltaTime);
            goingDown = true;
        }
        else if(closedDoors == true && onBottomFloor== true && active)
        {
            elevator.transform.position = Vector2.MoveTowards(elevator.transform.position, floorUp.position, elevatorSpeed * Time.deltaTime);
            goingUp = true;
        }
        if (goingUp == true && elevator.transform.position == floorUp.position)
        {
            active=false;
            onBottomFloor = false;
            goingUp=false;
            
            if (explodable==true)
            {
                elevator.gameObject.SetActive(false);
                Instantiate(Explo, floorUp.position, transform.rotation);
                activeTimer=true;
            }
        }
        if (goingDown == true && elevator.transform.position == floorDown.position)
        {
            active = false;
            onBottomFloor=true;
            goingDown=false;

            if (explodable==true)
            {
                elevator.gameObject.SetActive(false);
                Instantiate(Explo, floorDown.position, transform.rotation);
                activeTimer = true;
            }
        }
        if (activeTimer == true)
        {
            timer += Time.deltaTime;
        }
        if (timer > 0.5f)
        {
            if (onBottomFloor == true)
            {
                elevator.transform.position = floorUp.position;
                onBottomFloor=false;
            }
            else if (onBottomFloor == false)
            {
                elevator.transform.position = floorDown.position;
                onBottomFloor = true;
            }
            timer = 0;
            activeTimer = false;
            elevator.gameObject.SetActive(true);
        }
    }
}
