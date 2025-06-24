using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformTest : MonoBehaviour
{
    [SerializeField] float speed;
    public int startPoint;
    public Transform[] points;
    private int i;
    public bool activate;
    public GameObject PlatSprite;
    public Transform OG_P;

    void Start()
    {
        transform.position = points[startPoint].position;
        activate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == (points.Length-1))
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                PlatSprite.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (i == points.Length)
            {
                transform.position = points[startPoint].position;
                activate = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                PlatSprite.GetComponent<SpriteRenderer>().enabled = true;
                i = 0;
            }
        }
        if(activate==true)
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        activate = true;
        if (transform.position.y < other.transform.position.y)
        {
            other.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(OG_P);
    }
}
