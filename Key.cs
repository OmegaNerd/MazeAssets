using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Player") == true) {
            if (used == false) {
                used = true;
                GameManager.keysCurrent += 1;
                GameManager.keySoundFlag = true;
            }
            Destroy(this.gameObject);
        }
    }
}
