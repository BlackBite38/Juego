using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : MonoBehaviour
{
    [SerializeField] private float AttackCooldown;
    [SerializeField] private float meleeDamage;
    [SerializeField] private float CooldownTimer;
    [SerializeField] private float range, colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask PlayerLayer;
    private Animator anim;
    private PlayerHp playerHealth;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        CooldownTimer += Time.deltaTime;
        if (PlayerInRange())
        {
            if (CooldownTimer >= AttackCooldown)
            {
                CooldownTimer = 0;
                anim.SetTrigger("MeleeAttack");
            }
        }
        if (CooldownTimer >= AttackCooldown + 3)
        {
            CooldownTimer = 0;
            SpellAttack();
        }
    }
    private bool PlayerInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, PlayerLayer);
        
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHp>();
        }

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayerMelee()
    {
        if(PlayerInRange())
        {
            playerHealth.TakeDamage(meleeDamage);
        }
    }
    private void SpellAttack()
    {
        return;
    }
}
