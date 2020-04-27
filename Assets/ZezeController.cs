using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZezeController : MonoBehaviour
{
    public GameObject crystal1;
    public GameObject crystal2;
    public GameObject crystal3;

    public int collectedCrystal = 0;
    public GameObject lastDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void getCrystal()
    {
        if (collectedCrystal == 1)
        {
            crystal3.GetComponent<CrystalUI>().enable = true;
            crystal3.GetComponent<CrystalUI>().render();
        }
        if (collectedCrystal == 2)
        {
            crystal2.GetComponent<CrystalUI>().enable = true;
            crystal2.GetComponent<CrystalUI>().render();
        }
        if (collectedCrystal == 3)
        {
            crystal1.GetComponent<CrystalUI>().enable = true;
            crystal1.GetComponent<CrystalUI>().render();
            lastDoor.GetComponent<Door>().enable = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
