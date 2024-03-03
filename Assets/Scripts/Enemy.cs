using UnityEngine;

namespace AGDDPlatformer
{
    public class Enemy : MonoBehaviour
    {
        public GameObject player;
        public AudioSource source;
        public float speed = 2f;
        public float knockbackSpeed = 10f; // Speed to apply to the player when hit
        private Rigidbody2D rb;
        private PlayerController player1; // Reference to the player script
        private PulsingObject pulsingObject; // Reference to the PulsingObject script

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Disable gravity

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

            // Get the PulsingObject script from the child object
            pulsingObject = GetComponentInChildren<PulsingObject>();
        }

        void Update()
        {
            FollowPlayer();
        }

        void FollowPlayer()
        {
            Vector3 direction = player.transform.position - transform.position;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Move horizontally
                rb.velocity = new Vector2(speed * Mathf.Sign(direction.x), 0);
            }
            else
            {
                // Move vertically
                rb.velocity = new Vector2(0, speed * Mathf.Sign(direction.y));
            }
        }
    }
}