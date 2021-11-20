using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Shoot : MonoBehaviour
{
    GameObject pistol;
    Animator pistolAnime;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    float rayLength = 500f;

    ParticleSystem muzzleFlash;


    private void Start()
    {
        pistol = transform.GetChild(1).gameObject;
        pistolAnime = pistol.transform.GetChild(0).GetComponent<Animator>();

        muzzleFlash = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                ShootHit(hit);
            }
            else
            {
                Shoot();
            }
        }
    }

    void ShootHit(RaycastHit hitPos)
    {
        Shoot();
        GameObject hole = Resources.Load<GameObject>("Player/Shooting/BulletHole");
        GameObject instHole = Instantiate(hole, hitPos.point, Quaternion.LookRotation(hitPos.normal));
        instHole.transform.position += instHole.transform.forward * 0.1f;
    }
    void Shoot()
    {
        MusicManager.ShootPistol();
        muzzleFlash.Play();
        StartCoroutine(ShootAnim());
    }

    IEnumerator ShootAnim()
    {
        pistolAnime.SetBool("Shoot", true);
        yield return new WaitForSeconds(0.01f);
        pistolAnime.SetBool("Shoot", false);
    }
}
