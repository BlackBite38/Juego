using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentHeightDoor : MonoBehaviour
{
    [SerializeField] private Transform PreviousArea;
    [SerializeField] private Transform NewArea;
    [SerializeField] private SegmentCamera Cam;
    // Start is called before the first frame update
    //private void Awake()
    //{
    //    Cam = Camera.main.GetComponent<SegmentCamera>();
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Player")
        {
            if (other.transform.position.y < transform.position.y)
            {
                Cam.MoveToNewPlaceY(NewArea);
            }
            else if (other.transform.position.y > transform.position.y) 
            {
                Cam.MoveToNewPlaceY(PreviousArea);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.transform.position.y < transform.position.y)
            {
                Cam.MoveToNewPlaceY(PreviousArea);
            }
            else if (other.transform.position.y > transform.position.y)
            {
                Cam.MoveToNewPlaceY(NewArea);
            }
        }
    }
}
