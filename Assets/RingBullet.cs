using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBullet : Attack
{
    public GameObject projectilePrefab;
    public int numberOfProjectile = 3;
    public float radiusFromBoss;
    public float bulletSpeed = 3f;
    public float delayStartTime = 0.5f;
    public angleType angleType;
    public float offsetAngle = 0;
    public float bendAngle = 0;
    public bool focus;
    public float color;
    private float rotatingSpeed;
    

    List<GameObject> projectiles;
    Animator animator;
    Animator bulletAnimator;


    private void Start()
    {
        rotatingSpeed = offsetAngle;
        if (angleType == angleType.Cone)
        {
            offsetAngle = offsetAngle / 2;
        }
    }

    override public IEnumerator Perform(Enemy enemyScript)
    {
        projectiles = new List<GameObject>();

        if (animator == null)
            animator = enemyScript.GetComponent<Animator>();
        Vector3 playerPos = enemyScript.player.transform.position;
        Vector3 enemyPos = enemyScript.transform.position;
        Vector3 directionToPlayer = (playerPos - enemyPos).normalized;

        float angle = 360f / numberOfProjectile;

        if (angleType == angleType.Random)
        {
            offsetAngle = Random.Range(0f, 360f);
        }
        if (angleType == angleType.Rotating)
        {
            offsetAngle += rotatingSpeed;
            directionToPlayer = new Vector3(1, 1, 1).normalized;
        }
        if (angleType == angleType.Fixed)
        {
            directionToPlayer = new Vector3(0, 1, 1).normalized;
        }
        if (angleType == angleType.Cone)
        {
            angle = -2*offsetAngle / numberOfProjectile;
        }
        for (int i = 0; i < numberOfProjectile; i++)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, enemyPos + radiusFromBoss * (Quaternion.Euler(0, 0, i * angle + offsetAngle) * directionToPlayer), Quaternion.identity);
            

            ProjectileEnemy projectile = projectileObject.GetComponent<ProjectileEnemy>();
            Vector2 dir = directionToPlayer;
            if (focus)
            {
                Vector3 bulletPos = projectile.transform.position;
                dir = (playerPos - bulletPos).normalized;
            }
            projectile.DelayLaunch(Quaternion.Euler(0, 0, i * angle + offsetAngle + bendAngle) * dir, bulletSpeed*100f, delayStartTime);

            bulletAnimator = projectileObject.GetComponent<Animator>();
            bulletAnimator.SetFloat("Color", color);
        }
        yield return base.Perform(enemyScript);
    }

}

public enum angleType
{
    ToPlayer,
    Random,
    Fixed,
    Rotating,
    Cone
};
