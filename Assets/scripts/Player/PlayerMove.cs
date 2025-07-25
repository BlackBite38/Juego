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

    [SerializeField] float wallJumpX, wallJumpY;
    [SerializeField] BoxCollider2D hitBox;

    [Header("Attacks")]
    [SerializeField] public float AttackCooldown;
    [SerializeField] public int weaponType;   //    weapon[];
    [SerializeField] GameObject BombT, BombP, BombTCharged, BombPCharged; // Firework, FireworkCharged;
    [SerializeField] public Transform BombOffset1, BombOffset2, BombOffset3, FireworkOffset, BombOffsetCharged1, BombOffsetCharged2, BombOffsetCharged3;
    //[SerializeField] private GameObject[] FireworksAmmo, ChargedFireworksAmmo;
    [SerializeField] public bool activeFireworks, chargeUnlocked;
    public float chargeAmount;
    public float direction;

    public float canShoot;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    [SerializeField]  private float coyoteCount;

    [Header("Sounds")]
    [SerializeField] private AudioClip JumpSound, throwBombSound;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        hitBox = boxCollider;
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
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded() || Input.GetKeyDown(KeyCode.S) && isGrounded()) //Grounded)
        {
            anim.SetBool("Crouch", true);
            Crouching = true;
            spid = 0;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Crouch", false);
            Crouching = false;
            spid = speed;
        }
        //salto en paredes antiguo
        if (wallJumpCooldown > 0.2f)
        {
            rb.velocity = new Vector2(HorizontalInput * spid, rb.velocity.y);

            if (onWall() )// && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = 2;
                if (isGrounded())
                {
                    coyoteCount = coyoteTime;
                }
                else
                    coyoteCount -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space)) //Grounded && !Crouching)
            {
                Jump();
            }
            if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
        //armas
        if (AttackCooldown>0)
        {
            AttackCooldown -= Time.deltaTime*2;
        }
        else if (AttackCooldown<0)
        {
            AttackCooldown = 0;
        }
        if (Input.GetKeyDown(KeyCode.C) && activeFireworks==true)
        {
            weaponType+=1;
        }
        if(weaponType>1)
        {
            weaponType = 0;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if(chargeUnlocked)
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
            }
        }
        if (onWall() && !isGrounded())
        {
            canShoot = 1;
        }
        else if(Crouching==true)
        {
            canShoot = 1;
            hitBox.size = new Vector2(1.1f, 0.85f);
            hitBox.offset = new Vector2(0.05f,0.425f);
        }
        else
        {
            canShoot=0;
            hitBox.size = new Vector2(1.1f, 1.75f);
            hitBox.offset = new Vector2(0.05f, 0.875f);
        }
    }
    void Jump()
    {
        if(coyoteCount<=0 && !onWall()) return;

        //anim.SetTrigger("Jump");
        //SoundManager.instance.PlaySound(JumpSound);

        if(onWall())
        {
            WallJump();
        }
        else
        {
            if(isGrounded() && !Crouching)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                anim.SetTrigger("Jump");
                SoundManager.instance.PlaySound(JumpSound);
            }
            else if(!isGrounded() && !Crouching)
            {
                if(coyoteCount>0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jump);
                    anim.SetTrigger("Jump");
                    SoundManager.instance.PlaySound(JumpSound);
                }
                coyoteCount = 0;
            }
            
        }

        //if (isGrounded() && !Crouching)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jump);
        //    anim.SetTrigger("Jump");
        //    SoundManager.instance.PlaySound(JumpSound);
        //    //Grounded = false;
        //}
        //else if(onWall() && !isGrounded())
        //{
        //    if (HorizontalInput == 0)
        //    {
        //        rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
        //        anim.SetTrigger("Jump");
        //    }
        //    else
        //    {
        //        rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 7);
        //        transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //        anim.SetTrigger("Jump");
        //        SoundManager.instance.PlaySound(JumpSound);
        //    }
        //    wallJumpCooldown = 0;
        //}
    }
    private void WallJump()
    {
        anim.SetTrigger("Jump");
        SoundManager.instance.PlaySound(JumpSound);
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown= 0;
    }
    void BombThrow()
    {
        if (!Crouching && !onWall())
        {
            anim.SetTrigger("BombThr");
            SoundManager.instance.PlaySound(throwBombSound);
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
            SoundManager.instance.PlaySound(throwBombSound);
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
        //anterior metodo de salto
        //if (collision.gameObject.tag == "Ground")
        //{
        //    Grounded = true;
        //}
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<PlatformEffector2D>())
        {
            if (Input.GetKey(KeyCode.S) && (Input.GetKeyDown(KeyCode.Space)) || Input.GetKey(KeyCode.DownArrow) && (Input.GetKeyDown(KeyCode.Space)))
            {
                gameObject.transform.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.GetComponent<PlatformEffector2D>())
        {
            gameObject.transform.GetComponent<Collider2D>().isTrigger = false;
        }
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
