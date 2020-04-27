using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Sprite sprite;

    private SpriteRenderer sr;
    private BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        if (isOpen) {
            sr.sprite = null;
            collider.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen) {
            sr.sprite = null;
            collider.isTrigger = true;
        }
        else
        {
            sr.sprite = sprite;
            collider.isTrigger = false;

        }

    }
}
