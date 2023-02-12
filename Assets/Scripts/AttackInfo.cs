using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    Enemy enemy;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Damage");
            enemy = other.GetComponent<Enemy>();
            enemy.Knockback(player.gameObject.transform.localRotation * Vector3.forward * KnockbackRange);
            enemy.curHP -= SkillDamage + SkillCoefficient * levelManager.AtkDamage;
        }
    }
}
