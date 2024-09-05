using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public GameObject musicOffObject;
    private int levelIndex = 0;
    public GameObject tutorialBut;
    public GameObject generateBut;
    public GameObject leftBut;
    public GameObject rightBut;
    public GameObject levelModes;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject modeText1;
    public GameObject modeText2;
    public GameObject modeText3;
    public GameObject choose1;
    public GameObject choose2;
    public GameObject choose3;
    [SerializeField]private int tutorsCount;
    public TMP_Text sizeText;
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.CamSizeStart = 1;
        
        GameManager.levelMode = PlayerPrefs.GetInt("Levels") % 3 + 1;
        if (PlayerPrefs.GetInt("Tutors") < tutorsCount)
        {
            levelIndex = 0;
        }
        else
        {
            levelIndex = PlayerPrefs.GetInt("Levels") / 3 + 1;
            if (levelIndex > 12)
            {
                levelIndex = 12;
                GameManager.levelMode = 3;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (GameManager.musicOn == true)
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
        }*/
        GameManager.MazeSize = levelIndex + 3;
        sizeText.text = GameManager.MazeSize.ToString() + "x" + GameManager.MazeSize.ToString();
        GameManager.currentLevelNumber = (levelIndex - 1) * 3 + GameManager.levelMode;
        if (PlayerPrefs.GetInt("Levels") / 3 >= levelIndex - 1 && PlayerPrefs.GetInt("Tutors") >= tutorsCount)
        {
            switch (GameManager.levelMode)
            {
                case 1:
                    choose1.SetActive(true);
                    choose2.SetActive(false);
                    choose3.SetActive(false);
                    break;
                case 2:
                    choose1.SetActive(false);
                    choose2.SetActive(true);
                    choose3.SetActive(false);
                    break;
                case 3:
                    choose1.SetActive(false);
                    choose2.SetActive(false);
                    choose3.SetActive(true);
                    break;
            }
        }
        else {
            choose1.SetActive(false);
            choose2.SetActive(false);
            choose3.SetActive(false);
        }
        musicOffObject.SetActive(!GameManager.musicOn);
        if (levelIndex == 0)
        {
            generateBut.SetActive(false);
            tutorialBut.SetActive(true);
            leftBut.SetActive(false);
            levelModes.SetActive(false);
        }
        else {
            if (PlayerPrefs.GetInt("Tutors") < tutorsCount)
            {
                generateBut.SetActive(false);
                lock1.SetActive(true);
                lock2.SetActive(true);
                lock3.SetActive(true);
                modeText1.SetActive(false);
                modeText2.SetActive(false);
                modeText3.SetActive(false);
            }
            else {
                if (PlayerPrefs.GetInt("Levels") / 3 > levelIndex - 1) {
                    lock1.SetActive(false);
                    lock2.SetActive(false);
                    lock3.SetActive(false);
                    modeText1.SetActive(true);
                    modeText2.SetActive(true);
                    modeText3.SetActive(true);
                    generateBut.SetActive(true);
                }
                else {
                    if (PlayerPrefs.GetInt("Levels") / 3 == levelIndex - 1 && PlayerPrefs.GetInt("Levels") % 3 == 2)
                    {
                        lock1.SetActive(false);
                        lock2.SetActive(false);
                        lock3.SetActive(false);
                        modeText1.SetActive(true);
                        modeText2.SetActive(true);
                        modeText3.SetActive(true);
                        generateBut.SetActive(true);
                    }
                    if (PlayerPrefs.GetInt("Levels") / 3 == levelIndex - 1 && PlayerPrefs.GetInt("Levels") % 3 == 1) {
                        lock1.SetActive(false);
                        lock2.SetActive(false);
                        lock3.SetActive(true);
                        modeText1.SetActive(true);
                        modeText2.SetActive(true);
                        modeText3.SetActive(false);
                        generateBut.SetActive(true);
                    }
                    if (PlayerPrefs.GetInt("Levels") / 3 == levelIndex - 1 && PlayerPrefs.GetInt("Levels") % 3 == 0)
                    {
                        lock1.SetActive(false);
                        lock2.SetActive(true);
                        lock3.SetActive(true);
                        modeText1.SetActive(true);
                        modeText2.SetActive(false);
                        modeText3.SetActive(false);
                        generateBut.SetActive(true);
                    }
                    if (PlayerPrefs.GetInt("Levels") / 3 < levelIndex - 1) {
                        lock1.SetActive(true);
                        lock2.SetActive(true);
                        lock3.SetActive(true);
                        modeText1.SetActive(false);
                        modeText2.SetActive(false);
                        modeText3.SetActive(false);
                        generateBut.SetActive(false);
                    }
                }
            }
            
            tutorialBut.SetActive(false);
            leftBut.SetActive(true);
            levelModes.SetActive(true);
        }
        if (levelIndex == 12)
        {
            rightBut.SetActive(false);
        }
        else {
            rightBut.SetActive(true);
        }
    }

    public void GenerateButton()
    {
        GameManager.menuMusicCan = false;
        SceneManager.LoadScene("SampleScene");
        
    }

    public void MusicBut()
    {
        GameManager.musicOn = !GameManager.musicOn;
    }

    public void BackBut() {
        GameManager.destroyMusicFlag = true;
        SceneManager.LoadScene("MenuScene");
    }

    public void LeftBut() {
        if (levelIndex > 0)
        {
            levelIndex -= 1;
            if (PlayerPrefs.GetInt("Levels") / 3 > levelIndex - 1)
            {
                GameManager.levelMode = 3;
            }
            else {
                GameManager.levelMode = PlayerPrefs.GetInt("Levels") % 3 + 1;
            }
        }
    }

    public void RightBut() {
        if (levelIndex < 12) {
            levelIndex += 1;
        }
        if (PlayerPrefs.GetInt("Levels") / 3 > levelIndex - 1)
        {
            GameManager.levelMode = 3;
        }
        else
        {
            GameManager.levelMode = PlayerPrefs.GetInt("Levels") % 3 + 1;
        }
    }

    public void TutorBut() {
        SceneManager.LoadScene("TutorScene");
    }

    public void ModeBut_1() {
        if (lock1.active == false) {
            GameManager.levelMode = 1;
        }
        
    }

    public void ModeBut_2()
    {
        if (lock2.active == false)
        {
            GameManager.levelMode = 2;
        }
    }

    public void ModeBut_3()
    {
        if (lock3.active == false)
        {
            GameManager.levelMode = 3;
        }
    }
}
