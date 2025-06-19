using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileTest : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float direction;
    [SerializeField] GameObject explosion;
    private float timer;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5)
        {
            Instantiate(explosion, gameObject.transform.position, transform.rotation);
            gameObject.SetActive(false);
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
        Instantiate(explosion, gameObject.transform.position, transform.rotation);
    }
    public void SetDirection(float _direction)
    {
        timer = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit=false;
        boxCollider.enabled=true;
       
        float LocalScaleX = transform.localScale.x;
        if(Mathf.Sign(LocalScaleX)!=_direction)
        {
            LocalScaleX=-LocalScaleX;
        }
        transform.localScale = new Vector3(LocalScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
