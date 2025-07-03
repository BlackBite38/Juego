using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float AttackCooldown;
    [SerializeField] private float CooldownTimer;
    [SerializeField] private float range, colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private float waitTimer;
    
    [SerializeField] private float side;
    [SerializeField] private GameObject Player;
    private Vector3 scaleChangeRight, scaleChangeLeft;

    [SerializeField] private GameObject[] projectiles, projectilesLeft, projectilesRight;
    [SerializeField] private Transform offsetLeft, offsetRight, currentOffset;
    [Header("Random Shooting")]
    [SerializeField] private bool isRandom;
    [SerializeField] private float RanMin, RanMax;

    [Header("Sounds")]
    [SerializeField] private AudioClip shootSound;
    // Start is called before the first frame update
    void Awake()
    {
        side = 1;
        scaleChangeLeft = new Vector3(1, 1, 1);
        scaleChangeRight = new Vector3(-1, 1, 1);
        currentOffset=offsetLeft;
        Player = GameObject.FindGameObjectWithTag("Player");
        if(isRandom)
        {
            AttackCooldown = Random.Range(RanMin, RanMax);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CooldownTimer += Time.deltaTime;
        if (PlayerDetected())
        {
            if (CooldownTimer >= AttackCooldown)
            {
                waitTimer += Time.deltaTime*2;
                if (waitTimer >= 1)
                {
                    CooldownTimer = 0;
                    CannonShoot();
                    SoundManager.instance.PlaySound(shootSound);
                    waitTimer = 0;
                    if (isRandom)
                    {
                        AttackCooldown = Random.Range(RanMin, RanMax);
                    }
                }
            }
        }
        else
        {
            waitTimer = 0;
        }
        if (Player.transform.position.x > transform.position.x)
        {
            side = -1;
            currentOffset = offsetRight;
            projectiles = projectilesRight;
        }
        else if(Player.transform.position.x < transform.position.x)
        {
            side = 1;
            currentOffset = offsetLeft;
            projectiles = projectilesLeft;
        }
    }
    private bool PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance *side,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, PlayerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * side,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void CannonShoot()
    {
        CooldownTimer = 0;
        projectiles[FindBullets()].transform.position = currentOffset.position;
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
