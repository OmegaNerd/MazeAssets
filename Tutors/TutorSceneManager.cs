using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorSceneManager : MonoBehaviour
{
    [SerializeField] private float startCamSize;
    [SerializeField] private int keysNeed;
    public GameObject key1;
    public GameObject key2;
    public GameObject key1Fill;
    public GameObject key2Fill;
    public MazeBlock mazeBlockFinal;
    [SerializeField] private bool spawnMultiLazer = false;
    public GameObject MultiLazer;
    public GameObject BlockForMultiLazer;
    [SerializeField] float lazerDelay;
    private float timeCurrent = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.CamSizeStart = startCamSize;
        GameManager.keysNeed = keysNeed;
        GameManager.keysCurrent = 0;
        timeCurrent = lazerDelay - 0.5f;
        GameManager.MazeSize = 4;
        GameManager.levelMode = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            key1.SetActive(GameManager.keysNeed >= 1);
            key2.SetActive(GameManager.keysNeed >= 2);
            key1Fill.SetActive(GameManager.keysCurrent >= 1);
            key2Fill.SetActive(GameManager.keysCurrent >= 2);
            if (GameManager.keysCurrent >= GameManager.keysNeed)
            {
                mazeBlockFinal.leftOpen = true;
            }
            else {
                mazeBlockFinal.leftOpen = false;
            }
            if (spawnMultiLazer == true) {
                timeCurrent += 1 * Time.deltaTime;
                if (timeCurrent >= lazerDelay) {
                    timeCurrent = 0;
                    Instantiate(MultiLazer, BlockForMultiLazer.transform.position, new Quaternion());
                }
            }
        }
        
    }
}
