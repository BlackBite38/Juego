using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float speed, power;
    [SerializeField] private float resetTime;
    private float timer, timer3;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    private void OnDisable()
    {
        timer3 = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer3 < 1)
        {
            timer3 += Time.deltaTime;
            transform.position += transform.up * Time.deltaTime * power;
        }
        //transform.position += transform.right * Time.deltaTime * speed;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        timer += Time.deltaTime;
        if (timer > resetTime)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Enemy" || other.gameObject.tag != "EnemyProjectile")
        {
            explode();
        }
    }
    private void explode()
    {
        Instantiate(explosion, gameObject.transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
    public void ActivateProjectile()
    {
        timer = 0;
        gameObject.SetActive(true);
    }
}
