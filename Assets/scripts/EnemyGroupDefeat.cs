using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupDefeat : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject evento;
    int numero;
    bool hecho;
    // Start is called before the first frame update
    void Awake()
    {
        numero = 0;
        hecho = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(enemies.Length==0)
        //{
        //    evento.SetActive(true);
        //}
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    if (i > enemies.Length)
        //    {
        //        i = 0;
        //    }
        //    if (enemies[i].activeInHierarchy)
        //    {
        //        break;
        //    }
        //    print("a");
        //}

        foreach (GameObject i in enemies)
        {
            if (i.activeInHierarchy)
            {
                numero = 0;
                break;
            }
            else
            {
                numero++;
            }
            if (numero > enemies.Length && hecho==false)
            {
                evento.SetActive(true);
                hecho = true;
            }
        }
    }
}
