using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextController : MonoBehaviour
    {
        public Text uiText; // assign di Inspector ke UI Text

        public void ShowText(string message)
        {
            uiText.text = message;
            uiText.gameObject.SetActive(true);
            CancelInvoke(nameof(HideText));
            Invoke(nameof(HideText), 2f); // sembunyikan setelah 3 detik
        }

        private void HideText()
        {
            uiText.gameObject.SetActive(false);
        }
    }

