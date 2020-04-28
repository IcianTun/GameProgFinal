using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippityFlipWall : MonoBehaviour
{

    public float aliveTime;

    void Start()
    {
        StartCoroutine(DestroyAfter(aliveTime));
    }

    public IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }



}
