using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMove player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMove>();
    }

    void Update()
    {

    }

    private void BasicAttackCombo()
    {
        if (player.comboCount == 2)
        {
            anim.SetBool("isBasicAttack2", true);
        }
        else
        {
            player.comboCount = 1;
            player.basicAttack1 = true;
            player.basicAttack2 = false;
            player.canMove = true;
        }
    }

    private void BasicAttackEnd()
    {
        anim.SetBool("isBasicAttack2", false);
        player.comboCount = 1;
        player.basicAttack1 = true;
        player.basicAttack2 = false;
        player.canMove = true;
    }
}
