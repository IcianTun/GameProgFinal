using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{

    public float circleMoveSpeed = 1;
    public float circleSize = 1;
    public float circleGrowSpeed = 0.01f;

    public Vector3 centerPos;
    public float offsetAngle;

    float myTime;

    private void Start()
    {
        myTime = 0;
    }

    public void Setup(Vector3 centerPos, float offsetAngle)
    {
        this.centerPos = centerPos;
        this.offsetAngle = offsetAngle;
    }

    // Update is called once per frame
    void Update()
    {
        
        var xPos = Mathf.Sin(myTime * circleMoveSpeed + offsetAngle * Mathf.Deg2Rad) * circleSize;
        var yPos = Mathf.Cos(myTime * circleMoveSpeed + offsetAngle * Mathf.Deg2Rad) * circleSize;

        circleSize += circleGrowSpeed;
        transform.position = new Vector3(xPos, yPos,0) + centerPos;

        myTime += Time.deltaTime;
    }
}
