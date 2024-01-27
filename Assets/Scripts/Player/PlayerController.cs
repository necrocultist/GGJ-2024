using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private float moveSpeed;
    private bool isPlayerMovementDisabled = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        moveSpeed = player.playerDetails.GetMoveSpeed();
    }
    
    private void SetPlayerAnimationSpeed()
    {
        // Set animator speed to match movement speed
        player.animator.speed = moveSpeed / PlayerDetailsSO.animationBaseSpeed;
    }
    
    private void Update()
    {
        if (isPlayerMovementDisabled)
            return;

        HandleMovementInput();
    }
    
    private void HandleMovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);
        
        Debug.Log(direction);

        if (direction != Vector2.zero)
        {
            player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
            // TODO: create anim script
            // player.animator.SetBool("isWalking", true);
            // player.animator.SetFloat("inputX", direction.x);
            // player.animator.SetFloat("inputY", direction.y);
        }
        else
        {
            player.idleEvent.CallIdleEvent();
            // player.animator.SetBool("isWalking", false);
        }
    }
    
    public void EnablePlayer()
    {
        isPlayerMovementDisabled = false;
    }

    public void DisablePlayer()
    {
        isPlayerMovementDisabled = true;
        player.idleEvent.CallIdleEvent();
    }
}
