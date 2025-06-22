using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementTest : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    Animator anim;
    [SerializeField] float speed;
    [SerializeField] public int state;
    [SerializeField] float timeReset;
    [SerializeField] public float timer;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        state = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speed!=0)
        {
            timer += Time.deltaTime;
        }
        if (timer>=timeReset)
        {
            state +=1;
            timer = 0;
        }
        if (state>=4)
        {
            state = 0;
        }
        if (state == 0 || state ==2)
        {
            anim.SetBool("Walking", false);
        }
        else if (state == 1)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            anim.SetBool("Walking", true);
            spriteRenderer.flipX = false;
        }
        else if (state == 3)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            spriteRenderer.flipX = true;
            anim.SetBool("Walking", true);
        }
        //void OnTriggerEnter2D(Collider2D other) //Collission Collision2D
        //{ 
        //    if(other.tag == "wall") //gameObject.
        //    {
        //        state += 1;
        //        timer = 0;
        //    }
        //}
    }
}
