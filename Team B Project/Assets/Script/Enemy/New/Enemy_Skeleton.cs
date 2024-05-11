using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("Move info")]
    [SerializeField] private float moveSpeed;

    [Header("Player Detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Enemy Settings")]
    [SerializeField] private float life = 1f;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("Enemy State")]
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isDead;
    [SerializeField] private bool isInvincible;

    private RaycastHit2D isPlayerDetected;
    private float attackTimer = 0f;

    protected override void Start()
    {
        base.Start();

        isInvincible = false;
    }

    protected override void Update()
    {
        base.Update();

        ChasePlayer();

        WallCheck();

        Movement();

        AnimatorControllers();

        attackTimer += Time.deltaTime;

    }

    private void ChasePlayer()
    {
        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1 && !isAttacking)
            {
                rb.velocity = new Vector2(moveSpeed * 1.5f * facingDir, rb.velocity.y);
                isAttacking = false;

                Debug.Log("I see player");
            }

            else
            {
                StartAttackEvent();
            }
        }

        else
        {
            return;
        }
    }

    private void StartAttackEvent()
    {
        isAttacking = true;
    }

    public void AttackOver()
    {
        isAttacking = false;
    }

    private void Movement()
    {
        if (!isAttacking && !isPlayerDetected)
        {
            rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
        }

        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }

    private void WallCheck()
    {
        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("isDead", isDead);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }
}
