using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAktivator : MonoBehaviour
    {
        public GameObject objectToActivate;
        public GameObject objectToDeactivate;
        public GameObject objectToDeactivate2;

        private bool hasPlayedSFX = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (objectToActivate != null)
                    objectToActivate.SetActive(true);

                if (objectToDeactivate != null)
                    objectToDeactivate.SetActive(false);

                if (objectToDeactivate2 != null)
                    objectToDeactivate2.SetActive(false);

                if (!hasPlayedSFX)
                {
                    AudioManager.instance.PlaySFX(2);
                    hasPlayedSFX = true;
                }
            }
        }
    }

