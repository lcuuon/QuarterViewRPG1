using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    PlayerLevelManager levelManager;
    Item_Data data;
    [SerializeField] private PlayerInventory inventory;

    public int itemId;

    private bool iswear;
    private bool isInstance = true;

    void Start()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        
    }

    void Update()
    {
        if (isInstance)
        {
            isInstance = false;
            data = Item_DataManager.GetInstance().LoadDatas(itemId);
        }
    }

    private void ItemWear()
    {
        if (!iswear)
        {
            iswear = true;
            levelManager.MaxHP += data.HP;
            levelManager.AtkDamage += data.atk;
            levelManager.AtkSpeed += data.attackSpeed;
            levelManager.CDR += data.CDR;
            levelManager.criticalProb += data.criticalProb;
            levelManager.criticalDmg += data.criticalDmg;

        }
        else
        {
            iswear = false;
            levelManager.MaxHP -= data.HP;
            levelManager.AtkDamage -= data.atk;
            levelManager.AtkSpeed -= data.attackSpeed;
            levelManager.CDR -= data.CDR;
            levelManager.criticalProb -= data.criticalProb;
            levelManager.criticalDmg -= data.criticalDmg;
        }
    }
}
