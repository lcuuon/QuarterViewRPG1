using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class StatsTab : MonoBehaviour
{
    [Header("TMP")]
    [SerializeField] TMP_Text CurPoint;
    [SerializeField] TMP_Text Level;
    [SerializeField] TMP_Text[] LevelCount = new TMP_Text[5];
    [SerializeField] TMP_Text[] statPluse_tmp = new TMP_Text[5];
    [SerializeField] TMP_Text[] reqPoint_tmp = new TMP_Text[5];
    
    [Header("StatTab")]
    [SerializeField] GameObject statTab;
    [SerializeField] Image pressF;

    private PlayerMove player;
    private float[] statPluse = new float[5];
    private int[] reqPoints = new int[5];
    private bool iszone;
    private bool statTabOpen = false;
    PlayerLevelManager levelManager;
    SkillCoolTime coolTime;

    private int reqPoint = 5;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            reqPoints[i] = 5;
        }
        statTab.SetActive(false);
        player = GameObject.Find("Character").GetComponent<PlayerMove>();
        coolTime = GameObject.Find("Skill1").GetComponent<SkillCoolTime>();
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        statPluse[0] = 0;
        statPluse[1] = 0;
    }

    void Update()
    {
        if (!iszone)
            return;
        CurPoint.text = ($"{levelManager.statPoint}P");
        Level.text = ($"Lv {levelManager.Level}");
        if (Input.GetKeyDown(KeyCode.F) && !statTabOpen)
        {
            Debug.Log("iszone");
            player.isUI = true;
            statTab.SetActive(true);
            Invoke("closeTab", 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.F) && statTabOpen)
        {
            player.isUI = false;
            statTab.SetActive(false);
            statTabOpen = false;
        }
    }

    private void closeTab()
    {
        statTabOpen = true;
    }

    public void MaxHpUp()
    {
        Debug.Log("Click");
        if (levelManager.MaxHPLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.MaxHPLv));
        if (levelManager.MaxHPLv < 10)
        {
            if (levelManager.statPoint >= reqPoint)
            {
                levelManager.MaxHPLv++;
                levelManager.statPoint -= reqPoint;
                levelManager.MaxHP += 100;
                statPluse[0] += 100;
                reqPoints[0] = 5 * (int)(Math.Pow(2, levelManager.MaxHPLv));
                player.PlayerCurHP = levelManager.MaxHP;
                StatTabSet();
            }
        }
    }

    public void AtkDamageUp()
    {
        if (levelManager.AtkDamageLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.AtkDamageLv));
        if (levelManager.AtkDamageLv < 20)
        {
            if (levelManager.statPoint >= reqPoint)
            {
                levelManager.AtkDamageLv++;
                levelManager.statPoint -= reqPoint;
                levelManager.AtkDamage += 5;
                statPluse[1] += 5;
                reqPoints[1] = 5 * (int)(Math.Pow(2, levelManager.AtkDamageLv));
                StatTabSet();
            }
        }
    }

    public void AtkSpeedUp()
    {
        if (levelManager.AtkSpeedLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.AtkSpeedLv));
        if (levelManager.AtkSpeedLv < 10)
        {
            if (levelManager.statPoint >= reqPoint)
            {
                levelManager.AtkSpeedLv++;
                levelManager.statPoint -= reqPoint;
                levelManager.AtkSpeed += levelManager.AtkSpeed * 0.05f;
                statPluse[2] += 5;
                reqPoints[2] = 5 * (int)(Math.Pow(2, levelManager.AtkSpeedLv));
                StatTabSet();
            }
        }
    }
    public void MoveSpeedUp()
    {
        if (levelManager.MoveSpeedLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.MoveSpeedLv));
        if (levelManager.MoveSpeedLv < 10)
        {
            if (levelManager.statPoint >= reqPoint)
            {
                levelManager.MoveSpeedLv++;
                levelManager.statPoint -= reqPoint;
                levelManager.MoveSpeed += levelManager.MoveSpeed * 0.1f;
                statPluse[3] += 10;
                reqPoints[3] = 5 * (int)(Math.Pow(2, levelManager.MoveSpeedLv));
                StatTabSet();
            }
        }       
    }
    public void CDRUp()
    {
        if (levelManager.CDRLv > 0)
            reqPoint = 5 * (int)(Math.Pow(2, levelManager.CDRLv));
        if (levelManager.CDRLv < 10)
        {
            if (levelManager.statPoint >= reqPoint)
            {
                levelManager.CDRLv++;
                levelManager.statPoint -= reqPoint;
                levelManager.CDR += 5;
                statPluse[4] += 5;
                reqPoints[4] = 5 * (int)(Math.Pow(2, levelManager.CDRLv));
                coolTime.CoolTimeReset();
                StatTabSet();
            }
        }
    }

    private void StatTabSet()
    {
        for (int i = 0; i < 5; i++)
        {   
            int value1 = (int)statPluse[i];
            int txt3;
            switch (i)
            {
                case 0:
                    txt3 = levelManager.MaxHPLv;
                    LevelCount[i].text = ($"Lv {txt3}");
                    break;
                case 1:
                    txt3 = levelManager.AtkDamageLv;
                    LevelCount[i].text = ($"Lv {txt3}");
                    break;
                case 2:
                    txt3 = levelManager.AtkSpeedLv;
                    LevelCount[i].text = ($"Lv {txt3}");
                    break;
                case 3:
                    txt3 = levelManager.MoveSpeedLv;
                    LevelCount[i].text = ($"Lv {txt3}");
                    break;
                case 4:
                    txt3 = levelManager.CDRLv;
                    LevelCount[i].text = ($"Lv {txt3}");
                    break;
            }
            if (i > 1)
            {
                statPluse_tmp[i].text = ($"+ {value1}%");
            }
            else
            {
                statPluse_tmp[i].text = ($"+ {value1}");
            }
            reqPoint_tmp[i].text = ($"- {reqPoints[i]}P");


        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            iszone = true;
            pressF.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            iszone = false;
            pressF.gameObject.SetActive(false);
        }
    }


}
