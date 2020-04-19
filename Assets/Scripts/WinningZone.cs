using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningZone : MonoBehaviour
{
    public GameObject MissionPassContainer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController controller = collision.GetComponent<RubyController>();
        if (controller != null)
        {
            // winning
            Debug.Log("win");
            MissionPassContainer.SetActive(true);

            // pause game use 1 to run game again
            Time.timeScale = 0;
        }

    }
}
