using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataforms : MonoBehaviour
{
    [SerializeField] float speed;
    public int startPoint;
    public Transform[] points;
    private int i;
    public Transform OG_P;

    void Start()
    {
        transform.position=points[startPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position)<0.02f)
        {
            i++;
            if(i==points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (transform.position.y < other.transform.position.y)
        {
            other.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(OG_P);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
            other.transform.SetParent(OG_P);
    }
}
