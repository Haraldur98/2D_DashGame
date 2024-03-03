using System.Collections;
using UnityEngine;

namespace AGDDPlatformer
{
    public class PulsingObject : MonoBehaviour
    {
        private float pulseSpeed = 6f; // Speed of the pulse
        private float pulseMagnitude = 0.2f; // Magnitude of the pulse
        public float shockwaveDuration = 1f; // Duration of the shockwave
        private PlayerController player1; // Reference to the player script
        private bool isShockwaveActive = false; // Whether a shockwave is currently active
        private float knockbackSpeed = 20f; // Speed to apply to the player when hit


        void Start()
        {
            // Find the player object and get the Player script
            GameObject playerObject = GameObject.FindWithTag("Player1");
            if (playerObject != null)
            {
                player1 = playerObject.GetComponent<PlayerController>();
            }
            else
            {
                Debug.LogError("Player1 object not found");
            }
        }

        void Update()
        {
            // Calculate the pulse
            float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseMagnitude * (isShockwaveActive ? 2 : 1);

            // Apply the pulse
            transform.localScale = Vector3.one + Vector3.one * pulse;
        }

        void OnTriggerEnter2D(Collider2D col)
        { 
            if (col.gameObject.tag == "Player1")
            {   
                // Check the player's properties
                if (player1.ForceActive == true && player1.isDashing == true)
                {
                    Debug.Log("Player collided with explodable object");
                    // player1.source.PlayOneShot(player1.brakeSound);
                }
                else
                {
                    // Determine the horizontal direction based on the relative positions of the player and the enemy
                    float horizontalDirection = (player1.transform.position.x > transform.position.x) ? 1 : -1;

                    // Apply a velocity to the player in the direction away from the enemy
                    Vector2 knockbackDirection = new Vector2(horizontalDirection, 1).normalized;
                    player1.Knockback(knockbackDirection, knockbackSpeed);
                }
            }
        }

        public void Shockwave()
        {
            // Start the shockwave coroutine
            StartCoroutine(ShockwaveCoroutine());
        }

        private IEnumerator ShockwaveCoroutine()
        {
            // Activate the shockwave
            isShockwaveActive = true;

            // Wait for the duration of the shockwave
            yield return new WaitForSeconds(shockwaveDuration);

            // Deactivate the shockwave
            isShockwaveActive = false;
        }
    }
}