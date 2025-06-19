using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCamera : MonoBehaviour
{
    [SerializeField] private float speed;
    private float CurrentPosX, CurrentPosY;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(CurrentPosX, CurrentPosY, transform.position.z), ref velocity, speed);
    }
    public void MoveToNewPlaceX(Transform newPlaceX)
    {
        CurrentPosX = newPlaceX.position.x;
        CurrentPosY = newPlaceX.position.y;
    }
    public void MoveToNewPlaceY(Transform newPlaceY)
    {
        CurrentPosX = newPlaceY.position.x;
        CurrentPosY = newPlaceY.position.y;
    }
}
