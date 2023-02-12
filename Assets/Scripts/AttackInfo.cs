using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    Enemy enemy;
    Rigidbody enemyrb;
    PlayerMove player;

    [SerializeField] public float Damage;
    [SerializeField] float castingTime;
    [SerializeField] float KnockbackRange;
    [SerializeField] float SternTime;

    void Start()
    {
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
        }
    }
}
