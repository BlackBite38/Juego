using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSideDoor : MonoBehaviour
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
            if (other.transform.position.x < transform.position.x)
            {
                Cam.MoveToNewPlaceX(NewArea);
            }
            else if (other.transform.position.x > transform.position.x)
            {
                Cam.MoveToNewPlaceX(PreviousArea);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.transform.position.x < transform.position.x)
            {
                Cam.MoveToNewPlaceX(PreviousArea);
            }
            else if (other.transform.position.x > transform.position.x)
            {
                Cam.MoveToNewPlaceX(NewArea);
            }
        }
    }
}
