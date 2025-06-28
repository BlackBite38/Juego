using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform postActiveArea, preActiveArea;
    [SerializeField] GameObject moveObject;
    public bool activate;
    private SpriteRenderer spriteRend;
    [SerializeField] bool timed;
    [SerializeField] float timer, timerReset;


    // Start is called before the first frame update
    void Awake()
    {
        activate = false;
        moveObject.transform.position = preActiveArea.position;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            moveObject.transform.position = Vector2.MoveTowards(moveObject.transform.position, postActiveArea.position, speed * Time.deltaTime);
            if (timed == true)
            {
                spriteRend.color = Color.yellow;
            }
            else
            {
                spriteRend.color = Color.green;
            }
        }
        else
        {
            moveObject.transform.position = Vector2.MoveTowards(moveObject.transform.position, preActiveArea.position, speed * Time.deltaTime);
            spriteRend.color = Color.red;
        }
        if (moveObject.transform.position == postActiveArea.position && timed)
        {
                timer += Time.deltaTime;
        }
        if(timer > timerReset)
        {
            activate=false;
            timer = 0;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Bomb" || other.gameObject.tag == "BombCharged" || other.gameObject.tag == "Firework" || other.gameObject.tag == "FireworkCharged")
        {
            activate=true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bomb" || other.gameObject.tag == "BombCharged" || other.gameObject.tag == "Firework" || other.gameObject.tag == "FireworkCharged")
        {
            activate = true;
        }
    }
}
