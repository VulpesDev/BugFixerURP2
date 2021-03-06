using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Enemy_Behaviour memory;
    [HideInInspector]public Enemy_Behaviour.EnemyType typeEnemy;
    NavMeshAgent agent;

    GameObject player;
    float distance;
    float distanceY;
    float maxDistanceA;
    float maxDistanceB;
    float maxDistance;
    float rotationDamping;

    int explodeDamage;

    Flesh flesh;
    float firerate;
    int accuracy;
    int damage;

    GameObject turret;

    ParticleSystem muzzleFlash;
    AudioSource bugEngine;

    Animator humanAnimator;

    private void Awake()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        flesh = gameObject.AddComponent<Flesh>();
    }
    void PlayEngineSound()
    {
        bugEngine.Play();
    }
    private void Start()
    {
        LoadSettings();

        player = GameObject.FindGameObjectWithTag("Player");
        maxDistance = Random.Range(maxDistanceA, maxDistanceB);
        StartCoroutine(SetDestinationCheck());
        if (typeEnemy == Enemy_Behaviour.EnemyType.Shooter)
        {
            bugEngine = transform.GetChild(0).GetComponent<AudioSource>();
            StartCoroutine(Aim());
            turret = transform.GetChild(0).GetChild(0).gameObject;
            muzzleFlash = turret.transform.GetChild(1).GetComponent<ParticleSystem>();
            bugEngine.pitch = Random.Range(0.8f, 1.2f);
            Invoke("PlayEngineSound", Random.Range(0.0f, 0.9f));
        }
        else if (typeEnemy == Enemy_Behaviour.EnemyType.Bomber)
        {
            humanAnimator = transform.GetChild(0).GetComponent<Animator>();
        }

    }

    void Update()
    {
        distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(player.transform.position.x, 0, player.transform.position.z));
        distanceY = Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(player.transform.position.y));

        

        if (typeEnemy == Enemy_Behaviour.EnemyType.Bomber)   ///// Bomber ONLY
        {
            if (distance <= maxDistanceB && distanceY <= 3) Explode();
            humanAnimator.SetFloat("RunningSpeed", (agent.velocity.magnitude / agent.speed) + 0.2f); 
        }
        else if (typeEnemy == Enemy_Behaviour.EnemyType.Shooter) ///// Shooter ONLY
        {
            //limits Xrot 25 and -25
            Vector3 lookPosY = player.transform.position - turret.transform.position;
            lookPosY.y = 0;
            Quaternion rotationY = Quaternion.LookRotation(lookPosY);
            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, rotationY, Time.deltaTime 
                * rotationDamping);

            Vector3 relativePos = player.transform.position - turret.transform.position;
            Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);
            Quaternion LookAtRotationOnly_X = Quaternion.Euler(LookAtRotation.eulerAngles.x,
                turret.transform.rotation.eulerAngles.y, turret.transform.rotation.eulerAngles.z);
            turret.transform.rotation = LookAtRotationOnly_X;

            Vector3 lookPos2 = player.transform.position - transform.position;
            lookPos2.y = 0;
            Quaternion rotation2 = Quaternion.LookRotation(lookPos2);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation2, Time.deltaTime
                * rotationDamping/3);
        }

#if UNITY_EDITOR
        Debugging();
#endif
    }
    IEnumerator Aim()
    {
        if (HasAim())
        {
            yield return new WaitForSeconds(firerate);
            Shoot();
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Aim());
    }
    public void Shoot()
    {
        muzzleFlash.Play();
        MusicManager.ShootLaser(transform.position);

        int percent = Random.Range(0, 101);
        if(percent<=accuracy && HasAim())
        {
            player.GetComponent<Flesh>().TakeDamage(damage);
        }
    }
    bool HasVision()
    {
        Vector3 raycastPos = new Vector3(transform.position.x, transform.position.y + 0.5f,
            transform.position.z);
        Vector3 direction = player.transform.position - transform.position;
        direction = direction.normalized;
        RaycastHit ray;
        if (Physics.Raycast(raycastPos, direction, out ray) && ray.collider.tag == "Player")
            return true;
        else return false;
    }
    [SerializeField] Transform raycastTr;
    bool HasAim()
    {
        Vector3 direction = -raycastTr.up;
        direction = direction.normalized;
        RaycastHit ray;
        if (Physics.Raycast(raycastTr.position, direction, out ray) && ray.collider.tag == "Player")
            return true;
        else return false;
    }

    IEnumerator SetDestinationCheck()
    {
        Vector3 velocity = agent.velocity;
        if (distance > maxDistance || !HasVision()) agent.SetDestination(player.transform.position);
        else agent.SetDestination(transform.position);
        yield return new WaitWhile(() => agent.pathPending);
        agent.velocity = velocity;
        yield return new WaitForSeconds(1f);
        StartCoroutine(SetDestinationCheck());
    }

    void LoadSettings()
    {
        Debug.Log(memory);
        typeEnemy = memory.typeEnemy;

        agent.speed = memory.speed;
        agent.angularSpeed = memory.angularSpeed;
        agent.acceleration = memory.acceleration;
        rotationDamping = memory.rotationDamping;

        flesh.health = memory.health;
        firerate = memory.firerate;
        accuracy = memory.accuracy;
        damage = memory.damage;

        maxDistanceA = memory.keepDistanceA;
        maxDistanceB = memory.keepDistanceB;

        explodeDamage = memory.explodeDamage;
    }
    public void Explode()
    {

        player.GetComponent<Flesh>().TakeDamage(explodeDamage);
        flesh.TakeDamage(flesh.health);
    }
    
    [Space]
    [Header("Debugging")]
    public bool showDistance;
    public bool showHealth;
    public bool showTarget;
    public bool showVision;
    public bool showAim;
    void Debugging()
    {
       if(showDistance) Debug.Log(distance);
       if(showHealth) Debug.Log(flesh.health);
       if(showTarget) Debug.Log(agent.steeringTarget);
       if(showVision)
        {
            Color col = Color.red;
            if (HasVision()) col = Color.green; else col = Color.red;
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1,
            transform.position.z), (player.transform.position - transform.position).normalized *
                Vector3.Distance(player.transform.position, transform.position), col);
        }
        if (showAim)
        {
            Color col = Color.red;
            if (HasAim()) col = Color.green; else col = Color.red;
            Debug.DrawRay(raycastTr.position, -raycastTr.up *
                Vector3.Distance(player.transform.position, transform.position), col);
        }
    }
}
