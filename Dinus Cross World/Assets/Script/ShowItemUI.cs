using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItemUI : MonoBehaviour
{
    public GameObject penggarisUI; // GameObject UI untuk penggaris
    public GameObject TahuKuningUI;
    public GameObject KunciUI;

    void Start()
    {
        // Cek apakah player memiliki item "penggaris"
        if (PlayerPrefs.GetInt("HasRuler", 0) == 1)
        {
            penggarisUI.SetActive(true); // Aktifkan UI
        }
        else
        {
            penggarisUI.SetActive(false); // Sembunyikan jika tidak punya
        }

        if (PlayerPrefs.GetInt("HasTahu", 0) == 1)
        {
            TahuKuningUI.SetActive(true); // Aktifkan UI
        }
        else
        {
            TahuKuningUI.SetActive(false); // Sembunyikan jika tidak punya
        }

        if (PlayerPrefs.GetInt("HasKey", 0) == 1)
        {
            KunciUI.SetActive(true); // Aktifkan UI
        }
        else
        {
            KunciUI.SetActive(false); // Sembunyikan jika tidak punya
        }
    }

    void Update()
    {
        // Cek apakah player memiliki item "penggaris"
        if (PlayerPrefs.GetInt("HasRuler", 0) == 1)
        {
            penggarisUI.SetActive(true); // Aktifkan UI
        }
        else
        {
            penggarisUI.SetActive(false); // Sembunyikan jika tidak punya
        }

        if (PlayerPrefs.GetInt("HasTahu", 0) == 1)
        {
            TahuKuningUI.SetActive(true); // Aktifkan UI
        }
        else
        {
            TahuKuningUI.SetActive(false); // Sembunyikan jika tidak punya
        }

        if (PlayerPrefs.GetInt("HasKey", 0) == 1)
        {
            KunciUI.SetActive(true); // Aktifkan UI
        }
        else
        {
            KunciUI.SetActive(false); // Sembunyikan jika tidak punya
        }
    }
}
