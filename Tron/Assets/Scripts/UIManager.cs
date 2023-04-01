using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image splash;
    public Text name;
    public GameObject buttonPlay;
    public GameObject buttonQuit;
    public Text countUp;
    public int timePlayed;
    public Text pointsBlue;
    public int bluePoints;
    public Text pointsPink;
    public int pinkPoints;
    public Text win;

    int count = 10;

    public GameObject powerUp;

    public GameObject[] barriers;
    public GameObject[] barriersD;
    public int barriersCount = 0;

    Vector3 GeneratedPosition()
    {
        int min = -63;
        int max = 63;
        int x, y, z;
        x = Random.Range(min, max);
        y = Random.Range(min, max);
        z = 0;

        return new Vector3(x, y, z);
    }

    void PlaceCubes()
    {
        Instantiate(powerUp, GeneratedPosition(), Quaternion.identity);
    }

    Vector3 GeneratedPositionB()
    {
        int min = -50;
        int max = 50;
        int x, y, z;
        x = Random.Range(min, max);
        y = Random.Range(min, max);
        z = 0;

        return new Vector3(x, y, z);
    }

    void PlaceBarriers()
    {
        int rotation = 0;
        for(int i = 0; i < barriers.Length; i++)
        {
            if(i > 1)
                rotation = 90;

            GameObject g = Instantiate(barriers[i], GeneratedPositionB(), Quaternion.Euler(0, 0, rotation));
            barriersD[barriersCount] = g;
            barriersCount++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        countUp.enabled = false;
        pointsBlue.enabled = false;
        pointsPink.enabled = false;
        StartCoroutine("CountTime");
    }

    // Update is called once per frame
    void Update()
    {
        countUp.text = ("Time Played: " + timePlayed);
        pointsBlue.text = ("Points: " + bluePoints);
        pointsPink.text = ("Points: " + pinkPoints);

        if (timePlayed == count)
        {
            count = count + 10;
            PlaceCubes();
        }
    }

    public void Play()
    {
        splash.enabled = false;
        name.enabled = false;
        buttonPlay.SetActive(false);
        buttonQuit.SetActive(false);
        Time.timeScale = 1;
        countUp.enabled = true;
        pointsBlue.enabled = true;
        pointsPink.enabled = true;
        win.text = ("");
        PlaceCubes();
        PlaceBarriers();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Win()
    {
        Time.timeScale = 0;
        name.enabled = true;
        buttonPlay.SetActive(true);
        buttonQuit.SetActive(true);
        for(int i = 0; i < count; i++)
        {
            Destroy(barriersD[i]);
            barriersCount = 0;
        }
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timePlayed++;           
        }    
    }

    public void bluePointsUp()
    {
        bluePoints++;
    }

    public void pinkPointsUp()
    {
        pinkPoints++;
    }
}
