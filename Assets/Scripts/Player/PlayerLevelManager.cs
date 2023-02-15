using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelManager : MonoBehaviour
{
    [Header("Level")]
    public float MaxExp;
    public float CurExp;
    public float Level;

    [Header("Stats")]
    public int statPoint;
    public float MaxHP;
    public float AtkDamage;
    public float AtkSpeed;
    public float MoveSpeed;
    public float CDR;

     
    [Header("Stats_Level_Info")]
    public int MaxHPLv = 1;
    public int AtkDamageLv = 1;
    public int AtkSpeedLv = 1;
    public int MoveSpeedLv = 1;
    public int CDRLv = 1;

    public Slider expBar;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Level = 1;
        MaxExp = 100;
        if (expBar!= null)
            expBar.value = 0;
    }

    void Update()
    {
        if (expBar != null)
            expBar.value = CurExp;
        if (CurExp >= MaxExp)
        {
            Level++;
            MaxExp += MaxExp * 0.2f;
            expBar.maxValue = MaxExp;
            CurExp = 0;
            if (Level % 3 == 0)
            {
                statPoint += 5;
            }
            else
            {
                statPoint += 3;
            }
        }
    }
}
