using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public CharacterController2D controller;
    public float dmgValue = 1;
    public GameObject throwableObject;
    public Transform attackCheck;
    private Rigidbody2D m_Rigidbody2D;
    public Animator animator;
    public bool canAttack = true;
    public bool canDash = true;
    public bool isTimeToCheck = false;

    public GameObject cam;
    public float dashSpeed = 80f; // kecepatan dash
    public float dashTime = 0.1f; // durasi dash
    public float dashCooldown = 2f; // cooldown dash

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack) // jika klik kiri
        {
            canAttack = false;
            animator.SetBool("IsAttacking", true);

            StartCoroutine(AttackCooldown());

            // Menyesuaikan rotasi karakter agar menghadap ke arah mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;

            if (direction.x < 0)
            {
                if (controller.m_FacingRight) controller.Flip();
            }
            else
            {
                // Jika arah ke kanan, biarkan seperti semula
                if (!controller.m_FacingRight) controller.Flip();
            }
        }

        if (Input.GetMouseButtonDown(0) && canDash) // jika klik kiri dan bisa dash
        {
            canDash = false;
            animator.SetBool("IsAttacking", true);
            StartCoroutine(DashTowardsMouse());
            StartCoroutine(DashCooldown());
            StartCoroutine(AttackCooldown());

            // Menyesuaikan rotasi karakter agar menghadap ke arah mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;

            if (direction.x < 0)
            {
                if (controller.m_FacingRight) controller.Flip();
            }
            else
            {
                // Jika arah ke kanan, biarkan seperti semula
                if (!controller.m_FacingRight) controller.Flip();
            }
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
        canDash = true; // bisa dash lagi
    }

    IEnumerator DashTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            m_Rigidbody2D.velocity = direction * dashSpeed;
            yield return null;
        }

        m_Rigidbody2D.velocity = Vector2.zero;
        DoDashDamage();
    }

    public void DoDashDamage()
    {
        dmgValue = Mathf.Abs(dmgValue);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Enemy") || colliders[i].gameObject.CompareTag("Destructible"))
            {
                if (colliders[i].transform.position.x - transform.position.x < 0)
                {
                    dmgValue = -dmgValue;
                }
                colliders[i].gameObject.SendMessage("ApplyDamage", dmgValue);
                cam.GetComponent<CameraFollow>().ShakeCamera();
            }
        }
    }
}
