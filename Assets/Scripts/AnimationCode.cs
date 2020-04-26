using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCode : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameChange = 0.5f;

    private int index;
    private float timer;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        timer = frameChange;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            index = (index+1)%sprites.Length;
            sr.sprite = sprites[index];
            timer = frameChange;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
