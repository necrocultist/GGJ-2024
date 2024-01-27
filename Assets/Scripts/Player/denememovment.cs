using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class denememovment : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 8f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        
        rb.velocity = direction.normalized * moveSpeed;
        
    }
}
