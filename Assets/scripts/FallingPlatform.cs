using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool activate;
    [SerializeField] float timer1;
    [SerializeField] float timer2;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activate==true)
        {
            timer1 += Time.deltaTime;
        }
        if(timer1>=2)
        {
            anim.SetBool("fall", true);
            timer2 += Time.deltaTime;
        }
        if (timer1>=3)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (timer2>=3)
        {
            timer1 = 0;
            timer2 = 0;
            anim.SetBool("fall", false);
            anim.SetBool("active", false);
            activate = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            activate = true;
            anim.SetBool("active", true);
        }
    }
}
