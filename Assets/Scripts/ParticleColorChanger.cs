using UnityEngine;

namespace AGDDPlatformer
{
    public class ParticleColorChanger : MonoBehaviour
    {
        public GameObject player;
        private ParticleSystem particleSystem;

        void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            ChangeMaterialColorToPlayerColor();
        }

        void ChangeMaterialColorToPlayerColor()
        {
            // Get the Particle System Renderer
            var particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();

            // Get the Material of the Particle System
            var particleMaterial = particleRenderer.material;

            // Change the Main Color to the Player's Color
            particleMaterial.SetColor("_Color", player.GetComponent<SpriteRenderer>().color);
        }
    }
}