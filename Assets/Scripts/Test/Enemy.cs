using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public int maxHealth;
    public float timeInvincible;

    public int health { get { return currentHealth; } }
    public EnemyHealthBar enemyHealthBar;

    //test need to change to private and find in own room
    public GameObject player;

    #region tuntst
    public List<Attack> attackList;
    public Attack currentAttack; //for debug or select next attack not same as current
    public bool nextAttackReady = true;

    public Vector2 tstVector2PlayerPos;
    #endregion

    //protected variable for child class

    [SerializeField]
    protected int currentHealth;
    protected bool isInvincible;
    protected float invincibleTimer;

    protected Rigidbody2D rigidbody2d;

    protected Animator animator;
    protected Vector2 lookDirection = new Vector2(1, 0);

    public float HpPercent { get { return (float)currentHealth / maxHealth; } }

    SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        enemyHealthBar = GetComponent<EnemyHealthBar>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    protected virtual void Update()
    {
        ////ded
        //if (currentHealth == 0)
        //{
        //    //animator.SetTrigger("Fixed");
        //    Destroy(gameObject);
        //    return;
        //    //or
        //}
        /*

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
                while (attackList.Count > 1 && attack == currentAttack)
                {
                    int b = Random.Range(0, attackList.Count);
                    attack = attackList[b];
                }
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
            StartCoroutine(PlayFlickerAnimation());
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (enemyHealthBar)
            enemyHealthBar.SetValue(currentHealth / (float)maxHealth);

        if (currentHealth == 0 )
        {
            Unlock();
            Destroy(gameObject);
        }
    }
    IEnumerator PlayFlickerAnimation()
    {
        //Debug.Log("Enter flick!");
        bool tst = false;
        while (isInvincible)
        {
            //Debug.Log("flick!");
            spriteRenderer.enabled = tst;
            yield return new WaitForSeconds(0.1f);
            tst = !tst ;
        }
        spriteRenderer.enabled = true;
        yield return null;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    virtual protected void Unlock()
    {

    }


    public void MoveToPosition(Vector3 targetPosition, float timeToMove, float delay)
    {
        StartCoroutine(_MoveToPosition(targetPosition, timeToMove, delay));
    }

    private IEnumerator _MoveToPosition(Vector3 targetPosition, float timeToMove, float delay)
    {
        yield return new WaitForSeconds(delay);

        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, targetPosition, t);
            yield return null;
        }
    }

}
