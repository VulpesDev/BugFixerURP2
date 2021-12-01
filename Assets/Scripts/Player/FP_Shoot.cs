using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Shoot : MonoBehaviour
{
    GameObject pistol;
    public Animator pistolAnime;
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
    float rayLength = 500f;
    int damage = 10;
    public int overheat = 0;
    bool canShoot;
    public bool isReloading = false;

    public ParticleSystem muzzleFlash, pressuredL, pressuredR, heatDistortion;


    private void OnEnable()
    {
        isReloading = false;
    }

    private void Start()
    {
        pistol = transform.GetChild(1).gameObject;
        pistolAnime = pistol.transform.GetChild(0).GetComponent<Animator>();

        muzzleFlash = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();

        pressuredL = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<ParticleSystem>();
        pressuredR = transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<ParticleSystem>();
        heatDistortion = transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<ParticleSystem>();
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Raycast();
            }
            //else if (Input.GetKeyDown(KeyCode.Mouse1))
            //{
            //    if (!inBurst)
            //        StartCoroutine(Burst());
            //}
        }
    }

    //IEnumerator Burst()
    //{
    //    inBurst = true;
    //    for (int i = 0; i < 3; i++)
    //    {
    //        Raycast();
    //        yield return new WaitForSeconds(0.08f);
    //    }
    //    inBurst = false;
    //}

    void Raycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            ShootHit(hit);
            if (hit.collider.GetComponent<Flesh>() != null) hit.collider.GetComponent<Flesh>().TakeDamage(damage);
            if (hit.collider.gameObject.CompareTag("Target")) StartCoroutine(ShotTrueFalseTarget(hit));
        }
        else
        {
            Shoot();
        }
    }

    IEnumerator ShotTrueFalseTarget(RaycastHit hit)
    {
        hit.collider.GetComponent<Animator>().SetBool("Shot", true);
        yield return new WaitForEndOfFrame();
        hit.collider.GetComponent<Animator>().SetBool("Shot", false);
    }

    IEnumerator Reload()
    {
        if (!isReloading)
        {
            //isReloading = true;

            pistolAnime.SetBool("Reload", true);
            yield return new WaitForEndOfFrame();
            pistolAnime.SetBool("Reload", false);
            MusicManager.ReloadInitialize();
            StartCoroutine(Discharge());
        }
    }
    IEnumerator Discharge()
    {
        while(overheat>0)
        {
            overheat--;
            yield return new WaitForSeconds(0.0035f);
        }
    }
    public void Stream(bool state)
    {
        if (state) { pressuredL.Play(); pressuredR.Play(); } else { pressuredL.Stop(); pressuredR.Stop(); }
    }
    public void Heat(bool state)
    {
        if (state) heatDistortion.Play(); else heatDistortion.Stop();
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

        MusicManager.ShootPistol();
        muzzleFlash.Play();
        StartCoroutine(ShootingOnOff());
    }
    IEnumerator ShootingOnOff()
    {
        pistolAnime.SetBool("Shoot", true);
        yield return new WaitForEndOfFrame();
        pistolAnime.SetBool("Shoot", false);
    }
}
