using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMove player;
    Rigidbody rb;
    [SerializeField] GameObject basicAttack;

    //Skill Effect
    [SerializeField] GameObject[] Effect;
    private int combonum;

    void Start()
    {
        combonum = 0;
        for (int i = 0; i < Effect.Length; i++)
        {
            Effect[i].SetActive(false);
        }
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
        Debug.Log(player.comboCount);
        if (player.comboCount == 2)
        {
            anim.SetBool("isBasicAttack2", true);
        }
        if (player.comboCount == 1)
        {
            Debug.Log("bb");
            player.comboCount = 1;
            player.basicAttack1 = true;
            player.basicAttack2 = false;
            //player.canMove = true;
            player.canBasicAttack = false;
        }
    }
    private void BasicAttack2End()
    {
        combonum = 0;
        Debug.Log("noError");
        player.dashError = false;
        anim.SetBool("isBasicAttack2", false);
        player.canDash = true;
        player.basicAttack1 = true;
        player.basicAttack2 = false;

        player.comboCount = 1;
        player.canBasicAttack = true;
        player.canMove = true;

    }
    private void BasicAttack1End()
    {
        player.comboCount = 1;
        player.canBasicAttack = true;
        player.canMove = true;
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
        Effect[combonum].SetActive(true);
        basicAttack.SetActive(true);
    }
    private void AttackEnd()
    {
        Effect[0].SetActive(false);
        Effect[1].SetActive(false);
        basicAttack.SetActive(false);
    }

    //Dash
    private void CanDash()
    {
        player.canDash = true;
    }
    private void CheckDeshError()
    {
        combonum = 1;
        player.dashError = true;
    }
}
