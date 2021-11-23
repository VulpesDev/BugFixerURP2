using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FP_UI : MonoBehaviour
{
    [SerializeField]Image damageImage;
    GameObject cam;
    public AnimationCurve magnitudeCurve;
    private void Awake()
    {
        cam = Camera.main.gameObject;
    }
    public void VFX_TakeDamage()
    {
        StartCoroutine(TakeDamage());
    }
    IEnumerator TakeDamage()
    {
        VFX_CameraShake(0.35f);
        damageImage.color = new Color(damageImage.color.r, damageImage.color.g,
            damageImage.color.b, 0.45f);
        yield return new WaitForSeconds(0.05f);
        while (damageImage.color.a > 0)
        {
            damageImage.color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    public void VFX_CameraShake(float duration)
    {
        StartCoroutine(CameraShake(duration));
    }
    IEnumerator CameraShake(float duration)
    {
        Vector3 originalPosition = cam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float strenght = magnitudeCurve.Evaluate(elapsed / duration);
            cam.transform.localPosition = originalPosition + Random.insideUnitSphere * strenght;
            yield return null;
        }
        cam.transform.localPosition = originalPosition;
    }
}
