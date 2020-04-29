using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{

    public float circleMoveSpeed = 1;
    public float circleSize = 1;
    public float circleGrowSpeed = 0.1f;

    public Vector3 centerPos;
    public float offsetAngle;

    float myTime;
    Vector2 startPos;

    private void Start()
    {
        myTime = 0;
        startPos = new Vector2(transform.position.x, transform.position.y);

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

        circleSize += circleGrowSpeed * Time.deltaTime;
        transform.position = new Vector3(xPos, yPos,0) + centerPos;

        myTime += Time.deltaTime;
        Vector2 current = new Vector2(transform.position.x, transform.position.y);
        //if (transform.position.magnitude > 1000.0f)
        if (Vector2.Distance(startPos, current) > 20)
        {
            Destroy(gameObject);
        }
    }
}
