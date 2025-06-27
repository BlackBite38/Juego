using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Transform Top, Bottom;
    //private Rigidbody2D rb;
    [SerializeField] int state;
    [SerializeField] float risingSpeed, attackSpeed;
    Animator anim;
    private float timer;
    [SerializeField] private bool Attacking;

    [SerializeField] private float range, colliderDistance;
    [SerializeField] private BoxCollider2D DetectorCollider, AttackDetector;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private float resetTimer;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        state = 0;
        anim = GetComponent<Animator>();
        Attacking = false;
        resetTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            anim.SetBool("Attack", false);
            transform.position = Vector2.MoveTowards(transform.position, Top.position, risingSpeed * Time.deltaTime);
            if (transform.position == Top.position && resetTimer>0)
            {
                resetTimer -= Time.deltaTime;
            }
        }
        if (state == 1)
        {
            anim.SetBool("Attack", true);
            Attack();
            if (transform.position == Bottom.position)
            {
                timer += Time.deltaTime;
                if (timer > 0.25f)
                {
                    Attacking = false;
                    state = 0;
                    timer = 0;
                    resetTimer = 0.25f;
                }
            }
        }

        if (CheckForPlayer1())
        {
            anim.SetBool("Detected", true);
        }
        else
        {
            anim.SetBool("Detected", false);
        }
        if (CheckForPlayer2() && Attacking == false)
        {
            if(resetTimer<=0)
            {
                Attacking = true;
                print("a");
            }
        }
        if (Attacking == true)
        {
            state = 1;
        }
    }
    private void Attack()
    {
        anim.SetBool("Attack", true);
        transform.position = Vector2.MoveTowards(transform.position, Bottom.position, attackSpeed * Time.deltaTime);
    }
    private bool CheckForPlayer1()
    {
        RaycastHit2D hit = Physics2D.BoxCast(DetectorCollider.bounds.center + transform.up * -1 * colliderDistance,
            new Vector3(DetectorCollider.bounds.size.x, DetectorCollider.bounds.size.y * range, DetectorCollider.bounds.size.z), 0, Vector2.up, 0, PlayerLayer);
        return hit.collider != null;
    }
    private bool CheckForPlayer2()
    {
        RaycastHit2D hit = Physics2D.BoxCast(AttackDetector.bounds.center + transform.up * -1 * colliderDistance,
            new Vector3(AttackDetector.bounds.size.x, AttackDetector.bounds.size.y * range, AttackDetector.bounds.size.z), 0, Vector2.up, 0, PlayerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(DetectorCollider.bounds.center + transform.up * -1 * colliderDistance,
            new Vector3(DetectorCollider.bounds.size.x, DetectorCollider.bounds.size.y * range, DetectorCollider.bounds.size.z));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackDetector.bounds.center + transform.up * -1 * colliderDistance,
            new Vector3(AttackDetector.bounds.size.x, AttackDetector.bounds.size.y * range, AttackDetector.bounds.size.z));
    }
}

