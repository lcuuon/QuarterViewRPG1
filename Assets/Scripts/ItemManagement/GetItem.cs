using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class GetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int itemId;
    private PlayerInventory inventory;
    [SerializeField] private DropTable dropTable;
    [SerializeField] private GameObject InfoTab;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text info;
    [SerializeField] private Image image;

    void Start()
    {
        inventory = GameObject.Find("InventoryP").GetComponent<PlayerInventory>();
    }

    void Update()
    {
        
    }

    public void Click()
    {
        inventory.ItemStandby(itemId);
        dropTable.closeTab();
    }

    public void ItemReset()
    {
        var data = Item_DataManager.GetInstance().LoadDatas(itemId);
        itemName.text = data.name;
        info.text = ("");
        bool isn = false;

        if (data.HP != 0)
        {
            if (!isn)
            {
                info.text += ($"{data.HP}");
                isn = true;
            }
        }
        if (data.atk != 0)
        {
            if (!isn)
            {
                info.text += ($"{data.atk}");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n{data.atk}");
            }
        }
        if (data.attackSpeed != 0)
        {
            if (!isn)
            {
                info.text += ($"{data.attackSpeed}");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n{data.attackSpeed}");
            }
        }
        if (data.criticalDmg != 0)
        {
            if (!isn)
            {
                info.text += ($"{data.criticalDmg}");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n{data.criticalDmg}");
            }
        }
        if (data.criticalProb != 0)
        {
            if (!isn)
            {
                info.text += ($"{data.criticalProb}");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n{data.criticalProb}");
            }
        }

        if (data.CDR != 0)
        {
            if (!isn)
            {
                info.text += ($"{data.CDR}");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n{data.CDR}");
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoTab.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {      
        InfoTab.SetActive(true);
    }
}
