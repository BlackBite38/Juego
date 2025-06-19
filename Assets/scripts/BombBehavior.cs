using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    [SerializeField] float timer = 0;
    [SerializeField] float timer2 = 0;
    Animator anim;
    [SerializeField] GameObject explosion;
    bool detection, exploded;
    public float spid;
    float speed;
    [SerializeField] PlayerMove Player;
    [SerializeField] float Direction;
    bool collided;
    public int power;
    [SerializeField] float timer3;
    //private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        speed = spid;
        timer3 = 0;
        Player = GameObject.FindObjectOfType<PlayerMove>();
        Direction = Player.GetComponent<PlayerMove>().direction;
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
        //var force = Vector3.up;
        //GetComponent<Rigidbody2D>().AddForce(force*speed*power, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer3<1)
        {
            timer3 += Time.deltaTime;
            transform.position += transform.up * Time.deltaTime * power;
        }
        if (Direction == 1 && collided == false)
        {
            speed = spid;
        }
        else if (Direction == -1 && collided == false)
        {
            speed = -(spid);
        }
        else if (collided == true) 
        {
            speed = 0;
        }
        transform.position += transform.right * Time.deltaTime * speed;

        timer += Time.deltaTime;
        if (detection)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= 3)
            {
                explode();
            }
        }
        if (timer >=5 && !detection)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= 3)
            {
                explode();
            }
        }
    }
    private void explode()
    {
        Instantiate(explosion, gameObject.transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "Firework" && other.gameObject.tag != "Player")
        {
            timer = 0;
            collided=true;
            detection = true;
            anim.SetBool("Active", true);
        }
        else if(other.gameObject.tag == "Firework")
        {
            explode();
        }
        else if(other.gameObject.tag =="Enemy")
        {
            explode();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Firework" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyProyectile")
        {
            explode();
        }
    }
}
