using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorActivation : MonoBehaviour
{
    [SerializeField] GameObject NormalCamera, BossCamera;
    bool inside;
    [SerializeField] Transform up, middle, down;
    float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        speed = 1;
    }
    private void Update()
    {
        if (inside == true)
        {
            BossCamera.transform.position = Vector3.MoveTowards(BossCamera.transform.position, down.position, speed * Time.deltaTime);
            NormalCamera.SetActive(false);
            BossCamera.SetActive(true);
        }
        else
        {
            NormalCamera.SetActive(true);
            BossCamera.SetActive(false);
            BossCamera.transform.position = up.transform.position;
        }
        if (BossCamera.transform.position.y < middle.transform.position.y)
        {
            speed = 15;
        }
        else
        {
            speed = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inside = true;
        }
    }
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        inside = true;
    //    }
    //}
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inside = false;
        }   
    }
}
