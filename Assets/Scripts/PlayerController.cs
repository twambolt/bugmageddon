using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float stompSpeed = 11f;
    private float playerheight = 7.24f; // height where the player should be
    private float groundHeight = 0.5f; // height of the floor

    public AudioSource playerAudio;
    public AudioClip stompSound;
    public AudioClip bugStompSound;
    public AudioClip powUpSound;

    private bool isStomping = false;

    private Rigidbody rgbd;
    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        playerAudio = GetComponent<AudioSource>();
        rgbd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        SignalStomp();
        Stomp();
        ResetStomp();
    }

    /// <summary>
    /// Signals the player's stomp ability
    /// </summary>
    private void SignalStomp() {
        if (Input.GetKeyDown(KeyCode.Space) && !isStomping)
        {
            isStomping = true;
            playerAudio.PlayOneShot(stompSound, 1.0f);
        }
    }

    /// <summary>
    /// Handles the player's downwards stomp movement
    /// </summary>
    private void Stomp()
    {
        if (transform.position.y >= groundHeight && isStomping)
        {
            transform.Translate(Vector3.down * stompSpeed * Time.deltaTime);
        }
        if (transform.position.y < groundHeight) {
            isStomping = false;
            //Debug.Log("Going up");
        }
    }


    /// <summary>
    /// Handles the player's upwards stomp movement
    /// </summary>
    private void ResetStomp() {
        if (transform.position.y <= playerheight && !isStomping) {
            transform.Translate(Vector3.up * stompSpeed * Time.deltaTime);
        }
        // definetly needed help here^^^
    }


    /// <summary>
    /// Handles the player movement on the screen and input from the arrow keys
    ///
    /// Citing this
    /// https://answers.unity.com/questions/13144/player-movement-boundaries.html
    /// 
    /// </summary>
    private void HandleMovement()
    {
        Vector3 pos = transform.position;
        if (!isStomping)
        {
            float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime; 
            float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

           // Vector3 move = new Vector3(horizontal, 0, vertical);
            //charController.Move(move * movementSpeed * Time.deltaTime);
        

            pos.x = Mathf.Clamp(pos.x + horizontal, GameSettings.xMin, GameSettings.xMax);
            pos.z = Mathf.Clamp(pos.z + vertical, GameSettings.zMin, GameSettings.zMax);
            transform.position = pos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bug"))
        {
            playerAudio.PlayOneShot(bugStompSound, 1.0f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            playerAudio.PlayOneShot(powUpSound, 1.0f);
            Destroy(collision.gameObject);
        }
    }
}
