using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool moveLeft;
    void OnTriggerStay2D(Collider2D other)
    {
        Vector3 movement = (moveLeft ? Vector3.left : Vector3.right) * moveSpeed * Time.deltaTime;
        other.transform.Translate(movement);
    }
}
