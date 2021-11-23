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
        //temporary
        Destroy(gameObject);
    }
}
