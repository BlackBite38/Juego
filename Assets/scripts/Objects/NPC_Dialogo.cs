using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogo : MonoBehaviour
{
    [SerializeField] GameObject bubble, evento;
    [SerializeField] bool unactive = true;
    [SerializeField] float timer;


    // Update is called once per frame
    void Update()
    {
        if (unactive == false)
        {
            timer += Time.deltaTime;
        }
        if (timer > 3)
        {
            bubble.SetActive(false);
            unactive = true;
            timer = 0;
            if(evento != null)
            {
                evento.SetActive(true);
            }
        }
        if(bubble.activeInHierarchy)
        {
            unactive = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.X))
        {
            bubble.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.X))
        {
            bubble.SetActive(true);
            unactive = false;
        }
    }
}