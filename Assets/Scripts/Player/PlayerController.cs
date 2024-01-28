using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private Animator animator;
    private float moveSpeed;
    private bool isPlayerMovementDisabled = false;
    private static readonly int Attack = Animator.StringToHash("attack");

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
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

        HandleAttackInput();
    }

    private void HandleMovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

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

        if (horizontalMovement > 0)
        {
            player.spriteRenderer.flipX = true;
        }
        else
        {
            player.spriteRenderer.flipX = false;
        }

        if (verticalMovement > 0)
        {
            animator.SetBool("FacingUp", true);
        }
        else
        {
            animator.SetBool("FacingUp", false);
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            player.attackEvent.CallAttackEvent(player.playerDetails.playerDamageAmount);

            int randomAttack = Random.Range(1, 3);
            string attackTrigger = (randomAttack == 1) ? "Attack1" : "Attack2";

            player.animator.SetTrigger(attackTrigger);
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