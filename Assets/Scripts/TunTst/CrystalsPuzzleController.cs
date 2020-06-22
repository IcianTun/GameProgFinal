using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsPuzzleController : MonoBehaviour
{
    public CrystalNPC redCrystal;
    public CrystalNPC greenCrystal;
    public CrystalNPC blueCrystal;

    public Door lockObject;
    
    private List<CrystalColor> solutionList = new List<CrystalColor>()
                                            {
                                                CrystalColor.Red,CrystalColor.Red,CrystalColor.Red,
                                                CrystalColor.Blue,CrystalColor.Green,CrystalColor.Green,CrystalColor.Blue
                                            };
    public int currentIndex = 0;
    private bool locked = true;


    public AudioClip collectSoundClip;
    public RubyController ruby;


    private void Start()
    {
        currentIndex = 0;
    }

    public void Pressed(CrystalNPC crystalNPC)
    {
        if (locked)
        {
            if (crystalNPC.color == solutionList[currentIndex])
            {
                currentIndex += 1;
            }
            else
            {
                currentIndex = 0;
            }
            if (currentIndex == solutionList.Count)
            {
                Unlock();
            }
        }
    }

    void Unlock()
    {

        ruby.PlaySound(collectSoundClip);
        lockObject.enable = true;
        locked = false;

    }

}
