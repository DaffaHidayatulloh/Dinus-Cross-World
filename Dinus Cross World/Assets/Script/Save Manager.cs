using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerState(Vector2 position, bool isFacingRight)
    {
        string sceneKey = SceneManager.GetActiveScene().name + "_Player";
        PlayerPrefs.SetFloat(sceneKey + "_X", position.x);
        PlayerPrefs.SetFloat(sceneKey + "_Y", position.y);
        PlayerPrefs.SetInt(sceneKey + "_FacingRight", isFacingRight ? 1 : 0);
        PlayerPrefs.Save();
    }

    public (Vector2, bool) LoadPlayerState()
    {
        string sceneKey = SceneManager.GetActiveScene().name + "_Player";
        float x = PlayerPrefs.GetFloat(sceneKey + "_X", 0);
        float y = PlayerPrefs.GetFloat(sceneKey + "_Y", 0);
        bool isFacingRight = PlayerPrefs.GetInt(sceneKey + "_FacingRight", 1) == 1;
        return (new Vector2(x, y), isFacingRight);
    }
}

