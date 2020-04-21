using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 5.0f;
    public float dashSpeed = 75f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    int currentHealth;
    public int health { get { return currentHealth; } }
    bool isInvincible;
    float invincibleTimer;

    public GameObject projectilePrefab;

    public Camera cam;

    Vector2 mousePos;

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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical).normalized;

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
            Vector2 right = transform.right;

            position2 = position2 + lookDirection * dashSpeed * Time.deltaTime;
            rigidbody2d.MovePosition(position2);
        }
        #endregion

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

        #region test
        //test
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !dash && move != Vector2.zero)
        {
            dash = true;
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }

        if (dash)
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
        Vector2 mouseDir = (mousePos - rigidbody2d.position).normalized;

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

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
