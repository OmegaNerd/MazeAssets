using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject MusicOffObject;
    public AudioSource musicSource;
    public AudioSource audioSource;
    private bool winSounded = false;
    private bool loseSounded = false;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip lazerSound;
    public AudioClip keySound;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.UpdateState(GameState.Game);
    }

    // Update is called once per frame
    void Update()
    {
        MusicOffObject.SetActive(!GameManager.musicOn);
        switch (GameManager.state) {
            case GameState.Game:
                if (GameManager.lazerSoundFlag == true) {
                    GameManager.lazerSoundFlag = false;
                    audioSource.PlayOneShot(lazerSound, 0.25f);
                }
                if (GameManager.keySoundFlag == true)
                {
                    GameManager.keySoundFlag = false;
                    audioSource.PlayOneShot(keySound);
                }
                pausePanel.SetActive(false);
                losePanel.SetActive(false);
                winPanel.SetActive(false);
                if (GameManager.musicOn)
                {
                    if (musicSource.isPlaying == false)
                    {
                        musicSource.Play();
                    }
                }
                else {
                    if (musicSource.isPlaying == true) {
                        musicSource.Stop();
                    }
                }
                break;
            case GameState.Pause:
                pausePanel.SetActive(true);
                losePanel.SetActive(false);
                winPanel.SetActive(false);
                if (GameManager.musicOn)
                {
                    if (musicSource.isPlaying == false)
                    {
                        musicSource.Play();
                    }
                }
                else
                {
                    if (musicSource.isPlaying == true)
                    {
                        musicSource.Stop();
                    }
                }
                break;
            case GameState.Lose:
                pausePanel.SetActive(false);
                losePanel.SetActive(true);
                winPanel.SetActive(false);
                if (musicSource.isPlaying == true)
                {
                    musicSource.Stop();
                }
                if (loseSounded == false)
                {
                    loseSounded = true;
                    audioSource.PlayOneShot(loseSound, 0.5f);
                }
                break;
            case GameState.Win:
                pausePanel.SetActive(false);
                losePanel.SetActive(false);
                winPanel.SetActive(true);
                if (musicSource.isPlaying == true)
                {
                    musicSource.Stop();
                }
                if (winSounded == false) {
                    winSounded = true;
                    audioSource.PlayOneShot(winSound, 0.5f);
                }
                break;
        }
    }

    public void PauseBut() {
        GameManager.UpdateState(GameState.Pause);
    }

    public void ResumeBut() {
        GameManager.UpdateState(GameState.Game);
    }

    public void MenuBut() {
        GameManager.destroyMusicFlag = true;
        GameManager.menuMusicCan = true;
        SceneManager.LoadScene("MenuScene");
    }

    public void LevelsBut(bool tutor = false)
    {
        GameManager.menuMusicCan = true;
        if (tutor == true)
        {
            SceneManager.LoadScene("TutorScene");
        }
        else {
            SceneManager.LoadScene("LevelsScene");
        }
    }

    public void RestartButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void MusicBut()
    {
        GameManager.musicOn = !GameManager.musicOn;
    }

}
