using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    public float speed;

    public int maxHealth;
    public float timeInvincible;

    public int health { get { return currentHealth; } }

    public float nextWaypointDistance = 3f;

    public float delayAfterRescue;
    public float healCooldown;

    private int currentHealth;
    private bool isInvincible;
    private float invincibleTimer;

    public Transform target;

    float delay = 0;
    float cooldown = 0;

    Rigidbody2D rigidbody2d;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    
    AIPath aIPath;
    Seeker seeker;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public bool rescued = false;
    public EnemyHealthBar healthBar;
    public GameObject healthBarObj;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        aIPath = GetComponent<AIPath>();
        seeker = GetComponent<Seeker>();
    }

    public void SetDestination(Transform target)
    {
        if (this.target == null)
        {
            InvokeRepeating("UpdatePath", 0f, .5f);
        }
        this.target = target;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rigidbody2d.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("Move X", lookDirection.x);

        if (currentHealth == 0)
        {
            Destroy(gameObject);
            return;
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (path == null)
            return;
        
        if (delay < delayAfterRescue)
        {
            delay += Time.deltaTime;
            if (delay >= delayAfterRescue)
            {
                rescued = true;
                healthBarObj.SetActive(true);
            }
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 position = rigidbody2d.position;
        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - position).normalized;

        if (!Mathf.Approximately(dir.x, 0.0f))
        {
            lookDirection.Set(dir.x, 0);
            lookDirection.Normalize();
        }

        float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        animator.SetFloat("Speed", Mathf.Abs(aIPath.desiredVelocity.normalized.x));

        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    public void Heal(RubyController player)
    {
        if (rescued)
        {
            if (cooldown <= 0)
            {
                player.ChangeHealth(1);
                cooldown = healCooldown;
                animator.SetTrigger("Heal");
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            //play hit animation
            animator.SetTrigger("Hit");

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (healthBar)
            healthBar.SetValue(currentHealth / (float)maxHealth);
    }
}
