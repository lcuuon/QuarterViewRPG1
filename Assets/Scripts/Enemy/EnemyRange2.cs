using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange2 : MonoBehaviour
{
    EnemyArcher enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyArcher>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "AttackRange")
            {
                enemy.player = other.gameObject;
                enemy.isAttack = true;
                enemy.isAggro = false;
            }
            else if (this.gameObject.name == "aggroRange")
            {
                if (!enemy.isAttack)
                { 
                    enemy.player = other.gameObject;
                    enemy.isAggro = true;
                }
            }
            else
            {
                enemy.isAttack = false;
                enemy.isAggro = false;
            }
        }
        else
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "AttackRange")
            {
                enemy.isAttack = false;
            }
        }
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "aggroRange")
            {
                enemy.isAggro = false;
            }
        }

    }

    
}
