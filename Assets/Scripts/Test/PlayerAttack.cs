using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackTime;
    public float startTimeAttack;

    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;

    public Camera cam;

    Vector2 mousePos;

    private Animator animator;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = attackLocation.GetComponent<SpriteRenderer>();

        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = transform.position;
        Vector2 mouseDir = (mousePos - pos).normalized;
        Vector2 up = transform.up;

        float angle = Vector2.Angle(up, mouseDir);

        if (pos.x > mousePos.x)
            angle = 360-angle;

        float rad = Mathf.Deg2Rad * angle;

        Vector2 offset = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)) * 1;

        attackLocation.position = pos + offset + new Vector2(0,0.5f);
        attackLocation.rotation = Quaternion.Euler(0, 0, 45 - angle);

        if (Input.GetButton("Fire2") && attackTime<=0)
        {
            attackTime = startTimeAttack;
            animator.SetTrigger("Launch");
            Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemies);

            for (int i = 0; i < damage.Length; i++)
            {
                //Destroy(damage[i].gameObject);
                damage[i].GetComponent<Enemy>().ChangeHealth(-1);
            }
        }

        if(attackTime > 0)
        {
            renderer.enabled = true;
            //anim.SetBool("Is_attacking", true);


            attackTime -= Time.deltaTime;
        }   
        else
        {
            //anim.SetBool("Is_attacking", false);
            renderer.enabled = false;
        }
    }
}
