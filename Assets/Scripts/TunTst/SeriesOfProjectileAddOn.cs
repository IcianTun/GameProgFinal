using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesOfProjectileAddOn : MonoBehaviour
{
    SeriesOfProjectile seriesOfProjectile;
    public Enemy enemyScript;

    float lastTriggerHpPercent;

    private void Start()
    {
        seriesOfProjectile = GetComponent<SeriesOfProjectile>();
        lastTriggerHpPercent = 1f;
    }

    void Update()
    {
        if(enemyScript.HpPercent < lastTriggerHpPercent - 0.25f)
        {
            seriesOfProjectile.numberOfProjectile += 1;
            lastTriggerHpPercent = enemyScript.HpPercent;
        }
    }
}
