using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float MaxHealth;
    public float CurrentHealth { get; private set; }
    [SerializeField] bool dead;
    [Header("Iframes")]
    [SerializeField] private float InvincibilityTimer;
    [SerializeField] private float IframesDuration;
    [SerializeField] private int IframesAmount;
    private SpriteRenderer spriteRend;
    [SerializeField] bool Invincible;

    [SerializeField] GameObject KO_anim;
    bool deadAnim;
    [Header("Sounds")]
    [SerializeField] private AudioClip hurt, KO;

    // Start is called before the first frame update
    void Awake()
    {
        CurrentHealth=MaxHealth;
        InvincibilityTimer = 0;
        Invincible = false;
        dead = false;
        deadAnim = false;
        spriteRend = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Invincible == true)
        {
            InvincibilityTimer -= Time.deltaTime;
        }
        if (InvincibilityTimer <= 0)
        {
            Invincible = false;
            InvincibilityTimer = 0;
        }
        if(CurrentHealth>0)
        {
            dead = false;
            GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            KO_anim.transform.position = transform.position;
            KO_anim.SetActive(false);
            deadAnim = false;
        }
        if(Input.GetKeyUp(KeyCode.V))
        {
            Respawn();
        }
    }
    public void TakeDamage(float _damage)
    {
        if(!Invincible && CurrentHealth>0)
        {   //resta el daño a la vida
            CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, MaxHealth);
            SoundManager.instance.PlaySound(hurt);
        }
        //frames de invencibilidad
        if (CurrentHealth > 0 && !Invincible)
        {
            StartCoroutine(Invulnerability());
            Invincible = true;
            InvincibilityTimer += IframesDuration;
        }
        else
        {   
            if (!dead)
            {
                GetComponent<PlayerMove>().enabled = false;
                dead = true;
            }
        }
        if(CurrentHealth<=0 && deadAnim==false)
        {
            SoundManager.instance.PlaySound(KO);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            KO_anim.SetActive(true);
            deadAnim=true;
        }
    }
    public void AddHP(float _heal)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + _heal, 0, MaxHealth);
    }
    public void Respawn()
    {
        CurrentHealth=MaxHealth;
    }

    private IEnumerator Invulnerability()
    {   //hace al personaje parpadear al recibir daño
        for (int i =0; i<IframesAmount; i++)
        {
            yield return new WaitForSeconds(IframesDuration / (IframesAmount * 2));
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IframesDuration/(IframesAmount*2));
            spriteRend.color = Color.white;
            //yield return new WaitForSeconds(IframesDuration /(IframesAmount*2));
        }
    }
}
