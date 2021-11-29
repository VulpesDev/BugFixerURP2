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
        if (Input.GetKeyDown(KeyCode.P))
        {
            laser = !laser;
        }
    }

    #region Player

    static public void ShootPistol()
    {
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
    static public void AirDischarge()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = player;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Player/Air_Discharge") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
    static public void ReloadSound()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = player;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Player/ReloadSound") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
    static public void ReloadInitialize()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = player;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Player/ReloadInitialization") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
    static public void DashSound()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = player;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Player/DashSound") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }

    #endregion

    #region Enviroment
    static public void Explode(Vector3 position)
    {

        GameObject Sound = new GameObject();
        Sound.transform.parent = interactions;
        Sound.transform.position = position;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Enviroment/BugExplosion") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.spatialBlend = 1;
        ASound.minDistance = 10;
        ASound.maxDistance = 40;
        ASound.pitch = Random.Range(0.8f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
    static public void ShootLaser(Vector3 position)
    {

        GameObject Sound = new GameObject();
        Sound.transform.parent = interactions;
        Sound.transform.position = position;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Player/LaserShot") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.spatialBlend = 1;
        ASound.minDistance = 10;
        ASound.maxDistance = 40;
        ASound.pitch = Random.Range(0.5f, 0.8f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }
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
    static public void HitCrosshair()
    {
        GameObject Sound = new GameObject();
        Sound.transform.parent = interactions;
        AudioSource ASound = Sound.AddComponent<AudioSource>();
        ASound.clip = Resources.Load("Sounds/Enviroment/Hit") as AudioClip;
        //ASound.outputAudioMixerGroup = Resources.Load<AudioMixer>("Sounds/Master").FindMatchingGroups("Master")[0];
        ASound.pitch = Random.Range(0.9f, 1.2f);
        ASound.Play();
        Sound.AddComponent<AudiosDefault>();
    }

    #endregion

    #region Ambient

    #endregion
}
