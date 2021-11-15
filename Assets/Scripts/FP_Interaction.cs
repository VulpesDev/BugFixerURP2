﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Interaction : MonoBehaviour
{
    [SerializeField] Transform rayTr;
    [SerializeField] float distance;
    [SerializeField] LayerMask layerCode;

    void Update()
    {
        RaycastHit ray;
        bool hit = Physics.Raycast(rayTr.position, rayTr.forward, out ray, distance, layerCode.value);
        if (hit)
        {
            GameObject target = ray.collider.gameObject;
            Transform targetTr = target.transform;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(target.CompareTag("Monitor") && targetTr.parent.parent.GetComponent<MonitorsStages>().broken)
                {
                    targetTr.GetChild(0).GetComponent<StatusManager>().PlayMinigame();
                }
            }
        }
        Debug.DrawRay(rayTr.position, rayTr.forward * distance, Color.red);
    }
}