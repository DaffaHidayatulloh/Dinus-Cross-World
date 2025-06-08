using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName;
    public void Start()
    {
        AudioManager.instance.PlayBGM();
    }
    public void StartGame()
    {
        AudioManager.instance.StopBGM();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("SceneToLoad", sceneName); // Simpan nama scene tujuan
        PlayerPrefs.Save();

        SceneManager.LoadScene("Loading"); // Panggil scene loading
    }
    public void ExitGame ()
    {
        Debug.Log ("Keluar");
        Application.Quit();
    }
}
