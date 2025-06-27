using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] public float MaxHealth, Health, Defense;
    [SerializeField] GameObject DeathExplo;
    [SerializeField] Transform ExploPoint;
    [SerializeField] bool isBoss, isBoss1;
    [SerializeField] GameObject defeatEvent;

    // Start is called before the first frame update
    void Awake()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            if (!isBoss1)
            {
                Instantiate(DeathExplo, ExploPoint.position, transform.rotation);
                gameObject.SetActive(false);
            }
            if (isBoss==true)
            {
                defeatEvent.SetActive(true);
            }
        }
    }
    public void TakeDamage(float _damage)
    {
        if (Defense == 0)
        {
            Health = Mathf.Clamp(Health - _damage, 0, MaxHealth);
        }
        else
        {
            float new_damage = _damage - Defense;
            if (new_damage < 0)
            { 
                new_damage = 0;
            }
            Health = Mathf.Clamp(Health - new_damage, 0, MaxHealth);
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
