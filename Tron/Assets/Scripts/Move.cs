using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    public float speed = 16;

    public GameObject wallPrefab;

    Collider2D wall;

    Vector2 lastWallEnd;

    public GameObject[] walls;
    int wallsCount = 0;

    public Text win;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnwall();
    }

    void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnwall();
        }
        else if (Input.GetKeyDown(downKey))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            spawnwall();
        }
        else if (Input.GetKeyDown(rightKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnwall();
        }
        else if (Input.GetKeyDown(leftKey))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            spawnwall();
        }

        fitColliderBetween(wall, lastWallEnd, transform.position);
    }

    void spawnwall()
    {
        lastWallEnd = transform.position;

        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        walls[wallsCount] = g;
        wallsCount = wallsCount + 1;
        wall = g.GetComponent<Collider2D>();
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * 0.5f;

        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.tag == "PowerUp")
        {
            StartCoroutine("CountTime");
        }

        else if(co.tag == "UpWall")
        {
            if (name == "Cyan")
            {
                FindObjectOfType<UIManager>().pinkPointsUp();
                win.text = ("Pink Won");
            }
            if (name == "Pink")
            {
                FindObjectOfType<UIManager>().bluePointsUp();
                win.text = ("Cyan Won");
            }

            for (int i = 0; i < wallsCount; i++)
            {
                Destroy(walls[i]);
            }

            Debug.Log(wallsCount);
            wallsCount = 0;
            spawnwall();
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            FindObjectOfType<UIManager>().Win();
        }

        else if (co.tag == "DownWall")
        {
            if (name == "Cyan")
            {
                FindObjectOfType<UIManager>().pinkPointsUp();
                win.text = ("Pink Won");
            }
            if (name == "Pink")
            {
                FindObjectOfType<UIManager>().bluePointsUp();
                win.text = ("Cyan Won");
            }

            for (int i = 0; i < wallsCount; i++)
            {
                Destroy(walls[i]);
            }

            wallsCount = 0;
            spawnwall();
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            FindObjectOfType<UIManager>().Win();
        }

        else if (co.tag == "LeftWall")
        {
            if (name == "Cyan")
            {
                FindObjectOfType<UIManager>().pinkPointsUp();
                win.text = ("Pink Won");
            }
            if (name == "Pink")
            {
                FindObjectOfType<UIManager>().bluePointsUp();
                win.text = ("Cyan Won");
            }

            for (int i = 0; i < wallsCount; i++)
            {
                Destroy(walls[i]);
            }
            
            wallsCount = 0;
            spawnwall();
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            FindObjectOfType<UIManager>().Win();
        }

        else if (co.tag == "RightWall")
        {
            if (name == "Cyan")
            {
                FindObjectOfType<UIManager>().pinkPointsUp();
                win.text = ("Pink Won");
            }
            if (name == "Pink")
            {
                FindObjectOfType<UIManager>().bluePointsUp();
                win.text = ("Cyan Won");
            }

            for (int i = 0; i < wallsCount; i++)
            {
                Destroy(walls[i]);
            }
            
            wallsCount = 0;
            spawnwall();
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            FindObjectOfType<UIManager>().Win();
        }

        else if (co.tag == "Barrier")
        {
            if (name == "Cyan")
            {
                FindObjectOfType<UIManager>().pinkPointsUp();
                win.text = ("Pink Won");
            }
            if (name == "Pink")
            {
                FindObjectOfType<UIManager>().bluePointsUp();
                win.text = ("Cyan Won");
            }

            for (int i = 0; i < wallsCount; i++)
            {
                Destroy(walls[i]);
            }

            wallsCount = 0;
            spawnwall();
            FindObjectOfType<UIManager>().Win();
        }

        else if (co != wall)
        {
            if (name == "Cyan")
            {
                FindObjectOfType<UIManager>().pinkPointsUp();
                win.text = ("Pink Won");
            }
            if (name == "Pink")
            {
                FindObjectOfType<UIManager>().bluePointsUp();
                win.text = ("Cyan Won");
            }

            for (int i = 0; i < wallsCount; i++)
            {
                Destroy(walls[i]);
            }
            
            wallsCount = 0;
            spawnwall();
            FindObjectOfType<UIManager>().Win();
        }
    }

    IEnumerator CountTime()
    {
        int count = 0;
        speed = 32;
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log(count);
            count++;
            if (count == 5)
            {
                count = 0;
                speed = 16;
                StopCoroutine("CountTime");
            }
        }
    }
}