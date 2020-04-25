using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public int maxHealth;
    public float timeInvincible;

    public int health { get { return currentHealth; } }

    //test need to change to private and find in own room
    public GameObject player;

    #region tuntst
    public List<Attack> attackList;
    public Attack currentAttack; //for debug or select next attack not same as current
    public bool nextAttackReady = true;

    public Vector2 tstVector2PlayerPos;
    #endregion

    //protected variable for child class
    protected int currentHealth;
    protected bool isInvincible;
    protected float invincibleTimer;

    protected Rigidbody2D rigidbody2d;

    protected Animator animator;
    protected Vector2 lookDirection = new Vector2(1, 0);

    protected virtual void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    public void Initiate(int hp, float speed, float iframe)
    {
        this.maxHealth = hp;
        this.speed = speed;
        this.timeInvincible = iframe;
    }

    protected virtual void Update()
    {
        //ded
        if (currentHealth == 0)
        {
            //animator.SetTrigger("Fixed");
            Destroy(gameObject);
            return;
            //or
        }
        /*
         if (player == null)
            return;


        //calculate from player pos
        Vector2 playerPos = player.GetComponent<Rigidbody2D>().position;
        Vector2 position = rigidbody2d.position;

        float horizontal = playerPos.x - position.x;
        float vertical = playerPos.y - position.y;

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);


        position = position + move * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
        */
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        #region tuntst

        if(player != null)
        {
            tstVector2PlayerPos = player.transform.position;
            if (nextAttackReady && attackList.Count > 0)
            {
                nextAttackReady = false;
                int a = Random.Range(0, attackList.Count);
                Attack attack = attackList[a];
                currentAttack = attack;
                StartCoroutine(attack.Perform(this));
                //waitDelayForNextAttack = attack.totalSubAttacksExecuteTime + attack.delayAfterAttack;
            }

            Vector2 playerPos = player.GetComponent<Rigidbody2D>().position;
            Vector2 position = rigidbody2d.position;

            float horizontal = playerPos.x - position.x;
            float vertical = playerPos.y - position.y;

            lookDirection = new Vector2(horizontal, vertical).normalized;
        }

        #endregion

    }

public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            //play hit animation

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
