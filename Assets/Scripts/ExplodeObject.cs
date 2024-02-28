using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGDDPlatformer
{
    public class ExplodeObject : MonoBehaviour
    {   
        private Explodable _explodable;
        private PlayerController player1; // Reference to the player script

        void Start()
        {
            _explodable = GetComponent<Explodable>();

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

        void OnCollisionEnter2D(Collision2D col)
        { 
            Debug.Log("Collision detected");
            Debug.Log(col.gameObject.tag);
            if (col.gameObject.tag == "Player1")
            {   
                // Check the player's properties
                if (player1.ForceActive == true && player1.isDashing == true)
                {
                    Debug.Log("Player collided with explodable object");
                    player1.source.PlayOneShot(player1.brakeSound);
                    _explodable.explode();
                    ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
                    ef.doExplosion(transform.position);

                }
            }
        }
    }
}