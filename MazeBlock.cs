using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBlock : MonoBehaviour
{
    public int index;
    public int indexX;
    public int indexZ;
    public bool activated = false;
    public bool left;
    public bool right;
    public bool up;
    public bool down;
    public bool leftOpen;
    public bool rightOpen;
    public bool upOpen;
    public bool downOpen;
    public GameObject activeObj;
    private List<int> sides = new List<int>();
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject upWall;
    public GameObject downWall;
    public GameObject closingWall;
    public GameObject deathFloor;
    public GameObject lazer;
    public bool obs1 = false;
    public bool obs2 = false;
    public bool finalBlock = false;
    public int lazerLenght = 0;
    public int lazerMaxLenght;
    private int obsIndex = 0;
    public bool endBlock = false;
    public Lazer lazerScript;
    [SerializeField] private float wallsShowSpeed;
    [SerializeField] private float startWallsDowning;
    public float mazeHeight;
    private bool wallsPosed = false;
    private float timeCurrent = 0;
    private float timeToShowObs = 1.5f;
    [SerializeField] private bool startBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        if (finalBlock == true) {
            //leftOpen = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (activated == true)
        {
            activeObj.SetActive(true);
        }
        else { 
            activeObj.SetActive(false);
        }*/
        if (GameManager.state == GameState.Game) {
            if (wallsPosed == false) {
                wallsPosed = true;

                if (index >= mazeHeight)
                {
                    upWall.transform.localPosition = new Vector3(upWall.transform.localPosition.x, startWallsDowning, upWall.transform.localPosition.z);
                }
                if (index < mazeHeight * (mazeHeight - 1)) {

                    downWall.transform.localPosition = new Vector3(downWall.transform.localPosition.x, startWallsDowning, downWall.transform.localPosition.z);
                }
                if (index % mazeHeight != 0 || index == 0) {
                    leftWall.transform.localPosition = new Vector3(leftWall.transform.localPosition.x, startWallsDowning, leftWall.transform.localPosition.z);
                }
                if (index % mazeHeight != mazeHeight - 1) {

                    rightWall.transform.localPosition = new Vector3(rightWall.transform.localPosition.x, startWallsDowning, rightWall.transform.localPosition.z);
                }
            }
            if (upWall.transform.localPosition.y < 0)
            {
                upWall.transform.localPosition += new Vector3(0, wallsShowSpeed, 0) * Time.deltaTime;
            }
            else {
                upWall.transform.localPosition = new Vector3(upWall.transform.localPosition.x, 0, upWall.transform.localPosition.z);
            }
            if (downWall.transform.localPosition.y < 0)
            {

                downWall.transform.localPosition += new Vector3(0, wallsShowSpeed, 0) * Time.deltaTime;
            }
            else {

                downWall.transform.localPosition = new Vector3(downWall.transform.localPosition.x, 0, downWall.transform.localPosition.z);
            }
            if (leftWall.transform.localPosition.y < 0)
            {

                leftWall.transform.localPosition += new Vector3(0, wallsShowSpeed, 0) * Time.deltaTime;
            }
            else {

                leftWall.transform.localPosition = new Vector3(leftWall.transform.localPosition.x, 0, leftWall.transform.localPosition.z);
            }
            if (rightWall.transform.localPosition.y < 0)
            {

                rightWall.transform.localPosition += new Vector3(0, wallsShowSpeed, 0) * Time.deltaTime;
            }
            else {

                rightWall.transform.localPosition = new Vector3(rightWall.transform.localPosition.x, 0, rightWall.transform.localPosition.z);
            }
            /*
            if (leftOpen == true) {
                if (leftWall.transform.localPosition.y > -1.5f)
                {
                    leftWall.transform.localPosition += new Vector3(0, -wallsShowSpeed, 0) * Time.deltaTime;
                }
                else {
                    leftWall.SetActive(false);
                }
            }
            if (rightOpen == true)
            {
                if (rightWall.transform.localPosition.y > -1.5f)
                {
                    rightWall.transform.localPosition += new Vector3(0, -wallsShowSpeed, 0) * Time.deltaTime;
                }
                else
                {
                    rightWall.SetActive(false);
                }
            }
            if (upOpen == true)
            {
                if (upWall.transform.localPosition.y > -1.5f)
                {
                    upWall.transform.localPosition += new Vector3(0, -wallsShowSpeed, 0) * Time.deltaTime;
                }
                else
                {
                    upWall.SetActive(false);
                }
            }
            if (downOpen == true)
            {
                if (downWall.transform.localPosition.y > -1.5f)
                {
                    downWall.transform.localPosition += new Vector3(0, -wallsShowSpeed, 0) * Time.deltaTime;
                }
                else
                {
                    downWall.SetActive(false);
                }
            }
            */
            leftWall.SetActive(!leftOpen);
            rightWall.SetActive(!rightOpen);
            upWall.SetActive(!upOpen);
            downWall.SetActive(!downOpen);
            if (timeCurrent >= timeToShowObs)
            {
                if (startBlock == true) {
                    rightOpen = true;
                }
                if (obs1 == true)
                {
                    if (((upOpen == false && downOpen == false) || (leftOpen == false && rightOpen == false)) && (endBlock == false))
                    {
                        obsIndex = 1;
                    }
                    else
                    {
                        obsIndex = 2;
                    }
                }
                else
                {
                    if (obs2 == true)
                    {
                        obsIndex = 3;
                    }
                }
                if (finalBlock == false)
                {
                    switch (obsIndex)
                    {
                        case 0:
                            closingWall.SetActive(false);
                            deathFloor.SetActive(false);
                            lazer.SetActive(false);
                            break;
                        case 1:
                            closingWall.SetActive(true);
                            deathFloor.SetActive(false);
                            lazer.SetActive(false);
                            if (upOpen == true || downOpen == true)
                            {
                                closingWall.transform.localRotation = Quaternion.Euler(0, 90, 0);
                            }
                            else
                            {
                                closingWall.transform.localRotation = Quaternion.Euler(0, 0, 0);
                            }
                            break;
                        case 2:
                            closingWall.SetActive(false);
                            deathFloor.SetActive(true);
                            lazer.SetActive(false);
                            break;
                        case 3:
                            lazerScript.lenght = lazerLenght;
                            lazerScript.maxLenght = lazerMaxLenght;
                            closingWall.SetActive(false);
                            deathFloor.SetActive(false);
                            lazer.SetActive(true);
                            if (upOpen == false && downOpen == true)
                            {
                                lazer.transform.localRotation = Quaternion.Euler(0, 270, 0);
                            }
                            else
                            {
                                if (upOpen == true && downOpen == false)
                                {
                                    lazer.transform.localRotation = Quaternion.Euler(0, 90, 0);
                                }
                                else
                                {
                                    if (rightOpen == true && leftOpen == false)
                                    {
                                        lazer.transform.localRotation = Quaternion.Euler(0, 180, 0);
                                    }
                                    else
                                    {
                                        lazer.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            else {
                timeCurrent += 1 * Time.deltaTime;
            }
        }
    }

    public int ChooseRandomSide() {
        sides.Clear();
        if (left == true) {
            sides.Add(1);
        }
        if (up == true)
        {
            sides.Add(2);
        }
        if (right == true)
        {
            sides.Add(3);
        }
        if (down == true)
        {
            sides.Add(4);
        }
        int newSide = sides[Random.Range(0, sides.Count)];
        return newSide;
    }
}
