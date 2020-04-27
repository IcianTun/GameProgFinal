using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTelegraph : MonoBehaviour
{


    public void SetDestroyTimer(float time)
    {
        StartCoroutine(DestroyAfter(time));
    }

    IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
