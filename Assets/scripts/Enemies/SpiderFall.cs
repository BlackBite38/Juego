using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFall : MonoBehaviour
{
    [SerializeField] GameObject spider, spiderBox, spiderArea;
    // Start is called before the first frame update
    void Awake()
    {
        spider.GetComponent<Animator>().SetBool("Awoken", true);
        spider.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        spiderBox.transform.right = new Vector3(0, 0, 0);
        spiderBox.transform.position -= new Vector3(0, 2, 0);
        spider.GetComponent<randomJumps>().enabled=true;
        spider.GetComponent<EnemyHP>().enabled = true;
        spiderArea.GetComponent<EnemyPatroll>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
