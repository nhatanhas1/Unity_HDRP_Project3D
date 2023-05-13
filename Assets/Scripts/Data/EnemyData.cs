using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Enemy", menuName ="Emeny")]
public class EnemyData : ScriptableObject
{
    public int id;
    public string nameEnemy;
    public Sprite sprite;
    
    public int hitPoint;
    public int attackDamage;
    public int armor;    
    public float attackSpeed;
    public float moveSpeed;

    public enum EnemyType
    {
        Slime1,
        Slime2,
        Slime3,
        Bat1,
        Bat2,
        Bat3,

    }

    public EnemyType enemyType;

    public RuntimeAnimatorController runtimeAnimatorController;


}
