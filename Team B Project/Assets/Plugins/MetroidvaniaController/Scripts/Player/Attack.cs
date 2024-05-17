using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public CharacterController2D controller;
    public float dmgValue = 1;
    public Transform attackCheck;
    private Rigidbody2D m_Rigidbody2D;
    public Animator animator;
    public bool canAttack = true;
    public bool canDash = true;
    public GameObject cam;
    public float dashSpeed = 80f;
    public float dashTime = 0.1f;
    public float dashCooldown = 2f;
    public float dashWaitTime = 1f; // Time to wait after dash before re-enabling controls
    private bool originalFacingRight;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canDash) // Ensure the player can only attack when the dash is not on cooldown
        {
            canDash = false;
            canAttack = false;
            animator.SetBool("IsAttacking", true);

            // Save the original facing direction
            originalFacingRight = controller.m_FacingRight;

            StartCoroutine(DashTowardsMouse());
            StartCoroutine(DashCooldown());
            AdjustFacingDirection();
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        canAttack = true;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        canAttack = true; // Allow attacking again only when dash cooldown is complete
    }

    IEnumerator DashTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float startTime = Time.time;

        // Adjust facing direction for the dash
        if (direction.x < 0 && controller.m_FacingRight)
        {
            controller.Flip();
        }
        else if (direction.x > 0 && !controller.m_FacingRight)
        {
            controller.Flip();
        }

        while (Time.time < startTime + dashTime)
        {
            m_Rigidbody2D.velocity = direction * dashSpeed;
            yield return null;
        }

        m_Rigidbody2D.velocity = Vector2.zero;
        DoDashDamage();

        // Restore the original facing direction after the dash
        if (originalFacingRight != controller.m_FacingRight)
        {
            controller.Flip();
        }

        // Wait for the specified duration after the dash
        yield return new WaitForSeconds(dashWaitTime);
    }

    private void AdjustFacingDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        if (direction.x < 0)
        {
            if (controller.m_FacingRight) controller.Flip();
        }
        else
        {
            if (!controller.m_FacingRight) controller.Flip();
        }
    }

    public void DoDashDamage()
    {
        // Ensure dmgValue is negative
        dmgValue = -Mathf.Abs(dmgValue);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, 1.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("Destructible"))
            {
                Debug.Log($"Applying {dmgValue} damage to {collider.gameObject.name}");
                collider.SendMessage("ApplyDamage", dmgValue, SendMessageOptions.DontRequireReceiver);
                cam.GetComponent<CameraFollow>().ShakeCamera();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackCheck.position, 1.5f);
        }
    }
}