using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool enableFlashlight = true;
    public Sprite flashlight;
    // Start is called before the first frame update
    void Start()
    {
        if (!enableFlashlight)
        {
            gameObject.GetComponent<SpriteMask>().sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteMask>().sprite = flashlight;
        }
    }
}
