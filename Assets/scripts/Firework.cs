using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed=5;
    [SerializeField] GameObject explosion;
    private float direction;
    public bool facedRight;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float movementSpeed= speed* Time.deltaTime*direction;
        //transform.Translate(movementSpeed, 0, 0);
        if(facedRight==true)
        {
            direction = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            direction = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        float vel=speed*direction;
        rb.velocity = new Vector2(vel, rb.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion, gameObject.transform.position, transform.rotation);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosion, gameObject.transform.position, transform.rotation);
        Destroy(gameObject);
    }
    //public void SetDirection(float _direction)
    //{
    //    direction = _direction;

    //     float LocalScaleX = transform.localScale.x;
    //     if(Mathf.Sign(LocalScaleX)!=_direction)
    //    {
    //        LocalScaleX=-LocalScaleX;
    //    }
    //    transform.localScale = new Vector3(LocalScaleX, transform.localScale.y, transform.localScale.z);
    //}
}
