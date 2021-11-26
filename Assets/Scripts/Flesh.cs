using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        health -= amount;
    }
    void Die()
    {
        if(GetComponent<Enemy>() != null)
        {
            if(GetComponent<Enemy>().typeEnemy == Enemy_Behaviour.EnemyType.Shooter)
            {
                GameObject g = Resources.Load("AI/Bug-Dead") as GameObject;
                Instantiate(g, transform.position, transform.rotation);
                MusicManager.Explode(transform.position);
            }
        }
        //temporary
        Destroy(gameObject);
    }
}
