﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSwitch : MonoBehaviour
{
    public bool switchOn = false;
    public bool oneTime = false;
    public Sprite normal;
    public Sprite pressed;
    public bool connectDoor;
    public GameObject door;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = normal;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void controlDoor()
    {
        if (connectDoor) {
            if (switchOn) {
                door.GetComponent<Door>().isOpen = true;
            } else
            {
                door.GetComponent<Door>().isOpen = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Interactable")
        {
            switchOn = true;
            sr.sprite = pressed;
            controlDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!oneTime && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Interactable") )
        {
            switchOn = false;
            sr.sprite = normal;
            controlDoor();
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Interactable")
    //    {
    //        switchOn = true;
    //        sr.sprite = pressed;
    //    }
    //}
   
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (!oneTime && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Interactable") )
    //    {
    //        switchOn = false;
    //        sr.sprite = normal;
    //    }
    //}
}
