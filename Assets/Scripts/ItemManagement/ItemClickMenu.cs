using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemClickMenu : MonoBehaviour
{
    [SerializeField] public TMP_Text text;

    private PlayerInventory inventory;
    private PlayerLevelManager levelManager;
    private PlayerMove player;
    public bool[] isWear = new bool[19];
    public int[] itemRegist = new int[19];
    public int curSlot;
    public int itemId;

    void Start()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        inventory = GameObject.Find("InventoryP").GetComponent<PlayerInventory>();
        player = GameObject.Find("Character").GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void WearButton()
    {
        ItemWear();
        inventory.slotchange(curSlot, isWear[curSlot]);
        this.gameObject.SetActive(false);
    }

    private void ItemWear()
    {
        var data = Item_DataManager.GetInstance().LoadDatas(itemId);

        if (!isWear[curSlot])
        {
            float ratio = player.PlayerCurHP / levelManager.MaxHP;
            itemRegist[curSlot] = 2;
            isWear[curSlot] = true;
            levelManager.MaxHP += data.HP;
            player.PlayerCurHP = ratio * levelManager.MaxHP;
            levelManager.AtkDamage += data.atk;
            levelManager.AtkSpeed += 1 * (data.attackSpeed * 0.01f);
            levelManager.CDR += data.CDR;
            levelManager.criticalProb += data.criticalProb;
            levelManager.criticalDmg += data.criticalDmg;

        }
        else
        {
            float ratio = player.PlayerCurHP / levelManager.MaxHP;
            itemRegist[curSlot] = 1;
            isWear[curSlot] = false;
            levelManager.MaxHP -= data.HP;
            player.PlayerCurHP = ratio * levelManager.MaxHP;
            levelManager.AtkDamage -= data.atk;
            levelManager.AtkSpeed -= 1 * (data.attackSpeed * 0.01f);
            levelManager.CDR -= data.CDR;
            levelManager.criticalProb -= data.criticalProb;
            levelManager.criticalDmg -= data.criticalDmg;
        }
    }
}
