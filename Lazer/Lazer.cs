using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject redLazer;
    public float timeClose;
    public float timeOpen;
    private float timeCurrent = 0;
    private bool opened = false;
    public float lenght;
    public float maxLenght;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            timeOpen = 2.25f + (maxLenght) * 0.8f;
            timeClose = timeOpen;
            timeCurrent += 1 * Time.deltaTime;
            redLazer.transform.localScale = new Vector3(lenght, 1, 1);
            if (opened == true)
            {
                redLazer.SetActive(false);
                if (timeCurrent >= timeOpen)
                {
                    timeCurrent = 0;
                    opened = false;
                }
            }
            else { 
                redLazer.SetActive(true);
                if (timeCurrent >= timeClose)
                {
                    timeCurrent = 0;
                    opened = true;
                }
            }
        }
    }
}
