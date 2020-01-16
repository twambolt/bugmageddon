using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float stompSpeed = 11f;
    public float playerheight = 7.24f;
    //private float horizontalInput;
    //private float verticalInput;
    //private float stompInput;

    private bool isStomping = false;

    public Rigidbody rgdb;

    public CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleStomp();
        HandleMovement();
    }

    /// <summary>
    /// Handles the player's stomp ability
    /// </summary>
    private void HandleStomp() {
        if (Input.GetKeyDown(KeyCode.Space) && !isStomping)
        {
            isStomping = true;
            charController.Move(Vector3.down * stompSpeed);
        }
        ResetStomp();
        isStomping = false;
    }



    /// <summary>
    /// Handles the player movement on the screen and input from the arrow keys
    /// </summary>
    private void HandleMovement()
    {
        if (!isStomping)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            charController.Move(move * movementSpeed * Time.deltaTime);
        }
    }

    private void ResetStomp()
    {
        while (isStomping) {

            if (transform.position.y >= playerheight) {
                isStomping = false;
                break;
            }
            charController.Move(Vector3.up * 0.1f);
        }
    }

}
