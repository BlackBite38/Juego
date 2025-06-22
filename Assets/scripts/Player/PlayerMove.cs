using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    private float spid;
    [SerializeField] float jump;
    private Rigidbody2D rb;
    Animator anim;
    //[SerializeField] bool Grounded;
    [SerializeField] bool Crouching;
    private BoxCollider2D boxCollider;
    [SerializeField] LayerMask groundLayer, wallLayer;
    private float wallJumpCooldown;
    private float HorizontalInput;
    [Header("Attacks")]
    [SerializeField] public float AttackCooldown;
    [SerializeField] public int weaponType;   //    weapon[];
    [SerializeField] GameObject BombT, BombP, BombTCharged, BombPCharged; // Firework, FireworkCharged;
    [SerializeField] public Transform BombOffset1, BombOffset2, BombOffset3, FireworkOffset, BombOffsetCharged1, BombOffsetCharged2, BombOffsetCharged3;
            //[SerializeField] private GameObject[] FireworksAmmo, ChargedFireworksAmmo;
    // bool chargeUnlocked;
    public float chargeAmount;
    public float direction;

    public float canShoot;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        direction = 1;
        spid=speed;
        chargeAmount = 0;
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
            direction = 1;
        }
        else if (HorizontalInput < -0.1)
        {
            anim.SetBool("FacingLeft", true);
            transform.localScale = new Vector3(-1, 1, 1);
            direction=-1;
        }
        anim.SetBool("Run", HorizontalInput != 0);
        anim.SetBool("OnGround", isGrounded()); //Grounded);
        //agacharse
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded()) //Grounded)
        {
            anim.SetBool("Crouch", true);
            Crouching = true;
            spid = 0;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("Crouch", false);
            Crouching = false;
            spid = speed;
        }
        //salto en paredes
        if (wallJumpCooldown > 0.2f)
        {
            rb.velocity = new Vector2(HorizontalInput * spid, rb.velocity.y);
            
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
        //armas
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
            weaponType+=1;
        }
        if(weaponType>1)
        {
            weaponType = 0;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            chargeAmount += Time.deltaTime;
            if(chargeAmount > 1)
            {
                chargeAmount = 1;
            } 
        }
        if (Input.GetKeyUp(KeyCode.Z) && AttackCooldown==0)
        {
            if (weaponType == 0)
            {
                //if(chargeUnlocked==true)
                //else
                if (chargeAmount < 1)
                {
                    BombThrow();
                    chargeAmount = 0;
                }
                else
                {
                    BigBombThrow();
                    chargeAmount = 0;
                }
            }
            else if (weaponType == 1)
            {
                return;
                //if(chargeUnlocked==true)     ChargedFireworkThrow();
                //else
                //FireworkThrow();
            }
        }
        if (onWall() && !isGrounded())
        {
            canShoot = 1;
        }
        else if(Crouching==true)
        {
            canShoot = 1;
        }
        else
        {
            canShoot=0;
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
    void BombThrow()
    {
        if (!Crouching && !onWall())
        {
            anim.SetTrigger("BombThr");
            Instantiate(BombT, BombOffset1.position, transform.rotation);
            AttackCooldown += 1;
        }
        else if (onWall() && !isGrounded())
        {
            Instantiate(BombP, BombOffset2.position, transform.rotation);
            AttackCooldown += 1;
        }
        else
        {
            Instantiate(BombP, BombOffset3.position, transform.rotation);
            AttackCooldown += 1;
        }
    }
    void BigBombThrow()
    {
        if (!Crouching && !onWall())
        {
            anim.SetTrigger("BombThr");
            Instantiate(BombTCharged, BombOffsetCharged1.position, transform.rotation);
            AttackCooldown += 1;
        }
        else if (onWall() && !isGrounded())
        {
            Instantiate(BombPCharged, BombOffsetCharged2.position, transform.rotation);
            AttackCooldown += 1;
        }
        else
        {
            Instantiate(BombPCharged, BombOffsetCharged3.position, transform.rotation);
            AttackCooldown += 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            // se activa al chocar con el enemigo
        }
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
    //public void FireworkThrow()
    //{
    //    if (!Crouching)
    //    {
    //        if (onWall() && !isGrounded())
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            anim.SetTrigger("FireThr");
    //            FireworksAmmo[FindFirework()].transform.position = Offset1.position;
    //            FireworksAmmo[FindFirework()].GetComponent<projectileTest>().SetDirection(Mathf.Sign(transform.localScale.x));
    //            AttackCooldown += 1;
    //            
    //            //Instantiate(Firework, Offset1.position, transform.rotation);
    //        }
    //    }
    //}
    //private int FindFirework()
    //{
    //    for(int i =0; i<FireworksAmmo.Length; i++)
    //    {
    //        if (!FireworksAmmo[i].activeInHierarchy)
    //        {
    //            return i;
    //        }
    //    }
    //    return 0;
    //}
}
