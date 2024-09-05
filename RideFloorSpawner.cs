using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RideFloorSpawner : MonoBehaviour
{
    [SerializeField] private int cornerIndex;
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private GameObject rideFloor;
    [SerializeField] private GameObject rideFloor2;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game) {
            if (spawned == false) {
                spawned = true;
                switch (cornerIndex)
                {
                    case 1:
                        for (int i = 0; i < mazeGenerator.height + 2; i++)
                        {
                            Instantiate(rideFloor, transform.position + new Vector3(0, 0, -i * mazeGenerator.blockSize), Quaternion.Euler(0, -270, 0));
                        }
                        Instantiate(rideFloor2, transform.position + new Vector3(0, 0, -(mazeGenerator.height + 1) * mazeGenerator.blockSize), Quaternion.Euler(0, -270, 0));
                        break;
                    case 2:
                        for (int i = 0; i < mazeGenerator.width + 3; i++)
                        {
                            Instantiate(rideFloor, transform.position + new Vector3(i * mazeGenerator.blockSize, 0, 0), new Quaternion());
                        }
                        Instantiate(rideFloor2, transform.position + new Vector3((mazeGenerator.width + 2.37f) * mazeGenerator.blockSize, 0, 0), new Quaternion());
                        break;
                    case 3:
                        for (int i = 0; i < mazeGenerator.height + 2; i++)
                        {
                            Instantiate(rideFloor, transform.position + new Vector3(0, 0, i * mazeGenerator.blockSize), Quaternion.Euler(0, -90, 0));
                        }
                        Instantiate(rideFloor2, transform.position + new Vector3(0, 0, (mazeGenerator.height + 1) * mazeGenerator.blockSize), Quaternion.Euler(0, -90, 0));
                        break;
                    case 4:
                        for (int i = 0; i < mazeGenerator.width + 3; i++)
                        {
                            Instantiate(rideFloor, transform.position + new Vector3(-i * mazeGenerator.blockSize, 0, 0), new Quaternion(0, -180, 0, 0));
                        }
                        Instantiate(rideFloor2, transform.position + new Vector3(-(mazeGenerator.width + 2.37f) * mazeGenerator.blockSize, 0, 0), new Quaternion(0, -180, 0, 0));
                        break;
                }
            }
        }
    }
}
