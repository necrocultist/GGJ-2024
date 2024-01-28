using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left,right,up,down
    }

    public DoorType doorType;
    private GameObject player;
    private float offset = 1.75f;
    public GameObject doorCollider;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (doorType)
        {
            case DoorType.down:
                player.transform.position = new Vector2(transform.position.x, transform.position.y - offset);
                break;
            case DoorType.left:
                player.transform.position = new Vector2(transform.position.x - offset, transform.position.y);
                break;
            case DoorType.right:
                player.transform.position = new Vector2(transform.position.x + offset, transform.position.y);
                break;
            case DoorType.up:
                player.transform.position = new Vector2(transform.position.x, transform.position.y + offset);
                break;
        }
    }
}
