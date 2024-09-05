using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    public float timeOpen;
    public float timeClose;
    public float posYMax;
    private float timeCurrent = 0;
    private bool opened = true;
    public GameObject blade;
    public float speed = 1f;
    public GameObject loseTrigger;
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
                if (blade.transform.localPosition.y > 0)
                {
                    blade.transform.localPosition += new Vector3(0, -speed, 0) * Time.deltaTime;
                    loseTrigger.SetActive(true);
                }
                else
                {
                    blade.transform.localPosition = new Vector3(0, 0, 0);
                    loseTrigger.SetActive(false);
                }
                if (timeCurrent >= timeOpen) {
                    timeCurrent = 0;
                    opened = false;
                }
            }
            else {
                loseTrigger.SetActive(true);
                if (blade.transform.localPosition.y < posYMax)
                {
                    blade.transform.localPosition += new Vector3(0, speed, 0) * Time.deltaTime;
                }
                else
                {
                    blade.transform.localPosition = new Vector3(0, posYMax, 0);
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
