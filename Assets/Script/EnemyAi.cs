using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public int health = 2;

    public void GetDamaged(int damage)
	{
        health -= damage;
	}
    

    void Update()
    {
        if (health == 0) Destroy(gameObject);
    }
}
