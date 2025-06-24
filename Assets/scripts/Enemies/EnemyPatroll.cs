using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    [SerializeField] private Transform leftLimit, rightLimit;

    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private float idleTime;
    private float waitTimer;
    //
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        initScale=enemy.localScale;
        anim = enemy.GetComponent<Animator>();
        //Monster = enemy.GetComponent<BlackEnemy>();
    }
    private void OnDisable()
    {
        anim.SetBool("Walking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x>=leftLimit.position.x)
            {
                MoveInDirection(-1); 
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightLimit.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }
    private void DirectionChange()
    {
        anim.SetBool("Walking", false);
        waitTimer += Time.deltaTime;
        if(waitTimer > idleTime)
        {
            movingLeft = !movingLeft;
        }
    }
    private void MoveInDirection(int _direction)
    {
        waitTimer = 0;
        if (enemy.GetComponent<BlackEnemy>() != null)
        {
            if (enemy.GetComponent<BlackEnemy>().flying == false)
            { 
                anim.SetBool("Walking", true);
            }
        }
        else
        {
            anim.SetBool("Walking", true);
        }
        enemy.localScale=new Vector3(Mathf.Abs(initScale.x) *_direction, enemy.localScale.y, enemy.localScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
