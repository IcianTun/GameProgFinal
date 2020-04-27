using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenEnemiesRoom : RoomController
{
    public List<GameObject> hiddenEnemies;

    public Hostage hostage;

    private void Update()
    {
        if (hostage.rescued)
        {
            for (int idx = hiddenEnemies.Count - 1; idx >= 0; idx--)
            {
                GameObject hiddenEnemy = hiddenEnemies[idx];
                hiddenEnemy.SetActive(true);
                enemies.Add(hiddenEnemy);
                hiddenEnemies.Remove(hiddenEnemy);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
