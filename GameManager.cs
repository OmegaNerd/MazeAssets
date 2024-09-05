using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameState state;
    public static int keysNeed;
    public static int keysCurrent = 0;
    public static PlayerController playerController;
    public static int levelMode = 2;
    public static float CamSizeStart;
    public static bool musicOn = true;
    public static int currentLevelNumber = 0;
    public static int MazeSize = 4;
    public static bool lazerSoundFlag = false;
    public static bool keySoundFlag = false;
    public static bool destroyMusicFlag = false;
    public static bool menuMusicCan = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateState(GameState newState) { 
        state = newState;
        switch (newState) {
            case GameState.Game:
                Time.timeScale = 1;
                break;
            case GameState.Lose:
                Time.timeScale = 0;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.Win:
                Time.timeScale = 0;
                if (SceneManager.GetActiveScene().name.StartsWith("Tutor") == false) {
                    if (currentLevelNumber > PlayerPrefs.GetInt("Levels"))
                    {
                        PlayerPrefs.SetInt("Levels", currentLevelNumber);
                    }
                }
                
                break;
        }
    }
}

public enum GameState { 
    Game,
    Pause,
    Lose,
    Win
}