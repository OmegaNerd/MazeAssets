using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAspect : MonoBehaviour
{
    private Camera _cam;
    private float targetAspect = 16f / 9f;
    public CanvasScaler canvasScaler;
    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((float)Screen.width / (float)Screen.height < targetAspect)
        {
            _cam.orthographicSize = GameManager.CamSizeStart - 1f + Mathf.Pow(5f + (GameManager.MazeSize - 4)/2f, (targetAspect - (float)Screen.width / (float)Screen.height) * 1.5f);
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            _cam.orthographicSize = GameManager.CamSizeStart;
            canvasScaler.matchWidthOrHeight = 1;
            if ((float)Screen.width / (float)Screen.height > targetAspect)
            {
                //_cam.orthographicSize = 5 + ((float)Screen.width / (float)Screen.height - targetAspect) * 5f;
            }
            else
            {

            }

        }
    }
}
