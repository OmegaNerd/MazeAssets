using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDestroyer : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.destroyMusicFlag == true) { 
            GameManager.destroyMusicFlag = false;
            audioSource.Stop();
            Destroy(this.gameObject);
        }
        if (GameManager.musicOn == true && GameManager.menuMusicCan == true)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

        }
        else {
            if (audioSource.isPlaying == true)
            {
                audioSource.Stop();
            }
        }
    }
}
