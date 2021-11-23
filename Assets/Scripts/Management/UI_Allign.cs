using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Allign : MonoBehaviour
{
    int overheat;
    Slider pistolSlider;
    FP_Shoot fpshoot;

    private void Start()
    {
        fpshoot = Camera.main.transform.GetComponent<FP_Shoot>();
        pistolSlider = Camera.main.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Slider>();
    }

    private void Update()
    {
        overheat = fpshoot.overheat;
        pistolSlider.value = overheat;
    }
}
