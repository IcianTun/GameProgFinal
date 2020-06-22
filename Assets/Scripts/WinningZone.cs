using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningZone : MonoBehaviour
{
    public GameObject MissionPassContainer;
    AudioSource audioSource;
    public AudioSource bgMusic;
    public RubyController rubyController;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController controller = collision.GetComponent<RubyController>();
        if (controller != null)
        {
            // winning
            Debug.Log("win");
            MissionPassContainer.SetActive(true);
            bgMusic.Stop();
            audioSource.Play();
            if (rubyController)
            {
                rubyController.enabled = false;
            }

            // pause game use 1 to run game again
            Time.timeScale = 0;
        }

    }
}
