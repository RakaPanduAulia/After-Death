using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    public bool isRewinding = false;
    [SerializeField] private List<CharacterController2D.PlayerState> states = new List<CharacterController2D.PlayerState>();
    private CharacterController2D controller;

    private void Awake()
    {
        controller = FindObjectOfType<CharacterController2D>();
    }

    private void Update()
{
    if (controller.life <= 0 && Input.GetKeyDown(KeyCode.Space))
    {
        StartRewind();
    }
    // When no more states are left, stop rewinding
    if (isRewinding && states.Count == 0)
    {
        StopRewind();
    }
}

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        controller.invincible = true;
        controller.animator.SetBool("IsDead", false);
        controller.animator.ResetTrigger("Hit");
        controller.m_Rigidbody2D.velocity = Vector2.zero;
        // If states exist, set the player to the first recorded state, which is the starting state.
        if (states.Count > 0)
        {
            CharacterController2D.PlayerState firstState = states[states.Count - 1];
            controller.transform.position = firstState.position;
            // ... Set any other necessary properties here
        }
    }

    private void Rewind()
{
    if (states.Count > 0)
    {
        RewindToState(states[0]);
        states.RemoveAt(0);
        controller.invincible = true;
    }
}

    private void RewindToState(CharacterController2D.PlayerState state)
    {
        controller.transform.position = state.position;
        controller.animator.SetBool("IsJumping", state.IsJumping);
        controller.animator.SetBool("IsDashing", state.isDashing);
        controller.animator.SetBool("IsAttacking", state.isAttacking);
    
        if (state.facingRight != controller.m_FacingRight)
        {
            controller.Flip();
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
        controller.invincible = false;
        controller.canMove = true;
        controller.GetComponent<Attack>().enabled = true;
    }

    private void Record()
    {
        if (states.Count > Mathf.Round(60f / Time.fixedDeltaTime))
        {
            states.RemoveAt(states.Count - 1);
        }

        states.Insert(0, new CharacterController2D.PlayerState
        {
            position = controller.transform.position,
            IsJumping = controller.animator.GetBool("IsJumping"),
            isDashing = controller.isDashing,
            isAttacking = controller.animator.GetBool("IsAttacking"),
            facingRight = controller.m_FacingRight,
        });
    }
}
