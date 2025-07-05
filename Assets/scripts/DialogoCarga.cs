using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoCarga : MonoBehaviour
{
    [SerializeField] GameObject dialogo1, dialogo2;
    [SerializeField] GameObject Player;
    [SerializeField] private AudioClip unlockSound;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Player.GetComponent<PlayerMove>().chargeUnlocked = true;
        SoundManager.instance.PlaySound(unlockSound);
        dialogo1.SetActive(false);
        dialogo2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
