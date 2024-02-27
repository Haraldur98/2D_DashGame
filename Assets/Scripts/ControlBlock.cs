using UnityEngine;

namespace AGDDPlatformer
{
    public class ControlBlock : MonoBehaviour
    {
        public GameObject activeIndicator;

        void Start()
        {
            DeactivateIndicator();
        }

        public void ActivateIndicator()
        {
            activeIndicator.SetActive(true);
        }

        public void DeactivateIndicator()
        {
            activeIndicator.SetActive(false);
        }
    }
}