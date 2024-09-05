using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            transform.position += transform.forward * speed * Time.deltaTime;
            if (transform.position.x < -50 || transform.position.x > 150 || transform.position.z > 50 || transform.position.z < -150) {
                Destroy(this.gameObject);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("Player") == true && used == false) {
            used = true;
            GameManager.UpdateState(GameState.Lose);
            Destroy(this.gameObject);
        }
    }
}
