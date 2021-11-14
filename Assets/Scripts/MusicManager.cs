using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    static public Transform ambient, player, interactions;
    static public AudioSource heartBeat;

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
        
    }

    #region Player

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
