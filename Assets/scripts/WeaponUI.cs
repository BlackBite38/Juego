using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;
    [SerializeField] Image Bomb, BombCharge, Firework, FireworkCharge, currentWeapon, currentWeaponCharge;

    // Start is called before the first frame update
    void Awake()
    {
        currentWeapon.sprite = Bomb.sprite;
        currentWeaponCharge.sprite = BombCharge.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        currentWeaponCharge.fillAmount = playerMove.chargeAmount;
        if (playerMove.weaponType==0)
        {
            currentWeapon.sprite = Bomb.sprite;
            currentWeaponCharge.sprite = BombCharge.sprite;
        }
        else if(playerMove.weaponType==1)
        {
            currentWeapon.sprite = Firework.sprite;
            currentWeaponCharge.sprite = FireworkCharge.sprite;
        }
    }
}
