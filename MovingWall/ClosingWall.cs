using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingWall : MonoBehaviour
{
    public float timeOpen;
    public float timeClose;
    public float posZMax;
    public float posZMin;
    private float timeCurrent = 0;
    private bool opened = false;
    public GameObject wall1;
    public GameObject wall2;
    public float speed = 1f;
    public GameObject loseTrigger1;
    public GameObject loseTrigger2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game)
        {
            timeCurrent += 1 * Time.deltaTime;
            if (opened == true)
            {
                if (wall1.transform.localPosition.z > posZMin)
                {
                    wall1.transform.localPosition += new Vector3(0, 0, -speed) * Time.deltaTime;
                    wall2.transform.localPosition += new Vector3(0, 0, speed) * Time.deltaTime;
                }
                else
                {
                    wall1.transform.localPosition = new Vector3(0, 0, posZMin);
                    wall2.transform.localPosition = new Vector3(0, 0, -posZMin);
                }
                if (timeCurrent >= timeOpen)
                {
                    timeCurrent = 0;
                    opened = false;
                }
                loseTrigger1.SetActive(false);
                loseTrigger2.SetActive(false);
            }
            else
            {
                if (wall1.transform.localPosition.z < posZMax)
                {
                    wall1.transform.localPosition += new Vector3(0, 0, speed) * Time.deltaTime;
                    wall2.transform.localPosition += new Vector3(0, 0, -speed) * Time.deltaTime;
                    loseTrigger1.SetActive(true);
                    loseTrigger2.SetActive(true);
                }
                else
                {
                    wall1.transform.localPosition = new Vector3(0, 0, posZMax);
                    wall2.transform.localPosition = new Vector3(0, 0, -posZMax);
                    loseTrigger1.SetActive(false);
                    loseTrigger2.SetActive(false);
                }
                if (timeCurrent >= timeClose)
                {
                    timeCurrent = 0;
                    opened = true;
                }
            }
        }
    }
}
