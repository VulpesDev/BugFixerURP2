using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    SpawnpointManager manager;
    public bool shooterOnly;
    bool bomber = true, bug = true;
    float minDistance = 45f;
    Renderer render;
    public static int requiredCount;
    public static int maxCount;
    static int spawnedCount = 0;
    GameObject enemy, player;
    GameObject[] enemies;

    void Start()
    {
        spawnedCount = 0;
        manager = transform.parent.GetComponent<SpawnpointManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        render = GetComponent<Renderer>();
        requiredCount = manager.requiredCount;
        maxCount = manager.maxCount;
        StartCoroutine(Spawning());
    }
    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies != null)
        {
            int bombers = 0;
            int bugs = 0;
            foreach (var enem in enemies)
            {
                if (enem.GetComponent<Enemy>().typeEnemy == Enemy_Behaviour.EnemyType.Bomber)
                    bombers++;
                else bugs++;
            }
            bomber = bombers > requiredCount / 2 ? false : true;
            bug = bugs > requiredCount / 2 ? false : true;
        }

        if(enemies.Length < requiredCount && spawnedCount < maxCount
            && Vector3.Distance(player.transform.position,
            transform.position) >= minDistance)
        {
            AddEnemy();
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Spawning());
    }
    void AddEnemy()
    {
        if (render.isVisible) return;

        if (!shooterOnly)
        {
            if (bomber)
                enemy = Resources.Load("AI/Human") as GameObject;
            else
                enemy = Resources.Load("AI/Bug") as GameObject;
        }
        else if (bug) enemy = Resources.Load("AI/Bug") as GameObject;
        else return;

        if (SeneManagement.hard)
        {
            if (enemy.GetComponent<Enemy>().memory == Resources.Load("AI/Presets/Bomber-Normal"))
            {
                enemy.GetComponent<Enemy>().memory = Resources.Load("AI/Presets/Bomber-Hard")
                    as Enemy_Behaviour;
            }
            else if (enemy.GetComponent<Enemy>().memory == Resources.Load("AI/Presets/Shooter-Normal"))
            {
                enemy.GetComponent<Enemy>().memory = Resources.Load("AI/Presets/Shooter-Hard")
                    as Enemy_Behaviour;
            }
        }

        Instantiate(enemy, transform.position, transform.rotation);
        spawnedCount++;
    }
}
