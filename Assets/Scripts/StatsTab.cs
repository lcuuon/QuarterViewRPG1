using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StatsTab : MonoBehaviour
{
    PlayerLevelManager levelManager;

    private int reqPoint = 5;

    void Start()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CDRUp();
        }
    }

    public void MaxHpUp()
    {
        if (levelManager.MaxHPLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.MaxHPLv));
        if (levelManager.statPoint >= reqPoint)
        {
            levelManager.MaxHPLv++;
            levelManager.statPoint -= reqPoint;
            levelManager.MaxHP += 100;
        }
    }

    public void AtkDamageUp()
    {
        if (levelManager.AtkDamageLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.AtkDamageLv));
        if (levelManager.statPoint >= reqPoint)
        {
            levelManager.AtkDamageLv++;
            levelManager.statPoint -= reqPoint;
            levelManager.AtkDamage += 5;
        }
    }

    public void AtkSpeedUp()
    {
        if (levelManager.AtkSpeedLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.AtkSpeedLv));
        if (levelManager.statPoint >= reqPoint)
        {
            levelManager.AtkSpeedLv++;
            levelManager.statPoint -= reqPoint;
            levelManager.AtkSpeed += levelManager.AtkSpeed * 0.05f;
        }
    }
    public void MoveSpeedUp()
    {
        if (levelManager.MoveSpeedLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.MoveSpeedLv));
        if (levelManager.statPoint >= reqPoint)
        {
            levelManager.MoveSpeedLv++;
            levelManager.statPoint -= reqPoint;
            levelManager.MoveSpeed += levelManager.MoveSpeed * 0.1f;
        }
    }
    public void CDRUp()
    {
        if (levelManager.CDRLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.CDRLv));
        if (levelManager.statPoint >= reqPoint)
        {
            levelManager.CDRLv++;
            levelManager.statPoint -= reqPoint;
            levelManager.CDR += levelManager.CDR * 0.2f;
            Debug.Log(levelManager.CDR);
        }
    }


}
