using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    [Header("Difficulty")] [SerializeField] int speed = 150;
    [Range(1, 13)] public int popups;
    [Range(0, 100)] public float percentage;
    KeyWord[] panelsTr;
    GameObject[] panels;
    [HideInInspector]public bool ready, passed;
    void Awake()
    {
        TrToGo();
        HidePanels();
        SetSpeedToKeyWord();
    }

    void Update()
    {
        for (int i = 0; i < popups - 1; i++)
        {
            if(panelsTr[i].ready)
            {
                StartCoroutine(StageTo(i+1, 0.1f));
            }
        }

        if(LastIsReady())
        {
            if(SuccessRate() >= percentage)
            {
                passed = true;
            }
            else
            {
                passed = false;
            }
            ready = true;
        }
    }
    public float SuccessRate()
    {
        int suceeded = 0;
        for (int i = 0; i < popups; i++)
        {
            if (panelsTr[i].success)
                suceeded++;
        }
        return ((float)suceeded/(float)popups) * 100f;
    }
    bool LastIsReady()
    {
        if(panelsTr[popups - 1].ready)
        {
            return true;
        }
        return false;
    }
    void SetSpeedToKeyWord()
    {
        foreach (KeyWord _keyword in panelsTr)
        {
            _keyword.speed = speed;
        }
    }
    void HidePanels()
    {
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }
    void TrToGo()
    {
        panelsTr = GetComponentsInChildren<KeyWord>();
        panels = new GameObject[panelsTr.Length];
        for (int i = 0; i < panelsTr.Length; i++)
        {
            panels[i] = panelsTr[i].gameObject;
        }
    }

    IEnumerator StageTo(int stage2, float delay)
    {
        yield return new WaitForSeconds(delay);
        panels[stage2].SetActive(true);
    }
}
