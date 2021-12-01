using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeneManagement : MonoBehaviour
{
    static bool created = false;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        if (!created)
        DontDestroyOnLoad(gameObject);
        created = true;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(
            SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/MainMenu.unity"));
    }
    public void LoadServerLevel()
    {
        SceneManager.LoadScene(
            SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Server_Level.unity"));
    }
    public void LoadTutorialLevel()
    {
        SceneManager.LoadScene(
            SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Tutorial_Server.unity"));
    }
}
