using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerMove player;

    [SerializeField] float Damage;

    void Start()
    {
        player = GameObject.Find("Character").GetComponent<PlayerMove>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Damage");
            player.PlayerCurHP -= Damage;
        }
    }
}
