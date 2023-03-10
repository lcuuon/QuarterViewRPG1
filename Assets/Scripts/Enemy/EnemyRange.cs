using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
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
                //Debug.Log(other.name);
                enemy.player = other.gameObject;
                enemy.isAttack = true;
                enemy.isAggro = false;
            }
            else if (this.gameObject.name == "aggroRange")
            {
                if (!enemy.isAttack)
                {
                    //Debug.Log(other.name);
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
