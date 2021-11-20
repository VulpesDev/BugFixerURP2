using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    static public Transform ambient, player, interactions;

    public static bool laser;

    public static MusicManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ambient = transform.GetChild(0);
        player = transform.GetChild(1);
        interactions = transform.GetChild(2);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            laser = !laser;
        }
    }

    #region Player

    static public void ShootPistol()
    {
        //GameObject Sound = new GameObject();
        //Sound.transform.parent = player;
        //AudioSource ASound = Sound.AddComponent<AudioSource>();
        //ASound.clip = laser==true ? Resources.Load("Sounds/Player/LaserShot") as AudioClip :
        //    Resources.Load("Sounds/Player/1911Colt") as AudioClip;
        ////ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        //ASound.pitch = Random.Range(0.9f, 1.2f);
        //ASound.Play();
        //Sound.AddComponent<AudiosDefault>();

        for (int i = 0; i <= 1; i++)
        {
            GameObject Sound = new GameObject();
            Sound.transform.parent = player;
            AudioSource ASound = Sound.AddComponent<AudioSource>();
            ASound.clip = i == 0 ? Resources.Load("Sounds/Player/LaserShot") as AudioClip :
                Resources.Load("Sounds/Player/1911Colt") as AudioClip;
            //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
            ASound.pitch = Random.Range(0.9f, 1.2f);
            ASound.Play();
            Sound.AddComponent<AudiosDefault>();
        }
    }

    #endregion

    #region Enviroment
    static public void TypeUI()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = interactions;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Enviroment/UI/BasicUI") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
    static public void PopUpUI()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = interactions;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Enviroment/UI/PopUp") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
    static public void CorrectUI()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = interactions;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Enviroment/UI/CorrectUI") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
    static public void IncorrectUI()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = interactions;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Enviroment/UI/IncorrectUI") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }

    #endregion

    #region Ambient

    #endregion
}
