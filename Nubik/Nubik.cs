using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nubik : MonoBehaviour
{
    public GameObject rotatingPart;
    public float shootDelay;
    private float timeCurrent = 0;
    public GameObject arrow;
    public GameObject arrowPos;
    public float speed;
    private int sideIndex = 1;
    public GameObject side1Pos;
    public GameObject side2Pos;
    public GameObject side3Pos;
    public GameObject side4Pos;
    public MazeGenerator mazeGenerator;
    public GameObject rideFloorSpawner1;
    public GameObject rideFloorSpawner2;
    public GameObject rideFloorSpawner3;
    public GameObject rideFloorSpawner4;
    public AudioSource audioSource;
    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        rideFloorSpawner1.transform.position = new Vector3(-5f, 0, 2.5f);
        rideFloorSpawner2.transform.position = new Vector3(-5f, 0, -(mazeGenerator.height * mazeGenerator.blockSize + 5));
        rideFloorSpawner3.transform.position = new Vector3(mazeGenerator.width * mazeGenerator.blockSize + 8f, 0, -(mazeGenerator.height * mazeGenerator.blockSize + 5));
        rideFloorSpawner4.transform.position = new Vector3(mazeGenerator.width * mazeGenerator.blockSize + 8f, 0, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            
            side1Pos.transform.position = new Vector3(-4.95f, 0, -(mazeGenerator.height * mazeGenerator.blockSize / 2 + 5));
            side2Pos.transform.position = new Vector3(mazeGenerator.width * mazeGenerator.blockSize / 2, 0, 2f);
            side3Pos.transform.position = new Vector3(mazeGenerator.width * mazeGenerator.blockSize + 8.1f, 0, -(mazeGenerator.height * mazeGenerator.blockSize / 2 + 5));
            side4Pos.transform.position = new Vector3(mazeGenerator.width * mazeGenerator.blockSize / 2, 0, -(mazeGenerator.height * mazeGenerator.blockSize + 5.5f));
            rotatingPart.transform.forward = Vector3.Lerp(rotatingPart.transform.forward.normalized, new Vector3(GameManager.playerController.transform.position.x - transform.position.x, 0, GameManager.playerController.transform.position.z - transform.position.z).normalized, 0.5f);
            timeCurrent += 1 * Time.deltaTime;
            if (timeCurrent >= shootDelay) {
                Shoot();
                timeCurrent = 0;
                
            }
            transform.position += transform.forward.normalized * speed * Time.deltaTime;
            switch (sideIndex) {
                case 1:
                    transform.forward = Vector3.Lerp(transform.forward, new Vector3(0, 0, -1), 0.1f);
                    transform.position = new Vector3(side1Pos.transform.position.x, transform.position.y, transform.position.z);
                    if (transform.position.z <= side4Pos.transform.position.z) {
                        sideIndex = 4;
                    }
                    break;
                case 2:
                    transform.forward = Vector3.Lerp(transform.forward, new Vector3(-1, 0, 0), 0.1f);
                    transform.position = new Vector3(transform.position.x, transform.position.y, side2Pos.transform.position.z);
                    if (transform.position.x <= side1Pos.transform.position.x)
                    {
                        sideIndex = 1;
                    }
                    break;
                case 3:
                    transform.forward = Vector3.Lerp(transform.forward, new Vector3(0, 0, 1), 0.1f);
                    transform.position = new Vector3(side3Pos.transform.position.x, transform.position.y, transform.position.z);
                    if (transform.position.z >= side2Pos.transform.position.z)
                    {
                        sideIndex = 2;
                    }
                    break;
                case 4:
                    transform.forward = Vector3.Lerp(transform.forward, new Vector3(1, 0, 0), 0.1f);
                    transform.position = new Vector3(transform.position.x, transform.position.y, side4Pos.transform.position.z);
                    if (transform.position.x >= side3Pos.transform.position.x)
                    {
                        sideIndex = 3;
                    }
                    break;
            }
        }
    }

    public void Shoot() {
        audioSource.PlayOneShot(shootSound, 0.75f);
        Instantiate(arrow, arrowPos.transform.position, arrowPos.transform.rotation);
    }
}
