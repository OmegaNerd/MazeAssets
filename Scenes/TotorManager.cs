using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotorManager : MonoBehaviour
{
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;
    public GameObject lock5;
    public GameObject lock6;
    private int tutorNumber;
    public GameObject modeText1;
    public GameObject modeText2;
    public GameObject modeText3;
    public GameObject modeText4;
    public GameObject modeText5;
    public GameObject modeText6;
    public GameObject choose1;
    public GameObject choose2;
    public GameObject choose3;
    public GameObject choose4;
    public GameObject choose5;
    public GameObject choose6;
    public GameObject musicOffObject;
    // Start is called before the first frame update
    void Start()
    {
        tutorNumber = PlayerPrefs.GetInt("Tutors") + 1;
        if (tutorNumber > 6) {
            tutorNumber = 6;
        }
        GameManager.CamSizeStart = 1;
    }

    // Update is called once per frame
    void Update()
    {
        musicOffObject.SetActive(!GameManager.musicOn);
        switch (tutorNumber) {
            case 1:
                choose1.SetActive(true);
                choose2.SetActive(false);
                choose3.SetActive(false);
                choose4.SetActive(false);
                choose5.SetActive(false);
                choose6.SetActive(false);
                break;
            case 2:
                choose1.SetActive(false);
                choose2.SetActive(true);
                choose3.SetActive(false);
                choose4.SetActive(false);
                choose5.SetActive(false);
                choose6.SetActive(false);
                break;
            case 3:
                choose1.SetActive(false);
                choose2.SetActive(false);
                choose3.SetActive(true);
                choose4.SetActive(false);
                choose5.SetActive(false);
                choose6.SetActive(false);
                break;
            case 4:
                choose1.SetActive(false);
                choose2.SetActive(false);
                choose3.SetActive(false);
                choose4.SetActive(true);
                choose5.SetActive(false);
                choose6.SetActive(false);
                break;
            case 5:
                choose1.SetActive(false);
                choose2.SetActive(false);
                choose3.SetActive(false);
                choose4.SetActive(false);
                choose5.SetActive(true);
                choose6.SetActive(false);
                break;
            case 6:
                choose1.SetActive(false);
                choose2.SetActive(false);
                choose3.SetActive(false);
                choose4.SetActive(false);
                choose5.SetActive(false);
                choose6.SetActive(true);
                break;
        }
        if (PlayerPrefs.GetInt("Tutors") >= 0)
        {
            lock1.SetActive(false);
            modeText1.SetActive(true);
        }
        else {
            lock1.SetActive(true);
            modeText1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Tutors") >= 1)
        {
            lock2.SetActive(false);
            modeText2.SetActive(true);
        }
        else
        {
            lock2.SetActive(true);
            modeText2.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Tutors") >= 2)
        {
            lock3.SetActive(false);
            modeText3.SetActive(true);
        }
        else
        {
            lock3.SetActive(true);
            modeText3.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Tutors") >= 3)
        {
            lock4.SetActive(false);
            modeText4.SetActive(true);
        }
        else
        {
            lock4.SetActive(true);
            modeText4.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Tutors") >= 4)
        {
            lock5.SetActive(false);
            modeText5.SetActive(true);
        }
        else
        {
            lock5.SetActive(true);
            modeText5.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Tutors") >= 5)
        {
            lock6.SetActive(false);
            modeText6.SetActive(true);
        }
        else
        {
            lock6.SetActive(true);
            modeText6.SetActive(false);
        }
    }

    public void ModeBut_1()
    {
        if (lock1.active == false)
        {
            tutorNumber = 1;
        }

    }

    public void ModeBut_2()
    {
        if (lock2.active == false)
        {
            tutorNumber = 2;
        }
    }

    public void ModeBut_3()
    {
        if (lock3.active == false)
        {
            tutorNumber = 3;
        }
    }

    public void ModeBut_4()
    {
        if (lock3.active == false)
        {
            tutorNumber = 4;
        }
    }

    public void ModeBut_5()
    {
        if (lock3.active == false)
        {
            tutorNumber = 5;
        }
    }

    public void ModeBut_6()
    {
        if (lock3.active == false)
        {
            tutorNumber = 6;
        }
    }

    public void StartButton()
    {
        GameManager.menuMusicCan = false;
        switch (tutorNumber) {
            case 1:
                SceneManager.LoadScene("Tutor1Scene");
                break;
            case 2:
                SceneManager.LoadScene("Tutor2Scene");
                break;
            case 3:
                SceneManager.LoadScene("Tutor3Scene");
                break;
            case 4:
                SceneManager.LoadScene("Tutor4Scene");
                break;
            case 5:
                SceneManager.LoadScene("Tutor5Scene");
                break;
            case 6:
                SceneManager.LoadScene("Tutor6Scene");
                break;

        }
        

    }

    public void MusicBut()
    {
        GameManager.musicOn = !GameManager.musicOn;
    }

    public void BackBut()
    {
        SceneManager.LoadScene("LevelsScene");
    }
}
