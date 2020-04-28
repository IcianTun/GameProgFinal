using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalNPC : NonPlayerCharacter
{
    public CrystalColor color;
    public CrystalsPuzzleController puzzleController;
    

    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    public override void DisplayDialog()
    {
        StartCoroutine(Shake());

    }

    IEnumerator Shake()
    {
        puzzleController.Pressed(this);
        float _distance = 0.1f;
        float shakeTime = 0.5f;
        float _timer = 0;
        var _startPos = transform.position;
        while (_timer < shakeTime)
        {
            _timer += Time.deltaTime;
            transform.position = _startPos + (Random.insideUnitSphere * _distance); ;
            yield return null;
        }
        transform.position = _startPos;
    }
}

public enum CrystalColor{

    Red,
    Green,
    Blue,

}