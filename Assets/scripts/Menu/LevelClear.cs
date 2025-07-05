using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClear : MonoBehaviour
{
    public int level;
    float timer;

    void Awake()
    {
        timer = 0;
    }
    void Update()
    {
        if (level == 2)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                SceneManager.LoadScene("Level 3");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.X))
        {
            if (level == 0)
            {
                SceneManager.LoadScene("Menu");
            }
            if(level==1)
            {
                SceneManager.LoadScene("Level 2");
            }
        }
    }
    public void EndScreen()
    {
        SceneManager.LoadScene("FinDelJuego");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
