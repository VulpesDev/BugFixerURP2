using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeneManagement : MonoBehaviour
{
    static bool created = false;
    public static bool hard = false;

    [SerializeField]Slider sensitivitySlider;
    [SerializeField]Slider volumeSlider;
    public static float sensitivity;
    public static float volume;

    AudioMixer mixer;
    private void Awake()
    {
        mixer = Resources.Load("Sounds/Master") as AudioMixer;

        Cursor.lockState = CursorLockMode.None;
        hard = false;
        if (!created)
        DontDestroyOnLoad(gameObject);
        created = true;
    }
    private void Start()
    {
        GetValues();
        ChangeValueVolume();
    }
    public void GetValues()
    {
        sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        volume = PlayerPrefs.GetFloat("Volume");
        sensitivitySlider.value = sensitivity;
        volumeSlider.value = volume;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }
    public void ChangeValueSensitivity()
    {
        sensitivity = sensitivitySlider.value;
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }
    public void ChangeValueVolume()
    {
        volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void MakeItHardDaddy()
    {
        hard = true;
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
    public void Quit()
    {
        Application.Quit();
    }
}
