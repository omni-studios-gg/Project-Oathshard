using System.Collections;
using System.Collections.Generic;
using Omni;
using UnityEngine;

public class Enemy : MonoBehaviour,HelloWorld.IDamageable
{
    private int health = 100;
    public bool IsDead => health <= 0;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy took" + amount + "damage, Health now"  + health);
        if(IsDead)
            Debug.Log("Enemy Died!");
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(25);
        }
    }
}
