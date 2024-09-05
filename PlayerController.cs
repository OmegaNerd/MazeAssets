using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveX;
    private float moveZ;
    private Rigidbody rb;
    [SerializeField] private float speed;
    public FloatingJoystick joystick;
    [SerializeField] private AudioSource audioSource;
    public GameObject wheel;
    public Animator animator;
    [SerializeField] private bool showFps = false;
    [SerializeField] private TMP_Text fpsText;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
        GameManager.playerController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game)
        {
            if (showFps == true) {
                float fps = 1 / Time.deltaTime;
                fpsText.text = fps.ToString();
            }
        }
        else {
            if (audioSource.isPlaying == true)
            {
                audioSource.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.state == GameState.Game)
        {
            MoveSetter();
            MoveController();
        }
    }

    public void MoveController() {
        if (moveX != 0 || moveZ != 0)
        {
            animator.SetBool("Move", true);
            wheel.transform.Rotate(0,0, -250 * Time.deltaTime);
            if (Vector3.AngleBetween(transform.right, new Vector3(moveX, 0, moveZ)) < 2.75f)
            {
                transform.right = Vector3.Lerp(transform.right, new Vector3(moveX, 0, moveZ), 0.1f);
            }
            else
            {
                transform.right = new Vector3(moveX, 0, moveZ);
            }
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
                
            }
            else {
                if (audioSource.volume < 0.75f)
                {
                    audioSource.volume += 20 * Time.deltaTime;
                }
                else
                {
                    audioSource.volume = 0.75f;
                }
            }
        }
        else {
            animator.SetBool("Move", false);
            if (audioSource.isPlaying == true)
            {
                if (audioSource.volume > 0)
                {
                    audioSource.volume -= 20 * Time.deltaTime;
                }
                else
                {
                    audioSource.volume = 0;
                    audioSource.Stop();
                }
                
            }
        }
        //transform.position += new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime ;
        rb.velocity = new Vector3(moveX, 0, moveZ) * speed;
        
    }

    public void MoveSetter() {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if (Mathf.Abs(joystick.Horizontal) <= 1)
            {
                moveX = joystick.Horizontal;
            }
            else {
                if (joystick.Horizontal < 0)
                {
                    moveX = -1;
                }
                else {
                    moveX = 1;
                }
            }
            if (Mathf.Abs(joystick.Vertical) <= 1)
            {
                moveZ = joystick.Vertical;
            }
            else
            {
                if (joystick.Vertical < 0)
                {
                    moveZ = -1;
                }
                else
                {
                    moveZ = 1;
                }
            }
        }
        else {
            if (Input.GetKey(KeyCode.D) || (joystick.Horizontal > 0.25f && moveZ == 0) || (joystick.Horizontal > 0.5f && moveZ != 0))
            {
                moveX = 1;
            }
            else if (Input.GetKey(KeyCode.A) || (joystick.Horizontal < -0.25f && moveZ == 0) || (joystick.Horizontal < -0.5f && moveZ != 0))
            {
                moveX = -1;
            }
            else
            {
                moveX = 0;
            }
            if (Input.GetKey(KeyCode.W) || (joystick.Vertical > 0.25f && moveX == 0) || (joystick.Vertical > 0.5f && moveX != 0))
            {
                moveZ = 1;
            }
            else if (Input.GetKey(KeyCode.S) || (joystick.Vertical < -0.25f && moveX == 0) || (joystick.Vertical < -0.5f && moveX != 0))
            {
                moveZ = -1;
            }
            else
            {
                moveZ = 0;
            }
        }
        
    }
}
