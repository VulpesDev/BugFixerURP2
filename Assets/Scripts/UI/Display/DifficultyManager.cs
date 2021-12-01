using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Difficulty")] public int speed = 150;
    [Range(1, 13)] public int popups;
    [Range(0, 100)] public float percentage;
    int[] serverNums = new int[4];
    public int brokenServers = 0;
    public bool stageFinished = false;

    GameObject[] mainMonitors;

    [SerializeField] GameObject superPC;

    private void Awake()
    {
        mainMonitors = GameObject.FindGameObjectsWithTag("ServerMonitor");
    }
    private void Start()
    {
        Bugging(); StartCoroutine(SlowUpdate());
    }
    private void FixedUpdate()
    {
        if(brokenServers <= 0)
        {
            superPC.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.green);
            for (int i = 0; i < 2; i++)
            {
                superPC.transform.GetChild(4 + i).gameObject.SetActive(true);
            }
        }
        else
        {
            superPC.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.red);
        }
    }
    void Bugging()
    {
        for (int i = 0; i < serverNums.Length; i++)
        {
            int r = Random.Range(0, mainMonitors.Length);
            if (i != 0)
            {
                do r = Random.Range(0, mainMonitors.Length);
                while (serverNums[i - 1] == r);
            }
            serverNums[i] = r;
            mainMonitors[serverNums[i]].
                GetComponent<MonitorsStages>().broken = true;
        }
    }
    IEnumerator SlowUpdate()
    {
        CheckForCompletition();

        yield return new WaitForSeconds(1f);
        StartCoroutine(SlowUpdate());
    }
    void CheckForCompletition()
    {
        brokenServers = 0;
        for (int i = 0; i < serverNums.Length; i++)
        {
            if (mainMonitors[serverNums[i]].
                GetComponent<MonitorsStages>().broken) brokenServers++;
        }
        if (brokenServers <= 0) stageFinished = true;
        else stageFinished = false;
    }
}
