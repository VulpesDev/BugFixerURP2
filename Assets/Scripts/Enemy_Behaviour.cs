using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "AI/Enemy")]
public class Enemy_Behaviour : ScriptableObject
{
    public enum EnemyType { Bomber, Shooter };
    public EnemyType typeEnemy;

    [Space]
    [Header("Steering")]
    public float speed;
    public float angularSpeed;
    public float acceleration;
    public float rotationDamping;
    [Space]
    [Header("Body")]
    public int health;
    [Space]
    [Header("Weapon")]
    public float firerate;
    public int accuracy;
    public int damage;
    [Space]
    [Header("Explosion")]
    public int explodeDamage;
    [Space]
    [Header("AI")]
    public float keepDistanceA;
    public float keepDistanceB;

}
