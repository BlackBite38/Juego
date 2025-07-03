using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : MonoBehaviour
{
    [SerializeField] Transform introPoint;
    private Animator anim;
    private PlayerHp playerHealth;
    [SerializeField] float speed;
    [SerializeField] private GameObject player, spawn;
    [Header("Vida")]
    [SerializeField] float maxBossHealth, bossHealth;
    [Header("Ataque fisico")]
    [SerializeField] private float AttackCooldown;
    [SerializeField] private float meleeDamage;
    [SerializeField] private float CooldownTimer;
    [SerializeField] private float range, colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask PlayerLayer;
    [Header("Ataque magico")]
    [SerializeField] GameObject DarkSpell;
    [SerializeField] GameObject FireSpell;
    public Transform darkSpellArea1, darkSpellArea2, FireSpellAreaA, FireSpellAreaB;
    float place1, place2, place3, F_PlaceA, F_PlaceB, F_PlaceC;
    [Header("Caminar")]
    public Transform[] points;
    private int i;
    private Vector3 initScale;
    float _direction;
    [Header("Acciones")]
    [SerializeField] int state;
    [SerializeField] float actionTimer, actionTimer2;
    [SerializeField] float actionMoment, actionMoment2;
    [Header("Sounds")]
    [SerializeField] private AudioClip damageSound;
    [Header("Fases")]
    [SerializeField] bool phase2, phase3, alive;

    // Start is called before the first frame update
    private void Awake()
    {
        actionTimer = 0;
        actionTimer2 = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = maxBossHealth;
        anim = GetComponent<Animator>();
        introPoint.position = transform.position;
        initScale = transform.localScale;
        place1 = darkSpellArea1.position.x;
        place2 = darkSpellArea2.position.x;
        place3 = darkSpellArea1.position.y;
        F_PlaceA = FireSpellAreaA.position.x;
        F_PlaceB = FireSpellAreaB.position.x;
        F_PlaceC = FireSpellAreaA.position.y;
        actionMoment = 3;
        actionMoment2 = 3;
        alive = true;
    }
    // Update is called once per frame
    private void Update()
    {
        if (alive == true)
        {
            if(!phase2 && !phase3)
            {
                speed = 3;
                actionMoment = 3;
            }
            else if(phase2 == true && !phase3)
            {
                speed = 5;
                actionMoment = 2.5f;
            }
            else if (phase2 == true && phase3==true)
            {
                speed = 8;
                actionMoment = 2;
            }
            if (actionTimer > actionMoment)
            {
                state = Random.Range(0, 4);
                actionTimer = 0;
            }
            if (actionTimer2 > actionMoment2)
            {
                state = 0;
                actionTimer2 = 0;
            }
            if (CooldownTimer < AttackCooldown)
            {
                CooldownTimer += Time.deltaTime;
            }
            if (i > points.Length)
            {
                i = 0;
            }
            if (state == 0)
            {
                actionTimer += Time.deltaTime;
            }
            if (state == 1)
            {
                anim.SetBool("Walking", true);
                transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
                if (transform.position.x < points[i].position.x)
                {
                    _direction = 1;
                    transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
                }
                else if (transform.position.x > points[i].position.x)
                {
                    _direction = -1;
                    transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
                }
                if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
                {
                    i += Random.Range(0, points.Length);
                    state = Random.Range(0, 3);
                }
            }
            if (state != 1)
            {
                anim.SetBool("Walking", false);
                if (transform.position.x < player.transform.position.x)
                {
                    _direction = 1;
                    transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
                }
                else if (transform.position.x > player.transform.position.x)
                {
                    _direction = -1;
                    transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
                }
            }
            if (state == 2)
            {
                anim.SetTrigger("DarkSpell");
                actionTimer2 += Time.deltaTime;
            }
            if (state == 3)
            {
                anim.SetTrigger("FireSpell");
                actionTimer2 += Time.deltaTime;
            }
            if(state >= 4)
            {
                state = 0;
            }

            if (PlayerInRange())
            {
                if (CooldownTimer >= AttackCooldown)
                {
                    CooldownTimer = 0;
                    anim.SetTrigger("MeleeAttack");
                }
            }
            if (bossHealth < ((7 * maxBossHealth) / 10))
            {
                phase2 = true;
            }
            if (bossHealth < ((4 * maxBossHealth) / 10))
            {
                phase3 = true;
            }
        }
        if (player.GetComponent<PlayerHp>().CurrentHealth <= 0)
        {
            OnPlayerKO();
        }
        if (bossHealth <= 0)
        {
            anim.SetTrigger("Dead");
            if (gameObject.GetComponent<EnemyDamage>() != null) 
                gameObject.GetComponent<EnemyDamage>().enabled = false;
            alive = false;
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
        if (PlayerInRange())
        {
            playerHealth.TakeDamage(meleeDamage);
        }
    }
    private void DarkSpellAttack()
    {
        if (phase2 == false && phase2 == false)
        {
            float positionD = Random.Range(place1, place2);
            Vector3 spawnPositionD = new Vector3(positionD, place3, 0f);
            Instantiate(DarkSpell, spawnPositionD, transform.rotation);
        }
        else
        {
            float positionD1 = Random.Range(place1, place2);
            float positionD2 = Random.Range(place1, place2);
            Vector3 spawnPositionD1 = new Vector3(positionD1, place3, 0f);
            Vector3 spawnPositionD2 = new Vector3(positionD2, place3, 0f);
            Instantiate(DarkSpell, spawnPositionD1, transform.rotation);
            Instantiate(DarkSpell, spawnPositionD2, transform.rotation);
        }
    }
    private void FireSpellAttack()
    {
        if (phase2 == false && phase2==false)
        {
            float positionF = Random.Range(F_PlaceA, F_PlaceB);
            Vector3 spawnPositionF = new Vector3(positionF, F_PlaceC, 0f);
            Instantiate(FireSpell, spawnPositionF, transform.rotation);
        }
        else if(phase2==true && phase3==false)
        {
            float positionF1 = Random.Range(F_PlaceA, F_PlaceB);
            float positionF2 = Random.Range(F_PlaceA, F_PlaceB);
            Vector3 spawnPositionF1 = new Vector3(positionF1, F_PlaceC, 0f);
            Vector3 spawnPositionF2 = new Vector3(positionF2, F_PlaceC, 0f);
            Instantiate(FireSpell, spawnPositionF1, transform.rotation);
            Instantiate(FireSpell, spawnPositionF2, transform.rotation);
        }
        else if(phase2 == true && phase3 == true)
        {
            float positionF_1 = Random.Range(F_PlaceA, F_PlaceB);
            float positionF_2 = Random.Range(F_PlaceA, F_PlaceB);
            float positionF_3 = Random.Range(F_PlaceA, F_PlaceB);
            Vector3 spawnPositionF_1 = new Vector3(positionF_1, F_PlaceC, 0f);
            Vector3 spawnPositionF_2 = new Vector3(positionF_2, F_PlaceC, 0f);
            Vector3 spawnPositionF_3 = new Vector3(positionF_3, F_PlaceC, 0f);
            Instantiate(FireSpell, spawnPositionF_1, transform.rotation);
            Instantiate(FireSpell, spawnPositionF_2, transform.rotation);
            Instantiate(FireSpell, spawnPositionF_3, transform.rotation);
        }
    }
    public void TakeDamage(float _damage)
    {
        if (alive == true)
        {
            bossHealth = Mathf.Clamp(bossHealth - _damage, 0, maxBossHealth);
            if (damageSound != null)
            {
                SoundManager.instance.PlaySound(damageSound);
            }
            if (!PlayerInRange())
            {
                anim.SetTrigger("Hurt");
            }
        }
    }
    private void OnPlayerKO()
    {
        bossHealth = maxBossHealth;
        spawn.GetComponent<spawn>().Summoned = false;
        transform.position = introPoint.position;
        spawn.GetComponent<spawn>().Summoned = false;
        actionTimer = 0;
        actionTimer2 = 0;
        speed = 3;
        actionMoment = 3;
        alive = true;
        phase2 = false;
        phase3 = false;
        gameObject.SetActive(false);
    }
    private void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
