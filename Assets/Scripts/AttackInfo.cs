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

    private bool _isCritical = false;

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
            int CriCheck = Random.Range(1, 100);
            bool isCritical = (CriCheck <= levelManager.criticalProb);
            if (isCritical)
            {
                Debug.Log("Critical!");
                _isCritical = true;
                return levelManager.criticalDmg;
            }
        }
        else
        {
            _isCritical = false;
            return 0;
        }
        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.GetComponent<Enemy>();
            if (enemy == null)
            {
                Aenemy = other.GetComponent<EnemyArcher>();
                Aenemy.Knockback(player.gameObject.transform.localRotation * Vector3.forward * KnockbackRange);
                float Damage = SkillDamage + SkillCoefficient * levelManager.AtkDamage + CriticalProbCal();
                Aenemy.curHP -= Damage;
                Aenemy.DamageText(Damage, _isCritical);
                _isCritical = false;
            }
            else
            {
                enemy.Knockback(player.gameObject.transform.localRotation * Vector3.forward * KnockbackRange);
                float Damage = SkillDamage + SkillCoefficient * levelManager.AtkDamage + CriticalProbCal();
                enemy.curHP -= Damage;
                enemy.DamageText(Damage, _isCritical);
                _isCritical = false;

            }
        }
    }
}
