using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    public float speed = 4f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*speed;
        Destroy(gameObject, 10f);
    }

    void OnCollisionEnter2D(){
        Destroy(gameObject);
    }
}
