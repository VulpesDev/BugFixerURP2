using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeneManagement : MonoBehaviour
{
    static bool created = false;
    private void Awake()
    {
        if(!created)
        DontDestroyOnLoad(gameObject);
        created = true;
        Cursor.lockState = CursorLockMode.None;
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
}
