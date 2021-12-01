using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{
    public int requiredCount;
    public int maxCount;
    [HideInInspector]public int killed;
    [SerializeField] GameObject superPC;
    private void FixedUpdate()
    {
        if(killed >= maxCount)
        {
            //completed
            superPC.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.blue);
            for (int i = 0; i < 2; i++)
            {
                superPC.transform.GetChild(4 + i).gameObject.SetActive(true);
            }
        }
        else
        {
            superPC.GetComponent<Renderer>().materials[2].SetColor("_EmissionColor", Color.red);
            for (int i = 0; i < 2; i++)
            {
                superPC.transform.GetChild(4 + i).gameObject.SetActive(false);
            }
        }
    }
}
