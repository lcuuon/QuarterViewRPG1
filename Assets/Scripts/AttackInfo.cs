using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    Enemy enemy;
    EnemyArcher Aenemy;
    Rigidbody enemyrb;
    PlayerMove player;
    PlayerLevelManager levelManager;

    [SerializeField] float SkillDamage;
    [SerializeField] float SkillCoefficient;
    [SerializeField] float castingTime;
    [SerializeField] float KnockbackRange;
    [SerializeField] float SternTime;

    void Start()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        player = GetComponentInParent<PlayerMove>();
    }

    void Update()
    {
        
    }

    private int CriticalProbCal()
    {
        if (levelManager.criticalProb > 0)
        {
            int CriCheck = Random.RandomRange(1, 100);
            bool isCritical = (CriCheck <= levelManager.criticalProb);
            if (isCritical)
            {
                Debug.Log("Critical!");
                return levelManager.criticalDmg;
            }
        }
        else
        {
            return 0;
        }

        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Damage");
            enemy = other.GetComponent<Enemy>();
            if (enemy == null)
            {
                Debug.Log("aa");
                Aenemy = other.GetComponent<EnemyArcher>();
                Aenemy.Knockback(player.gameObject.transform.localRotation * Vector3.forward * KnockbackRange);
                Aenemy.curHP -= SkillDamage + SkillCoefficient * levelManager.AtkDamage + CriticalProbCal();
            }
            else
            {
                enemy.Knockback(player.gameObject.transform.localRotation * Vector3.forward * KnockbackRange);
                enemy.curHP -= SkillDamage + SkillCoefficient * levelManager.AtkDamage + CriticalProbCal();
            }           
        }
    }
}
