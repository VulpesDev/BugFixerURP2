using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProcessManagement : MonoBehaviour
{
    [SerializeField]Volume dashVol;
    public void StartDash()
    {
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        dashVol.weight = 0.5f;
        while (dashVol.weight < 1)
        {
            dashVol.weight += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.2f);
        while (dashVol.weight > 0)
        {
            dashVol.weight -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
