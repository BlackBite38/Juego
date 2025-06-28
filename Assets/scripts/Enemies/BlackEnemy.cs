using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : MonoBehaviour
{
    [SerializeField] private float AttackCooldown;
    private float CooldownTimer;
    [SerializeField] private float range, colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] bool hasGun, hasCannon;
    private Animator anim;
    [SerializeField] public bool flying;
    private EnemyPatroll enemyPatroll;

    [SerializeField] private GameObject[] projectiles, projectilesRight, projectilesLeft;
    [SerializeField] private Transform offset;

    [SerializeField] public bool goingLeft;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootSound;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatroll = GetComponentInParent<EnemyPatroll>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hasGun || hasCannon)
        {
            CooldownTimer += Time.deltaTime; 
        }
        if(PlayerDetected())
        {
            if (CooldownTimer >= AttackCooldown)
            {
                if(hasCannon)
                {
                    CooldownTimer = 0;
                    CannonShoot();
                }
                else if(hasGun)
                {
                    CooldownTimer = 0;
                    anim.SetTrigger("Shoot");
                    //Shoot();
                }
            }
        }
        if(goingLeft == true)
        {
            projectiles = projectilesLeft;
        }
        else
        {
            projectiles = projectilesRight;
        }
        if(enemyPatroll != null && !hasCannon)
        {
            enemyPatroll.enabled=!PlayerDetected();
        }
    }
    private bool PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, PlayerLayer);
        return hit.collider!=null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void CannonShoot()
    {
        CooldownTimer = 0;
        projectiles[FindBullets()].transform.position = offset.position;
        projectiles[FindBullets()].GetComponent<CannonBall>().ActivateProjectile();
    }
    private void Shoot()
    {
        CooldownTimer = 0;
        projectiles[FindBullets()].transform.position = offset.position;
        projectiles[FindBullets()].GetComponent<EnemyBlast>().ActivateProjectile();
    }
    private int FindBullets()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
