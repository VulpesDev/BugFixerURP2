using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorsStages : MonoBehaviour
{
    public bool broken = false;
    bool rebooting = false;
    public GameObject[] statuses = new GameObject[3];
    AudioSource alarmAS;
    Material matA, matB;
    Color colorDefault;

    private void Awake()
    {
        SetStatuses();
        alarmAS = transform.GetChild(3).GetComponent<AudioSource>();
        matA = transform.parent.GetComponent<MeshRenderer>().materials[2];
        matB = transform.parent.GetChild(0).GetComponent<MeshRenderer>().materials[0];
        colorDefault = matA.GetColor("_EmissionColor");
    }
    void Update()
    {
        if (rebooting) return;
        if(broken)
        {
            if (!alarmAS.isPlaying) alarmAS.Play();
            matA.SetColor("_EmissionColor", Color.red); matB.SetColor("_EmissionColor", Color.red);
            for (int i = 0; i < statuses.Length; i++)
            {
                statuses[i].transform.GetChild(0).gameObject.SetActive(true);
                statuses[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else
        {
            if (alarmAS.isPlaying) alarmAS.Stop();
            matA.SetColor("_EmissionColor", colorDefault); matB.SetColor("_EmissionColor", colorDefault);
            for (int i = 0; i < statuses.Length; i++)
            {
                statuses[i].transform.GetChild(0).gameObject.SetActive(false);
                statuses[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    public void ResetMinigameR()
    {
        // RESETING the right one
        rebooting = true;
        Transform minigameR = transform.GetChild(0);
        if (minigameR.GetChild(0).GetChild(3).GetComponent<PopUps>().passed)
            broken = false;
        else
            broken = true;
        Destroy(minigameR.GetChild(0).gameObject);
        GameObject g = Resources.Load<GameObject>("Display/Monitor_Canvas");
        GameObject ginst = Instantiate(g, minigameR.position, minigameR.rotation, minigameR);
        ginst.name = "Monitor_CanvasR";
        ginst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


        Invoke("SetStatuses", 0.1f);
    }
    public void ResetMinigameL()
    {
        // RESETING the left one
        rebooting = true;
        Transform minigameL = transform.GetChild(1);
        if (minigameL.GetChild(0).GetChild(3).GetComponent<PopUps>().passed)
            broken = false;
        else
            broken = true;
        Destroy(minigameL.GetChild(0).gameObject);
        GameObject g2 = Resources.Load<GameObject>("Display/Monitor_Canvas");
        GameObject ginst2 = Instantiate(g2, minigameL.position, minigameL.rotation, minigameL);
        ginst2.name = "Monitor_CanvasL";
        ginst2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        Invoke("SetStatuses", 0.1f);
    }

    void SetStatuses()
    {
        for (int i = 0; i < statuses.Length; i++)
        {
            statuses[i] = transform.GetChild(i).GetChild(0).GetChild(0).gameObject;
        }
        rebooting = false;
    }
}
