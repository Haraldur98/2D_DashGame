using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGDDPlatformer
{
    public class ControlStation : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public AudioSource activateSound;
        public AudioSource switchSound;
        public List<Transform> controlBlocks;
        public int currentBlockIndex = 0;

        public GameObject activeIndicator;

        private bool isActivated = false;

        void Start()
        {
            Transform controlBlockParent = transform.Find("ControlBlocks");
            controlBlocks = new List<Transform>();
            foreach (Transform child in controlBlockParent)
            {
                controlBlocks.Add(child);
            }
            activeIndicator.SetActive(false);
        }

        void Update()
        {
            if (isActivated)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {
                    SwitchControlBlock();
                }

                if (controlBlocks.Count > 0)
                {
                    MoveCurrentControlBlock();
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController playerController) && !playerController.isGrounded)
            {
                playerController.toggleControl();
                isActivated = true;
                var currentBlock = controlBlocks[currentBlockIndex].GetComponent<ControlBlock>();
                currentBlock.ActivateIndicator();
                activateSound.Play();
                activeIndicator.SetActive(true);


            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                if (isActivated)
                {
                    playerController.toggleControl();
                    isActivated = false;
                    DeactivateCurrentControlBlock();
                    activateSound.Play();
                    activeIndicator.SetActive(false);
                }
            }
        }

        private void SwitchControlBlock()
        {
            var currentBlock = controlBlocks[currentBlockIndex].GetComponent<ControlBlock>();
            currentBlock.DeactivateIndicator();
            currentBlockIndex = (currentBlockIndex + 1) % controlBlocks.Count;
            currentBlock = controlBlocks[currentBlockIndex].GetComponent<ControlBlock>();
            currentBlock.ActivateIndicator();
            switchSound.Play();
        }

        private void MoveCurrentControlBlock()
        {
            var currentBlockTransform = controlBlocks[currentBlockIndex];
            var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            currentBlockTransform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        private void DeactivateCurrentControlBlock()
        {
            var currentBlock = controlBlocks[currentBlockIndex].GetComponent<ControlBlock>();
            currentBlock.DeactivateIndicator();
        }
    }
}