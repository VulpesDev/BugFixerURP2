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
        health -= amount;
    }
    void Die()
    {
        //temporary
        Destroy(gameObject);
    }
}
