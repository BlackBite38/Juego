using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField] Elevator _elevator;
    
    // Start is called before the first frame update
    void Awakw()
    {
        _elevator.GetComponent<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if (Input.GetKeyDown(KeyCode.X) && _elevator.GetComponent<Elevator>().active == false)
            {
                _elevator.GetComponent<Elevator>().active = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.X) && _elevator.GetComponent<Elevator>().active == false)
            {
                _elevator.GetComponent<Elevator>().active = true;
            }
        }
    }
}
