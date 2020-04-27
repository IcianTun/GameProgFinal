using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalUI : MonoBehaviour
{
    public bool enable = false;
    public Sprite activeSprite;
    public Sprite deactiveSprite;

    private Image sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<Image>();
        if(enable == false)
        {
            sr.sprite = deactiveSprite;
           // Debug.Log("Deactivated");
        }
    }

    public void render()
    {
        sr.sprite = activeSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
