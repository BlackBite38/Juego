using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private float Healing;
    [SerializeField] private bool stageMade;
    [SerializeField] private float Timer, Timer2, lifeTime;
    private bool startTime, confirmation;
    [SerializeField] private float TframesDuration;
    [SerializeField] private int TframesAmount;
    private SpriteRenderer spriteRend;
    [Header("Sounds")]
    [SerializeField] private AudioClip healSound;

    void Awake()
    {
        startTime=false;
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(stageMade==false)
        {
            if(Timer<=lifeTime)
                Timer += Time.deltaTime;
            else
            {
                startTime = true;
            }
        }
        if(startTime==true)
        {
            if (confirmation == false)
            {
                StartCoroutine(transparency());
                confirmation = true;
            }
            Timer2 += Time.deltaTime;
        }
        if (Timer2 > 3)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHp>().AddHP(Healing);
            SoundManager.instance.PlaySound(healSound);
            if(stageMade)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator transparency()
    {   //hace al personaje parpadear al recibir daño
        for (int i = 0; i < TframesDuration; i++)
        {
            yield return new WaitForSeconds(TframesDuration / (TframesAmount * 3));
            spriteRend.color = new Color(135, 135, 135, 0.5f);
            yield return new WaitForSeconds(TframesDuration / (TframesAmount * 3));
            spriteRend.color = Color.white;
            //yield return new WaitForSeconds(IframesDuration /(IframesAmount*2));
        }
    }
}
