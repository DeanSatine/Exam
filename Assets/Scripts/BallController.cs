using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public float playerSpeed = 2.0f;
    private float jumpHeight = 0f;
    public  float gravityValue = -10f;

    private bool isGameWon = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isGameWon)
        {
            groundedPlayer = characterController.isGrounded;

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.Move(move * Time.deltaTime * playerSpeed);

            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            characterController.Move(playerVelocity * Time.deltaTime);

            // Check if player falls below a certain y-coordinate
            if (transform.position.y < -10f) // Adjust this value as needed
            {
                DieAndRespawn();
            }
        }
    }

    void DieAndRespawn()
    {
        GameManager.Instance.PlayerFell();
        // Reset the player's position to the starting point or respawn point
        transform.position = GameManager.Instance.GetRespawnPoint();
    }

    public void SetGameWon()
    {
        isGameWon = true;
        // Add any additional logic you might need when the game is won
    }
}
