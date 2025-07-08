using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomJumps : MonoBehaviour
{
    [SerializeField] float timer, jumpMoment;
    private Rigidbody2D rb;
    [SerializeField] float speed, power;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpMoment = Random.Range(3, 8);
    }

    // Update is called once per frame
    void Update()
    {
        var force = Vector3.up;
        timer += Time.deltaTime;
        if (timer > jumpMoment)
        {
            GetComponent<Rigidbody2D>().AddForce(force * speed * power, ForceMode2D.Impulse);
            jumpMoment = Random.Range(3, 7);
            timer = 0;
        }
    }
}
