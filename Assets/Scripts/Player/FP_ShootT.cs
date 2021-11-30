using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_ShootT : MonoBehaviour
{
    GameObject gun;
    public Animator gunAnime;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    float rayLength = 500f;
    int damage = 10;
    public int overheat = 0;
    bool canShoot;
    public bool isReloading = false;

    public ParticleSystem muzzleFlash, pressureSteam;


    private void OnEnable()
    {
        isReloading = false;
    }
    private void Start()
    {
        gun = transform.GetChild(2).gameObject;
        gunAnime = gun.transform.GetChild(0).GetComponent<Animator>();
        muzzleFlash = gun.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        pressureSteam = gun.transform.GetChild(0).GetChild(2).GetChild(0)
            .GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        if (overheat > 100) overheat = 100;
        else if (overheat >= 100) canShoot = false;
        else canShoot = true;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (canShoot && !isReloading)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (!gunAnime.GetBool("Shoot"))
                    Raycast();
            }
        }
    }


    void Raycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            ShootHit(hit);
            if (hit.collider.GetComponent<Flesh>() != null) hit.collider.GetComponent<Flesh>().TakeDamage(damage);
        }
        else
        {
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        if (!isReloading)
        {
            gunAnime.SetBool("Reload", true);
            while (!gunAnime.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
                yield return new WaitForEndOfFrame();
            gunAnime.SetBool("Reload", false);
           
            StartCoroutine(Discharge());
            MusicManager.ReloadInitialize();

            //for (int i = 0; i < 6; i++)
            //{
            //    yield return new WaitForEndOfFrame();
            //}
            //MusicManager.ReloadSound();
            //for (int i = 0; i < 13; i++)
            //{
            //    yield return new WaitForEndOfFrame();
            //}
            //MusicManager.ReloadSound();
            //pressureSteam.Play();
            //MusicManager.AirDischarge2();
            //for (int i = 0; i < 74; i++)
            //{
            //    yield return new WaitForEndOfFrame();
            //}
            //MusicManager.ReloadSound();
            //yield return new WaitForEndOfFrame();
            //yield return new WaitForEndOfFrame();

            //isReloading = false;
        }
    }
    IEnumerator Discharge()
    {
        while (overheat > 0)
        {
            overheat--;
            yield return new WaitForSeconds(0.0035f);
        }
    }

    void ShootHit(RaycastHit hitPos)
    {
        Shoot();
        GameObject hole = Resources.Load<GameObject>("Player/Shooting/BulletHole");
        GameObject instHole = Instantiate(hole, hitPos.point, Quaternion.LookRotation(hitPos.normal));
        instHole.transform.parent = hitPos.collider.transform;
        instHole.transform.position += instHole.transform.forward * 0.05f;
    }
    void Shoot()
    {
        GameObject.FindGameObjectWithTag("UI_Canvas").GetComponent<FP_UI>().VFX_CameraShake(0.1f, 0.5f);

        overheat += 3;

        muzzleFlash.Play();
        MusicManager.TommyGun();
        gunAnime.SetBool("Shoot", true);
    }
}
