using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkShooter : MonoBehaviour
{
    private PlayerMove playerControler;
    public float cooldown;
    Animator anim;
    [SerializeField] Transform Offset;
    [SerializeField] private GameObject[] FireworksAmmo, ChargedFireworksAmmo;
    [Header("Sounds")]
    [SerializeField] private AudioClip throwFirework;

    private void Awake()
    {
        playerControler = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
        Offset = playerControler.GetComponent<PlayerMove>().FireworkOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        cooldown= playerControler.GetComponent<PlayerMove>().AttackCooldown;

        if (Input.GetKeyUp(KeyCode.Z) && playerControler.GetComponent<PlayerMove>().AttackCooldown == 0)
        {
            if (playerControler.GetComponent<PlayerMove>().weaponType == 1 && playerControler.GetComponent<PlayerMove>().canShoot==0)
            {
                if (playerControler.GetComponent<PlayerMove>().chargeAmount < 1)
                {
                    Attack();
                    playerControler.GetComponent<PlayerMove>().chargeAmount = 0;
                }
                else
                {
                    AttackCharged();
                    playerControler.GetComponent<PlayerMove>().chargeAmount = 0;
                }
            }
        }
    }
    void Attack()
    {
        anim.SetTrigger("FireThr");
        playerControler.GetComponent<PlayerMove>().AttackCooldown += 1;

        FireworksAmmo[FindFirework()].transform.position = Offset.position;
        FireworksAmmo[FindFirework()].GetComponent<FireworkProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    void AttackCharged()
    {
        anim.SetTrigger("FireThr");
        playerControler.GetComponent<PlayerMove>().AttackCooldown += 1;

        ChargedFireworksAmmo[FindChargedFirework()].transform.position = Offset.position;
        ChargedFireworksAmmo[FindChargedFirework()].GetComponent<FireworkProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFirework()
    {
        for(int i =0; i<FireworksAmmo.Length; i++)
        {
            if (!FireworksAmmo[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private int FindChargedFirework()
    {
        for (int i = 0; i < ChargedFireworksAmmo.Length; i++)
        {
            if (!ChargedFireworksAmmo[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
