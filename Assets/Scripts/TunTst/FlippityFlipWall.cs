using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippityFlipWall : MonoBehaviour
{
    public float aliveTime;
    public FlippityFlipWallDamageZone damageZone;

    void Start()
    {
        StartCoroutine(DisableDamage());
        StartCoroutine(DestroyAfter(aliveTime));
    }

    public IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(0.3f);
        damageZone.DisableDamageZone();
        yield return null;
    }

}
