using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCrystal : MonoBehaviour
{
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameController.GetComponent<ZezeController>().collectedCrystal += 1;
            gameController.GetComponent<ZezeController>().getCrystal();
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
