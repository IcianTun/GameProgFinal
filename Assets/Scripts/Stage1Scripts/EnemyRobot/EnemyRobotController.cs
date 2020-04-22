using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotController : MonoBehaviour
{
    public float speed = 3.5f;
    public bool vertical;
    public float changeTime = 1.5f;

    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    Animator animator;

    NonPlayerCharacter npcScript;

    bool broken;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        broken = true;
        npcScript = GetComponent<NonPlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            animator.SetTrigger("Fixed");
            if (npcScript)
            {
                npcScript.enabled = true;

            }
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }


        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (broken)
        {
            RubyController player = other.gameObject.GetComponent<RubyController>();
            if (player != null)
            {
                player.ChangeHealth(-1);
            }

        }
    }

    public void Fix()
    {
        broken = false;
        //rigidbody2D.simulated = false;
        smokeEffect.Stop();
        GetComponent<AudioSource>().Stop();
    }
}
