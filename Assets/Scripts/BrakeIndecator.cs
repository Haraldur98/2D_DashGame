using UnityEngine;

namespace AGDDPlatformer
{
    public class BrakeIndecator : MonoBehaviour
    {
        public Color brakeActiveColor = Color.red; // Color when brake is active
        private Color originalColor; // Original color of the sprite
        private SpriteRenderer[] spriteRenderers; // Array of all sprite renderers in this object and its children
        private PlayerController player1; // Reference to the player script

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

            // Get all sprite renderers in this object and its children
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            // Store the original color of the first sprite renderer
            if (spriteRenderers.Length > 0)
            {
                originalColor = spriteRenderers[0].color;
            }
        }

        void Update()
        {
            // Change the color of the sprite and all its children based on the ForceActive state
            Color targetColor = player1.ForceActive ? brakeActiveColor : originalColor;
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = targetColor;
            }
        }
    }
}