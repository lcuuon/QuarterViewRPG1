using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    PlayerLevelManager levelManager;
    Item_Data data;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject Info;

    public int itemId;

    private bool iswear;

    void Start()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        data = Item_DataManager.GetInstance().LoadDatas(itemId);

    }

    void Update()
    {

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("RightClick");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("out");
    }
}
