using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] EnemyHP HP;
    [SerializeField] GameObject spawn, explos, player; 
    [SerializeField] GameObject[] Weapons;
    bool bossIntroDone;
    float exploTimer;
    public Transform area1, area2;
    float place1, place2, place3, place4;

    void Awake()
    {
        HP = GetComponent<EnemyHP>();
        NoActiveWeapons();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        exploTimer = 0;
        place1 = area1.position.x;
        place2 = area1.position.y;
        place3 = area2.position.x;
        place4 = area2.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerHp>().CurrentHealth <= 0)
        {
            OnPlayerKO();
        }

        if (bossIntroDone == true)
        {
            activateWeapons();
        }

        if (HP.Health <= 0)
        {
            NoActiveWeapons();
            exploTimer += Time.deltaTime;
            if (exploTimer > 0.1f)
            {
                Instantiate(explos, new Vector3((Random.Range(place1, place3)), (Random.Range(place2, place4))), transform.rotation);
                exploTimer = 0;
            }
            spawn.GetComponent<spawn>().bossDead = true;
        }
    }
    public void IntroDone()
    {
        bossIntroDone = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    void activateWeapons()
    {
        foreach (GameObject i in Weapons)
        {
            if (i.GetComponent<EnemyPatroll>() != null)
            {
                i.GetComponent<EnemyPatroll>().enabled = true;
            }
            if (i.GetComponent<Cannon>() != null)
            {
                i.GetComponent<Cannon>().enabled = true;
            }
            if (i.GetComponent<MovingPlataforms>() != null)
            {
                i.GetComponent<MovingPlataforms>().enabled = true;
            }
        }
    }
    void NoActiveWeapons()
    {
        foreach(GameObject i in Weapons)
        {
            if(i.GetComponent<EnemyPatroll>() != null)
            {
                i.GetComponent<EnemyPatroll>().enabled=false;
            }
            if (i.GetComponent<Cannon>() != null)
            {
                i.GetComponent<Cannon>().enabled = false;
            }
            if (i.GetComponent<MovingPlataforms>() != null)
            {
                i.GetComponent<MovingPlataforms>().enabled = false;
            }
        }
    }
    private void OnPlayerKO()
    {
        HP.GetComponent<EnemyHP>().Health = HP.GetComponent<EnemyHP>().MaxHealth;
        spawn.GetComponent<spawn>().Summoned = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        bossIntroDone=false;
        NoActiveWeapons();
        gameObject.SetActive(false);
    }
}
