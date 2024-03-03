using UnityEngine;

namespace AGDDPlatformer
{
    public class FollowingEye : MonoBehaviour
    {
        public Transform player; // Reference to the player's transform

        void Update()
        {
            // Calculate the direction vector from the eye to the player
            Vector2 direction = player.position - transform.position;

            // Calculate the angle in degrees for this direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // Subtract 90 to adjust for the sprite's default orientation

            // Apply the rotation to the eye
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}