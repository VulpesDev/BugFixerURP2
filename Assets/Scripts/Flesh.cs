using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Flesh : MonoBehaviour
{
   public int health;
    private void Update()
    {
        if (health <= 0) Die();
    }
    public void TakeDamage(int amount)
    {
        if (gameObject.CompareTag("Player"))
            GameObject.FindGameObjectWithTag("UI_Canvas").GetComponent<FP_UI>().VFX_TakeDamage();
        else GameObject.Find("AllignUI").GetComponent<UI_Allign>().HitBullet();

        health -= amount;
    }
    void Die()
    {
        if(gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Death.unity"));
        }
        if(GetComponent<Enemy>() != null)
        {
            if(GameObject.FindGameObjectWithTag("SpawnManager") != null)
            GameObject.FindGameObjectWithTag("SpawnManager")
                .GetComponent<SpawnpointManager>().killed++;

            if(GetComponent<Enemy>().typeEnemy == Enemy_Behaviour.EnemyType.Shooter)
            {
                GameObject g = Resources.Load("AI/Bug-Dead") as GameObject;
                Instantiate(g, transform.position, transform.rotation);
                MusicManager.Explode(transform.position);
                Destroy(gameObject);
            }
            else if (GetComponent<Enemy>().typeEnemy == Enemy_Behaviour.EnemyType.Bomber)
            {
                gameObject.tag = "Untagged";
                Destroy(GetComponent<Enemy>());
                Destroy(GetComponent<CapsuleCollider>());
                Destroy(GetComponent<NavMeshAgent>());
                transform.GetChild(0).GetComponent<Animator>().enabled = false;
                foreach (SphereCollider col in GetComponentsInChildren<SphereCollider>())
                {
                    col.enabled = true;
                }
                foreach (BoxCollider col in GetComponentsInChildren<BoxCollider>())
                {
                    col.enabled = true;
                }
                foreach (CapsuleCollider col in GetComponentsInChildren<CapsuleCollider>())
                {
                    col.enabled = true;
                }
                foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
                {
                    rb.isKinematic = false;
                }
            }
        }
    }
}
