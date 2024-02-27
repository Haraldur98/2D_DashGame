using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGDDPlatformer
{
    public class ControlStation : MonoBehaviour
    {

        public float moveSpeed = 5f; // Speed at which the blocks move
        public List<Transform> controlBlocks; // List of control blocks
        public int currentBlockIndex = 0; // Index of the currently controlled block

        private bool isActivated = false;

        void Start()
        {
            // Get all child blocks
            Transform controlBlockParent = transform.Find("ControlBlocks");
            controlBlocks = new List<Transform>();
            foreach (Transform child in controlBlockParent)
            {
                controlBlocks.Add(child);
            }
        }

        void Update()
{
            // Switch control blocks
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                ControlBlock currentBlock = controlBlocks[currentBlockIndex].GetComponent<ControlBlock>();
                currentBlockIndex = (currentBlockIndex + 1) % controlBlocks.Count;
                currentBlock.DeactivateIndicator();
                currentBlock = controlBlocks[currentBlockIndex].GetComponent<ControlBlock>();
                currentBlock.ActivateIndicator();
            }

            // Move the current control block
            if (controlBlocks.Count > 0 && isActivated == true)
            {
                Transform currentBlockTransform = controlBlocks[currentBlockIndex];
                Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                currentBlockTransform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the player has collided with the control station
            PlayerController playerController = other.GetComponentInParent<PlayerController>();
            if (playerController != null && playerController.isGrounded == false)
            {
                // Set the player's controlling property to true
                playerController.toggleControl();
                isActivated = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            // Check if the player has left the control station
            PlayerController playerController = other.GetComponentInParent<PlayerController>();
            if (playerController != null && isActivated == true)
            {
                // Set the player's controlling property to false
                playerController.toggleControl();
                isActivated = false;
                ControlBlock currentBlock = controlBlocks[currentBlockIndex].GetComponent<ControlBlock>();
                currentBlock.DeactivateIndicator();
            }
        }
    }
}
