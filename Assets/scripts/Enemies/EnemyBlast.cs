using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlast : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float timer;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        timer += Time.deltaTime;
        if (timer > resetTime)
        {
            Instantiate(explosion, gameObject.transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
    public void ActivateProjectile() //SetDirection(float _direction)
    {
        timer = 0;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" || collision.gameObject.tag != "EnemyProjectile")
        {
            Instantiate(explosion, gameObject.transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
