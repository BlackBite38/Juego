using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    //[SerializeField] private Transform upperLimit, bottomLimit;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    //private bool movingUp;
    [SerializeField] private float idleTime;
    [SerializeField] private float waitTimer;

    public int startPoint;
    public Transform[] points;
    private int i;

    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        anim = enemy.GetComponent<Animator>();
        rb = enemy.GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        anim.SetBool("Flying", false);
    }
    // Update is called once per frame
    void Update()
    {
        //    if (movingUp)
        //    {
        //        if (enemy.position.y >= upperLimit.position.y)
        //        {
        //            MoveInDirection(-1);
        //        }
        //        else
        //        {
        //            DirectionChange();
        //        }
        //    }
        //    else
        //    {
        //        if (enemy.position.y <= bottomLimit.position.y)
        //        {
        //            MoveInDirection(1);
        //        }
        //        else
        //        {
        //            DirectionChange();
        //        }
        //    }

        if (enemy.GetComponent<BlackEnemy>().flying == true)
        {
            anim.SetBool("Flying", true);
            rb.gravityScale = 0;

            if (Vector2.Distance(enemy.transform.position, points[i].position) < 0.02f)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer > idleTime)
                {
                    i++;
                    if (i == points.Length)
                    {
                        i = 0;
                    }
                    waitTimer = 0;
                }
            }
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, points[i].position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Flying", false);
            rb.gravityScale = 1;
        }
    }
    //private void DirectionChange()
    //{
    //    waitTimer += Time.deltaTime;
    //    if (waitTimer > idleTime)
    //    {
    //        movingUp = !movingUp;
    //    }
    //}
    //private void MoveInDirection(int _direction)
    //{
    //    waitTimer = 0;
    //    //enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, enemy.localScale.y, enemy.localScale.z);
    //    enemy.position = new Vector3(enemy.position.x, enemy.position.y + Time.deltaTime * _direction * speed, enemy.position.z);
    //}
}
