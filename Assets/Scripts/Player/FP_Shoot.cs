using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Shoot : MonoBehaviour
{
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    float rayLength = 500f;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                Shoot(hit);
            }
        }
    }

    void Shoot(RaycastHit hitPos)
    {
        GameObject hole = Resources.Load<GameObject>("Player/Shooting/BulletHole");
        GameObject instHole = Instantiate(hole, hitPos.point, Quaternion.LookRotation(hitPos.normal));
        instHole.transform.position += instHole.transform.forward * 0.1f;
    }
}
