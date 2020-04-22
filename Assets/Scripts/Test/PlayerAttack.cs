using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackTime;
    public float startTimeAttack;

    public Transform sword;
    public float attackRange;
    public LayerMask enemies;

    public Camera cam;

    Vector2 mousePos;

    private Animator animator;
    private SpriteRenderer renderer;

    MeleeWeapon melee;

    Vector2 stabPos;
    Vector2 stabDir;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = sword.GetComponent<SpriteRenderer>();

        renderer.enabled = false;
        melee = sword.GetComponent<MeleeWeapon>();
        sword.GetComponent<PolygonCollider2D>().enabled = false;
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

        Vector2 offset = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));

        if (Input.GetButton("Fire2") && attackTime<=0)
        {
            attackTime = startTimeAttack;
            animator.SetTrigger("Launch");

            stabPos = mousePos;
            stabDir = mouseDir;

            animator.SetFloat("Look X", mouseDir.x);
            animator.SetFloat("Look Y", mouseDir.y);
        }

        if(attackTime > 0)
        {
            angle = Vector2.Angle(up, stabDir);

            if (pos.x > stabPos.x)
                angle = 360 - angle;

            rad = Mathf.Deg2Rad * angle;

            offset = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
            renderer.enabled = true;
            sword.GetComponent<PolygonCollider2D>().enabled = true;
            sword.position = pos + offset - stabDir * Mathf.Pow(attackTime / startTimeAttack,2);

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
}
