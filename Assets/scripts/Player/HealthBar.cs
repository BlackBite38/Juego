using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHp playerHealth;
    [SerializeField] Image TotalHealthBar, CurrentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        TotalHealthBar.fillAmount = playerHealth.MaxHealth / 20;
    }

    // Update is called once per frame
    void Update()
    {
        TotalHealthBar.fillAmount = playerHealth.MaxHealth / 20;
        CurrentHealthBar.fillAmount = playerHealth.CurrentHealth / 20;
    }
}
