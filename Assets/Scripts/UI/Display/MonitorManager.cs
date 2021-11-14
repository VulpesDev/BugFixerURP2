using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorManager : MonoBehaviour
{
    StagesManager[] childrenTr;
    GameObject[] children;

    public bool running = true; // to be changed to FALSE!!
    void Awake()
    {
        TrToGo();
        for (int i = 1; i < children.Length; i++)
        {
            children[i].SetActive(false);
        }
    }

    void Update()
    {
        CheckStages();
        gameObject.SetActive(running);
        //if (running && GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().enabled)
        //{
        //    GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().enabled = false;
        //}
        //else
        //{
        //    GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().enabled = true;
        //}

    }

    public void TurnMonitor(string state)
    {
        switch(state)
        {
            case "On":
                running = true;
                break;
            default:
                running = false;
                break;
        }
    }

    void CheckStages()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if(children[i].GetComponent<StagesManager>().ready() && children[i].activeSelf)
            {
                StartCoroutine(FromStageTo(i, i + 1, 0.2f));
            }
        }
    }


    void TrToGo()
    {
        childrenTr = GetComponentsInChildren<StagesManager>();
        children = new GameObject[childrenTr.Length];
        for (int i = 0; i < childrenTr.Length; i++)
        {
            children[i] = childrenTr[i].gameObject;
        }
    }

    IEnumerator FromStageTo(int stage1, int stage2, float delay)
    {
        yield return new WaitForSeconds(delay);
        children[stage1].SetActive(false);
        children[stage2].SetActive(true);
    }
}
