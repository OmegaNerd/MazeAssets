using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLazer : MonoBehaviour
{
    [SerializeField]private float timeToStart;
    [SerializeField] private float timeToStop;
    private float timeToDestroy;
    private float timeCurrent = 0;
    [SerializeField] private float speed = 2f;
    public GameObject lazer;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.lazerSoundFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            timeCurrent += 1 * Time.deltaTime;
            if (timeCurrent < timeToStart) {
                if (transform.position.y < 2)
                {
                    transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
                }
                else {
                    transform.position = new Vector3(transform.position.x, 2, transform.position.z);
                }
                lazer.SetActive(false);
            }
            if (timeCurrent >= timeToStart && timeCurrent < timeToStop) {
                lazer.SetActive(true);
            }
            if (timeCurrent >= timeToStop) {
                lazer.SetActive(false);
                if (transform.position.y > 0)
                {
                    transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
