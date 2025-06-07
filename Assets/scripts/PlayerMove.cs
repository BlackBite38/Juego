using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jump;
    private Rigidbody2D rb;
    Animator anim;
    //[SerializeField] bool Grounded;
    [SerializeField] bool Crouching;
    private BoxCollider2D boxCollider;
    [SerializeField] LayerMask groundLayer, wallLayer;
    private float wallJumpCooldown;
    private float HorizontalInput;
    [SerializeField] private float AttackCooldown;
    [SerializeField] int a;   //    weapon[];
    [SerializeField] GameObject Bomb, Firework, BombCharged, FireworkCharged;
    public Transform Offset;
    int HP;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        //girar jugador al moverse
        if (HorizontalInput > 0.1)
        {
            anim.SetBool("FacingLeft", false);
            transform.localScale = Vector3.one;
        }
        else if (HorizontalInput < -0.1)
        {
            anim.SetBool("FacingLeft", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        anim.SetBool("Run", HorizontalInput != 0);
        anim.SetBool("OnGround", isGrounded()); //Grounded);
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded()) //Grounded)
        {
            anim.SetBool("Crouch", true);
            Crouching = true;
            speed = 0;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("Crouch", false);
            Crouching = false;
            speed = 7;
        }
        if (wallJumpCooldown > 0.2f)
        {
            rb.velocity = new Vector2(HorizontalInput * speed, rb.velocity.y);
            
            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = 2;
            }
            
            if (Input.GetKeyDown(KeyCode.Space)) //Grounded && !Crouching)
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
        if(AttackCooldown>0)
        {
            AttackCooldown -= Time.deltaTime*2;
        }
        else if (AttackCooldown<0)
        {
            AttackCooldown = 0;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            a+=1;
        }
        if(a>1)
        {
            a = 0;
        }
        if (Input.GetKeyDown(KeyCode.Z) && AttackCooldown==0)
        {
            Attack();
            AttackCooldown += 1;
        }
    }
    void Jump()
    {
        if(isGrounded() && !Crouching)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetTrigger("Jump");
            //Grounded = false;
        }
        else if(onWall() && !isGrounded())
        {
            if (HorizontalInput == 0)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
            }
            else
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 7);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            wallJumpCooldown = 0;
            
        }
    }
    void Attack()
    {
        if(a==0)
        {
            if(!Crouching)
            {
                anim.SetTrigger("BombThr");
                Instantiate(Bomb, Offset.position, transform.rotation);
            }
            else
            {
                Instantiate(Bomb, Offset.position, transform.rotation);
            }
        }
        else if(a==1 && !Crouching)
        {
            anim.SetTrigger("FireThr");
            Instantiate(Firework, Offset.position, transform.rotation);
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //anterior metodo de salto
        //if (collision.gameObject.tag == "Ground")
        //{
        //    Grounded = true;
        //}
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down,0.1f, groundLayer);
        return raycastHit.collider !=null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
