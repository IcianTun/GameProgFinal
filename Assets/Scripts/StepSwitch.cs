using System.Collections;
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
    public bool delaySwitch;
    public GameObject stopTriggerObject;
    public float delayTime = 1.0f;
    private float timer;

    private bool exit;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = normal;
        timer = delayTime;
        exit = false;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        if (delaySwitch)
        {
            if (exit && timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer < 0)
            {
                exit = false;
                timer = delayTime;
                Debug.Log("Switch back");
                switchOn = false;
                sr.sprite = normal;
                // Do something
                try
                {
                    stopTriggerObject.GetComponent<DamageZone>().enable = true;
                }
                catch
                {
                    stopTriggerObject.GetComponent<Door>().enable = false;
                }
            }
        }
    }

    private void controlDoor()
    {
        if (connectDoor) {
            if (switchOn) {
                door.GetComponent<Door>().enable = true;
            } else
            {
                door.GetComponent<Door>().enable = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Interactable")
        {
            if (delaySwitch) {
                exit = false;
                try {
                    stopTriggerObject.GetComponent<DamageZone>().enable = false;
                }
                catch {
                    stopTriggerObject.GetComponent<Door>().enable = true;
                }
            }
            switchOn = true;
            sr.sprite = pressed;
            controlDoor();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Interactable")
        {
            if (delaySwitch)
            {
                exit = false;
                try
                {
                    stopTriggerObject.GetComponent<DamageZone>().enable = false;
                }
                catch
                {
                    stopTriggerObject.GetComponent<Door>().enable = true;
                }
            }
            switchOn = true;
            sr.sprite = pressed;
            controlDoor();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!oneTime && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Interactable") )
        {
            if (delaySwitch)
            {
                exit = true;
            }
            if (!delaySwitch)
            {
                sr.sprite = normal;
                switchOn = false;

            }
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
