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

        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f, -0.2f), Quaternion.identity) as GameObject;
            Vector2 direction = new Vector2(transform.localScale.x, 0);
            throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction;
            throwableWeapon.name = "ThrowableWeapon";
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
        Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
        for (int i = 0; i < collidersEnemies.Length; i++)
        {
            if (collidersEnemies[i].gameObject.tag == "Enemy")
            {
                if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
                {
                    dmgValue = -dmgValue;
                }
                collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
                cam.GetComponent<CameraFollow>().ShakeCamera();
            }
        }
    }
}
