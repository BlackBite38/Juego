using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoNPC : MonoBehaviour
{
    [SerializeField] GameObject NPC1, NPC2;
    // Start is called before the first frame update
    void Awake()
    {
        NPC1.SetActive(false);
        NPC2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
