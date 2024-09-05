using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{

    private bool used = false;
    [SerializeField] private bool tutor = false;
    [SerializeField] private int tutorNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Player") == true)
        {
            if (used == false)
            {
                used = true;
                if (tutor == true) {
                    if (tutorNumber > PlayerPrefs.GetInt("Tutors")) {
                        PlayerPrefs.SetInt("Tutors", tutorNumber);
                    }
                }
                GameManager.UpdateState(GameState.Win);
            }
        }
    }
}
