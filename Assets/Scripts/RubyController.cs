using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 5.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    int currentHealth;
    public int health { get { return currentHealth; } }
    bool isInvincible;
    float invincibleTimer;

    public GameObject projectilePrefab;

    public Camera cam;

    Vector2 mousePos;
    Vector2 mouseDir;
    Vector2 move;

    Rigidbody2D rigidbody2d;
    AudioSource audioSource;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    //dash test
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    bool dash;

    public GameObject dashEffect;

    //stab
    public float attackTime;
    public float startTimeAttack;

    public Transform sword;
    public float attackRange;
    public LayerMask enemies;

    private SpriteRenderer renderer;

    MeleeWeapon melee;

    Vector2 stabPos;
    Vector2 stabDir;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

        renderer = sword.GetComponent<SpriteRenderer>();

        melee = sword.GetComponent<MeleeWeapon>();
        sword.GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void Update()
    {
        mouseDir = (mousePos - rigidbody2d.position).normalized;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        move = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && !dash && move != Vector2.zero)
        {
            dash = true;
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if (Input.GetButton("Fire2") && attackTime <= 0)
        {
            if (!Mathf.Approximately(mouseDir.x, 0.0f) || !Mathf.Approximately(mouseDir.y, 0.0f))
            {
                lookDirection.Set(mouseDir.x, mouseDir.y);
                lookDirection.Normalize();
            }

            attackTime = startTimeAttack;
            animator.SetTrigger("Launch");

            stabPos = mousePos;
            stabDir = mouseDir;
        }
        Stab();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            //lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;

        position = position + move.normalized * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        //rigidbody2d.velocity = move.normalized * speed;
        //rigidbody2d.AddForce(move.normalized * speed);

       

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        /*if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }*/

        #region dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector2 position2 = rigidbody2d.position;
            position2 = position2 + lookDirection * dashSpeed * Time.deltaTime;
            rigidbody2d.MovePosition(position2);
        }
        #endregion


        #region test
        //test
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        if (dash)
        {
            Dash();
        }

        #endregion
        
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            animator.SetTrigger("Hit");

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        #region test
        //test attack by mouse
        if (!Mathf.Approximately(mouseDir.x, 0.0f) || !Mathf.Approximately(mouseDir.y, 0.0f))
        {
            lookDirection.Set(mouseDir.x, mouseDir.y);
            lookDirection.Normalize();
        }
        #endregion

        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

    void Dash()
    {
        dashTime -= Time.deltaTime;
        rigidbody2d.AddForce(move.normalized * dashSpeed);
        //rigidbody2d.velocity = move.normalized * dashSpeed;
        if (dashTime < 0)
        {
            dashTime = startDashTime;
            dash = false;
        }

    }

    void Stab()
    {
        Vector2 pos = transform.position;
        Vector2 up = transform.up;

        float angle = Vector2.Angle(up, mouseDir);

        if (pos.x > mousePos.x)
            angle = 360 - angle;

        float rad = Mathf.Deg2Rad * angle;

        Vector2 offset = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));

        if (attackTime > 0)
        {
            angle = Vector2.Angle(up, stabDir);

            if (pos.x > stabPos.x)
                angle = 360 - angle;

            rad = Mathf.Deg2Rad * angle;

            offset = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
            renderer.enabled = true;
            sword.GetComponent<PolygonCollider2D>().enabled = true;
            sword.position = pos + offset - stabDir * Mathf.Pow(attackTime / startTimeAttack, 2);

            attackTime -= Time.deltaTime;
        }
        else
        {
            sword.GetComponent<PolygonCollider2D>().enabled = false;

            renderer.enabled = false;
            sword.position = pos + offset + new Vector2(0, 0.5f);
            sword.rotation = Quaternion.Euler(0, 0, 45 - angle);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
