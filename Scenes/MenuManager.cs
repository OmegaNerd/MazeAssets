using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MusicOffObject;
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.CamSizeStart = 1;
        DontDestroyOnLoad(musicSource.gameObject);
        Time.timeScale = 1;
        GameManager.MazeSize = -1;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        MusicOffObject.SetActive(!GameManager.musicOn);
        /*if (GameManager.musicOn == true)
        {
            if (musicSource.isPlaying == false)
            {
                musicSource.Play();
            }
        }
        else {
            if (musicSource.isPlaying == true)
            {
                musicSource.Stop();
            }
        }*/
    }

    public void MazesButton() {
        SceneManager.LoadScene("LevelsScene");
    }

    public void MusicBut() {
        GameManager.musicOn = !GameManager.musicOn;
    }
}
