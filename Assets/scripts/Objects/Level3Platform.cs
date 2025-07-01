using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Platform : MonoBehaviour
{
    [SerializeField] float speed;
    public Transform[] points;
    private int i;
    public bool activate;
    public Transform OG_P;
    bool reset;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            activate = false;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        if(reset==true)
        {
            resetPlatform();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            activate = true;
        }
        if (transform.position.y < other.transform.position.y)
        {
            other.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(OG_P);
            activate = false;
        }
    }
    void resetPlatform()
    {
        transform.position = points[0].position;
        reset = false;
    }
}
