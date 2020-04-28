using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunBossRoomController : MonoBehaviour
{

    public Cinemachine.CinemachineConfiner confiner;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public GameObject confinerPolygon;

    public List<GameObject> lockObject;
    public List<GameObject> enemies;

    public float cameraYSize = 4.0f;

    public bool canRevert;

    [SerializeField]
    private bool islock = false;
    public bool Lock
    {
        get { return islock; }
        set
        {
            if (value)
            {
                foreach (GameObject obj in lockObject)
                    obj.SetActive(true);
            }
            if (!value)
            {
                confiner.InvalidatePathCache();
                foreach (GameObject obj in lockObject)
                    obj.SetActive(false);
            }
            islock = value;
        }
    }
    
    PolygonCollider2D oldPolygon;
    float oldCameraYSize;


    private void Start()
    {
        if (virtualCamera != null && confiner != null)
        {
            oldPolygon = confiner.m_BoundingShape2D as PolygonCollider2D;
            oldCameraYSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }
    

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("player enter");
            if (virtualCamera != null && confiner != null)
            {
                virtualCamera.m_Lens.OrthographicSize = cameraYSize;

                confiner.m_BoundingShape2D = confinerPolygon.GetComponent<PolygonCollider2D>();
                confiner.InvalidatePathCache();
            }
            Lock = true;
            for (int idx = enemies.Count - 1; idx >= 0; idx--)
            {
                GameObject enemy = enemies[idx];
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                    continue;
                }
                Enemy e = enemy.GetComponent<Enemy>();
                if (e != null)
                {
                    e.player = collision.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (canRevert)
            {
                virtualCamera.m_Lens.OrthographicSize = oldCameraYSize;
                confiner.m_BoundingShape2D = oldPolygon;
                confiner.InvalidatePathCache();
            }

            for (int idx = enemies.Count - 1; idx >= 0; idx--)
            {
                GameObject enemy = enemies[idx];
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                    continue;
                }
                Enemy e = enemy.GetComponent<Enemy>();
                if (e != null)
                {
                    e.player = null;
                }
            }
        }
    }
}
