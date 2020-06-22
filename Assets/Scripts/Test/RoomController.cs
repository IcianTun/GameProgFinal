using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public PolygonCollider2D confinerShape;
    public Cinemachine.CinemachineConfiner confiner;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private bool islock = false;
    public List<GameObject> lockObject;
    public List<GameObject> enemies;

    public float cameraYSize = 4.0f;

    public bool canRevert;

    PolygonCollider2D oldPolygon;
    float oldCameraYSize;

    public bool Lock
    {
        get { return islock; }
        set {
            if (!value)
                confiner.InvalidatePathCache();
            islock = value;
        }
    }

    private void Start()
    {
        if (virtualCamera != null && confiner != null)
        {
            oldPolygon = confiner.m_BoundingShape2D as PolygonCollider2D;
            oldCameraYSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }

    private void Update()
    {
        if (!Lock)
        {
            //test unlock camera
            foreach (GameObject obj in lockObject)
                obj.SetActive(false);
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

                if (confinerShape)
                {
                    confiner.m_BoundingShape2D = confinerShape;
                }
                else {
                    confiner.m_BoundingShape2D = GetComponent<PolygonCollider2D>();
                }

                confiner.InvalidatePathCache();
            }
 
            foreach (GameObject obj in lockObject)
                obj.SetActive(true);
            for (int idx = enemies.Count - 1; idx >= 0; idx--)
            {
                GameObject enemy = enemies[idx];
                if (enemy == null)
                {
                    enemies.Remove(enemy);
                    continue;
                }
                Enemy e = enemy.GetComponent<Enemy>();
                if (e!= null)
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
