using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    FP_Shoot fps;
    FP_ShootT fpst;
    private void Awake()
    {
        fps = Camera.main.GetComponent<FP_Shoot>();
        fpst = Camera.main.GetComponent<FP_ShootT>();
    }
    public void ReloadSound()
    {
        MusicManager.ReloadSound();
    }
    public void AirDischarge(int num)
    {
        switch(num) { case 1: MusicManager.AirDischarge();
                fps.Stream(true); fps.Heat(true);
                break; 
            case 2: MusicManager.AirDischarge2();
                fpst.pressureSteam.Play(); break; }
    }
    public void SetReloadingTo1(int state)
    {
        bool _state = state == 0 ? true : false;
        fps.isReloading = _state;
    }
    public void SetReloadingTo2(int state)
    {
        bool _state = state == 0 ? true : false;
        fpst.isReloading = _state;
    }
    public void SetShootFalse(int num)
    {
        switch (num)
        {
            case 1:
                fps.pistolAnime.SetBool("Shoot", false);
                break;
            case 2: fpst.gunAnime.SetBool("Shoot", false); break;
        }
    }
}
