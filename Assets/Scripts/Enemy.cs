using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Enemy_Behaviour memory;
    Enemy_Behaviour.EnemyType typeEnemy;
    NavMeshAgent agent;

    GameObject player;
    float distance;
    float distanceY;
    float maxDistanceA;
    float maxDistanceB;
    float maxDistance;

    int explodeDamage;

    Flesh flesh;
    float firerate;
    int accuracy;
    int damage;

    private void Awake()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        flesh = gameObject.AddComponent<Flesh>();
        LoadSettings();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxDistance = Random.Range(maxDistanceA, maxDistanceB);
        StartCoroutine(SetDestinationCheck());
        if (typeEnemy == Enemy_Behaviour.EnemyType.Shooter) StartCoroutine(Aim());
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
        }
        else if (typeEnemy == Enemy_Behaviour.EnemyType.Shooter) ///// Shooter ONLY
        {
            
        }

#if UNITY_EDITOR
        Debugging();
#endif
    }
    IEnumerator Aim()
    {
        if (HasVision())
        {
            yield return new WaitForSeconds(firerate);
            Shoot();
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Aim());
    }
    public void Shoot()
    {
        int percent = Random.Range(0, 101);
        if(percent<=accuracy && HasVision())
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
        typeEnemy = memory.typeEnemy;

        agent.speed = memory.speed;
        agent.angularSpeed = memory.angularSpeed;
        agent.acceleration = memory.acceleration;

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
    }
}
