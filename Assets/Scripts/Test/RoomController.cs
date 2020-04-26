using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Cinemachine.CinemachineConfiner confiner;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private bool islock = false;
    public List<GameObject> lockObject;
    public List<GameObject> enemies;

    public float cameraYSize = 4.0f;
    PolygonCollider2D polygon;

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
        polygon = confiner.m_BoundingShape2D.GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        //dev unlock
        if (Input.GetKeyDown(KeyCode.Space))
            Lock = false;

        if (!Lock)
        {
            //test unlock camera
            foreach (GameObject obj in lockObject)
                obj.SetActive(false);
            Lock = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.tag == "Player")
        {
            Debug.Log("player enter");
            virtualCamera.m_Lens.OrthographicSize = cameraYSize;
            Lock = true;
            if (Lock)
                //test lock camera
                confiner.m_BoundingShape2D = GetComponent<PolygonCollider2D>();
                confiner.InvalidatePathCache();
                
                foreach (GameObject obj in lockObject)
                    obj.SetActive(true);
            foreach (GameObject enemy in enemies)
            {
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
            foreach (GameObject enemy in enemies)
            {
                Enemy e = enemy.GetComponent<Enemy>();
                if (e != null)
                {
                    e.player = null;
                }
            }
        }
    }
}
