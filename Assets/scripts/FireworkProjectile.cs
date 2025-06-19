using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private float direction;
    [SerializeField] GameObject explosion;
    private float timer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            Deactivate();
        }
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("end");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        return;
    }
    public void SetDirection(float _direction)
    {
        timer = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float LocalScaleX = transform.localScale.x;
        if (Mathf.Sign(LocalScaleX) != _direction)
        {
            LocalScaleX = -LocalScaleX;
        }
        transform.localScale = new Vector3(LocalScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        Instantiate(explosion, gameObject.transform.position, transform.rotation);
        gameObject.SetActive(false);
        timer = 0;
    }
}
