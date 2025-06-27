using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Test : MonoBehaviour
{
    Animator anim;
    [SerializeField] float speed;
    [SerializeField] int state;
    [SerializeField] float timeReset;
    [SerializeField] float timer;
    public Transform[] points;
    private int i;
    [SerializeField] GameObject fireball;
    public Transform area1, area2;
    float place1, place2, place3;
    float timer2;
    [SerializeField] bool lowHealth, phase2;
    float attackSpeed;
    [SerializeField] int increase, requiredIncrease;
    [SerializeField] float maxHealth, health;
    [SerializeField] GameObject DeathExplo, ExitDoor;

    [SerializeField] Transform introPoint;
    [SerializeField] EnemyHP HP;

    void Awake()
    {
        HP = GetComponent<EnemyHP>();
        //maxHealth = HP.GetComponent<EnemyHP>().MaxHealth;
        increase = 0;
        requiredIncrease = Random.Range(8, 12);
        state = 9;
        anim = GetComponent<Animator>();
        transform.position = introPoint.position;
        speed = 4;
        place1 = area1.position.x;
        place2 = area2.position.x;
        place3 = area1.position.y;
        lowHealth = false;
        gameObject.GetComponent<BoxCollider2D>().enabled =false;
    }

    // Update is called once per frame
    void Update()
    {
        //health= HP.GetComponent<EnemyHP>().Health;
        if (timer>=timeReset)
        {
            timer = 0;
            state = Random.Range(0, 7);
            speed = Random.Range(3, 7);
            anim.SetBool("Attacking", false);
            timer2 = 0;
        }
        if(state!=0 && state!=9)
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                i += Random.Range(0, points.Length);
                increase += 1;
                if(lowHealth==false)
                { 
                    speed = Random.Range(4, 7);
                }
                else
                {
                    speed = Random.Range(5, 8);
                }
                state = Random.Range(0, 7);
                if (i >= points.Length)
                {
                    i = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        else if(state==9)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[0].position, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, points[0].position) < 0.02f)
            {
                state = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else if (state==0)
        {
            Attack();
            timer+= Time.deltaTime;
            increase=0;
            requiredIncrease = Random.Range(8, 12);
        }
        if (increase >= requiredIncrease)
        {
            state = 0;
        }
        if (lowHealth==true)
        {
            attackSpeed = 7;
        }
        else
        {
            attackSpeed = 4;
        }
        if (HP.GetComponent<EnemyHP>().Health <= 0)
        {
            Instantiate(DeathExplo, gameObject.transform.position, transform.rotation);
            ExitDoor.SetActive(true);
            gameObject.SetActive(false);
        }
        if (phase2 == true)
        {
            if (HP.GetComponent<EnemyHP>().Health < ((4 * HP.GetComponent<EnemyHP>().MaxHealth) / 10))
            {
                lowHealth=true;
            }
        }
    }
    void Attack()
    {
        speed = 0;
        anim.SetBool("Attacking", true);
        timer2 += Time.deltaTime*attackSpeed;
        if (timer2 > 3 && lowHealth==false)
        {
            float position = Random.Range(place1, place2);
            Vector3 spawnPosition = new Vector3(position, place3, 0f);
            Instantiate(fireball, spawnPosition, transform.rotation);
            timer2 = 0;
        }
        else if (timer2 > 3 && lowHealth == true)
        {
            float position1 = Random.Range(place1, place2);
            float position2 = Random.Range(place1, place2);
            Vector3 spawnPosition1 = new Vector3(position1, place3, 0f);
            Vector3 spawnPosition2 = new Vector3(position2, place3, 0f);
            Instantiate(fireball, spawnPosition1, transform.rotation);
            Instantiate(fireball, spawnPosition2, transform.rotation);
            timer2 = 0;
        }
    }
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Firework")
    //    {
    //        Health -= 1;
    //    }
    //    else if (other.gameObject.tag == "FireworkCharged")
    //    {
    //        Health -= 2;
    //    }
    //    else if (other.gameObject.tag == "Bomb")
    //    {
    //        Health -= 2;
    //    }
    //    else if (other.gameObject.tag == "BombCharged")
    //    {
    //        Health -= 4;
    //    }
    //}
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Firework")
    //    {
    //        Health -= 1;
    //    }
    //    else if (other.gameObject.tag == "FireworkCharged")
    //    {
    //        Health -= 2;
    //    }
    //    else if (other.gameObject.tag == "Bomb")
    //    {
    //        Health -= 2;
    //    }
    //    else if (other.gameObject.tag == "BombCharged")
    //    {
    //        Health -= 4;
    //    }
    //}
}
