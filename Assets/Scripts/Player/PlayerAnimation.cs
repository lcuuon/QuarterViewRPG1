using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMove player;
    Rigidbody rb;
    [SerializeField] GameObject basicAttack;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMove>();
        rb = GetComponentInParent<Rigidbody>();
        basicAttack.SetActive(false);
    }

    void Update()
    {

    }

    //ComboAttack Animation
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
        Debug.Log("noError");
        player.dashError = false;
        anim.SetBool("isBasicAttack2", false);
        player.comboCount = 1;
        player.basicAttack1 = true;
        player.basicAttack2 = false;
        player.canMove = true;
        player.canDash = true;

    }
    private void AttackMove()
    {
        player.isdash = true;
        Vector3 DashDir = player.transform.localRotation * Vector3.forward;
        rb.velocity = DashDir * 10;
        Invoke("StopDash", 0.2f);
    }
    private void StopDash()
    {
        rb.velocity = Vector3.zero;
        player.isdash = false;
    }
    private void AttackStart()
    {
        basicAttack.SetActive(true);
    }
    private void AttackEnd()
    {
        basicAttack.SetActive(false);
    }

    //Dash
    private void CanDash()
    {
        player.canDash = true;
    }
    private void CheckDeshError()
    {
        player.dashError = true;
    }
}
