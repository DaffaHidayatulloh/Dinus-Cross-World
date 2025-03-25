using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName; 
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); 
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame ()
    {
        Debug.Log ("Keluar");
        Application.Quit();
    }
}
