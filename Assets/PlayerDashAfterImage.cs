using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAfterImage : MonoBehaviour
{
    private float activeTime = 0.8f;
    private float timeActivated;
    private float alpha;

    private float alphaSet = 0.97f;
    private float alphaMultiplier = 0.9f;


    private GameObject _player;    
    public GameObject Player
    {
        get { return _player;  }
        set
        {
            SpriteRenderer playerSR = value.GetComponent<SpriteRenderer>();
            SR.sprite = playerSR.sprite;
            SR.flipX = playerSR.flipX;
            transform.position = value.transform.position;
            transform.rotation = value.transform.rotation;
            _player = value;
        }
    }

    private SpriteRenderer SR;
    
    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        // get player transform.
        alpha = alphaSet;
        timeActivated = Time.time;
    }


    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;
        if(Time.time >= (timeActivated + activeTime))
        {
            AfterImagePool.Instance.AddToPool(gameObject);

        }
    }

}
