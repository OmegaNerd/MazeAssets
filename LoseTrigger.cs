using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    private bool used = false;
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
        if (other.name.StartsWith("Player") == true) {
            if (used == false) {
                used = true;
                GameManager.UpdateState(GameState.Lose);
            }
        }
    }
}
