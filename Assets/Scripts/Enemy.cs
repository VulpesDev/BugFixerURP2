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

    IEnumerator SetDestinationCheck()
    {
        Vector3 velocity = agent.velocity;
        if (distance <= maxDistance) agent.SetDestination(transform.position);
        else agent.SetDestination(player.transform.position);
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

        maxDistanceA = memory.keepDistanceA;
        maxDistanceB = memory.keepDistanceB;

        explodeDamage = memory.explodeDamage;
    }
    public void Shoot()
    {
        
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
    void Debugging()
    {
       if(showDistance) Debug.Log(distance);
       if(showHealth) Debug.Log(flesh.health);
       if(showTarget) Debug.Log(agent.steeringTarget);
    }
}
