using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Cinemachine.CinemachineConfiner confiner;
    public bool Lock = false;
    public List<GameObject> lockObject;
    public List<GameObject> enemies;

    PolygonCollider2D polygon;

    private void Start()
    {
        polygon = confiner.m_BoundingShape2D.GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        //dev unlock
        Debug.Log(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
            Lock = false;

        if (!Lock)
        {
            //test unlock camera
            confiner.m_BoundingShape2D = polygon;
            confiner.InvalidatePathCache();
            foreach (GameObject obj in lockObject)
                obj.SetActive(false);
            Lock = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Lock)
                //test lock camera
                Debug.Log(collision.gameObject);
                confiner.m_BoundingShape2D = GetComponent<PolygonCollider2D>();
                confiner.InvalidatePathCache();
                foreach (GameObject obj in lockObject)
                    obj.SetActive(true);
            foreach(GameObject enemy in enemies)
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
