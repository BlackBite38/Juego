using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorActivation : MonoBehaviour
{
    [SerializeField] GameObject NormalCamera, BossCamera;
    bool inside;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && inside==false)
        {
            NormalCamera.SetActive(false);
            BossCamera.SetActive(true);
            inside = true;
        }
            
    }
}
