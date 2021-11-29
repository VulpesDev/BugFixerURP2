using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Allign : MonoBehaviour
{
    FP_Shoot fpshoot;
    Flesh playerFlesh;
    CharacterVR playerMov;
    GameObject player;
    public Text healthVal, healthValS;
    public Slider dash;
    public Text dashVal, dashValS;
    public RectTransform crosshair;
    public Slider pistolSlider;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerFlesh = player.GetComponent<Flesh>();
        playerMov = player.GetComponent<CharacterVR>();
        fpshoot = Camera.main.transform.GetComponent<FP_Shoot>();

    }

    private void Update()
    {
        dashValS.text = dashVal.text;
        healthValS.text = healthVal.text;
        healthVal.text = playerFlesh.health.ToString();
        dash.value = playerMov.dashTimer;
        dashVal.text = playerMov.dashes.ToString();
        pistolSlider.value = fpshoot.overheat;
    }

    public void HitBullet()
    {
        StartCoroutine(bulletHit());
    }
    IEnumerator bulletHit()
    {
        MusicManager.HitCrosshair();
        crosshair.rotation = Quaternion.Euler(0, 0, 45);
        crosshair.sizeDelta = new Vector2(120, 120);
        crosshair.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.2f);
        crosshair.rotation = Quaternion.identity;
        crosshair.sizeDelta = new Vector2(100, 100);
        crosshair.GetComponent<Image>().color = Color.red;
    }
}
