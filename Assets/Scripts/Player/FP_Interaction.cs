using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                if(target.CompareTag("Monitor") && targetTr.parent.parent.GetComponent<MonitorsStages>()
                    .broken)
                {
                    targetTr.GetChild(0).GetComponent<StatusManager>().PlayMinigame();
                }
                else if (target.CompareTag("FinishMonitor"))
                {
                    SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath
                        ("Assets/Scenes/VR_Level.unity"));
                }
                else if (target.CompareTag("FinishMonitor2"))
                {
                    SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath
                        ("Assets/Scenes/Server_Level.unity"));
                }
            }
        }
        Debug.DrawRay(rayTr.position, rayTr.forward * distance, Color.red);
    }
}
