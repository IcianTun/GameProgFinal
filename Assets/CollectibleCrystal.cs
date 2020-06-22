using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCrystal : MonoBehaviour
{
    public GameObject gameController;
    public AudioClip collectSoundClip;
    public RubyController ruby;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ruby.PlaySound(collectSoundClip);
            gameController.GetComponent<ZezeController>().collectedCrystal += 1;
            gameController.GetComponent<ZezeController>().getCrystal();
            Destroy(gameObject);
        }
    }
}
