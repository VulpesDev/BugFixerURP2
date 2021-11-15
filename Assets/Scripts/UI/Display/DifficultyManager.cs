using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Difficulty")] public int speed = 150;
    [Range(1, 13)] public int popups;
    [Range(0, 100)] public float percentage;

    GameObject[] mainMonitors;

    private void Awake()
    {
        mainMonitors = GameObject.FindGameObjectsWithTag("ServerMonitor");
    }
    private void Start()
    {
        StartCoroutine(Bugging());
    }

    IEnumerator Bugging()
    {
        int r = Random.Range(0, mainMonitors.Length);
        mainMonitors[r].GetComponent<MonitorsStages>().broken = true;

        yield return new WaitForSeconds(15f);
        StartCoroutine(Bugging());
    }
}
