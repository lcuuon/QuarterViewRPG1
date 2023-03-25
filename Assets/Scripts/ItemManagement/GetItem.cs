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

    private string iteminfo;
    private Vector3 infoPos;
    private Vector2 infoSize;

    void Start()
    {
        inventory = GameObject.Find("InventoryP").GetComponent<PlayerInventory>();
        InfoTab.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Click()
    {
        inventory.ItemStandby(itemId);
        dropTable.closeTab();
        inventory.ItemInfoStnadby(iteminfo, infoSize, infoPos);
    }

    public void ItemReset()
    {
        image.rectTransform.localPosition = new Vector3(173.7f, 10.4f, 0);
        image.rectTransform.sizeDelta = new Vector2(215.9544f, 120.4526f);
        var data = Item_DataManager.GetInstance().LoadDatas(itemId);
        itemName.text = data.name;
        info.text = ("");
        bool isn = false;

        if (data.HP != 0)
        {
            if (!isn)
            {
                info.text += ($"ü��+    {data.HP}");
                isn = true;
            }
        }
        if (data.atk != 0)
        {
            if (!isn)
            {
                info.text += ($"���ݷ�+    {data.atk}");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n���ݷ�+    {data.atk}");
            }
        }
        if (data.attackSpeed != 0)
        {
            if (!isn)
            {
                info.text += ($"���ݼӵ�+    {data.attackSpeed}%");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n���ݼӵ�+    {data.attackSpeed}%");
            }
        }
        if (data.criticalDmg != 0)
        {
            if (!isn)
            {
                info.text += ($"ġ��Ÿ ������+     {data.criticalDmg}");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\nġ��Ÿ ������+    {data.criticalDmg}");
            }
        }
        if (data.criticalProb != 0)
        {
            if (!isn)
            {
                info.text += ($"ġ��Ÿ Ȯ��+    {data.criticalProb}%");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\nġ��Ÿ Ȯ��+    {data.criticalProb}%");
            }
        }

        if (data.CDR != 0)
        {
            if (!isn)
            {
                info.text += ($"��Ÿ�Ӱ���    {data.CDR}%");
                isn = true;
            }
            else
            {
                image.rectTransform.localPosition += new Vector3(0, -10, 0);
                image.rectTransform.sizeDelta += new Vector2(0, +20);
                info.text += ($"\n��Ÿ�Ӱ���    {data.CDR}%");
            }
        }
        iteminfo = info.text;
        infoPos = image.rectTransform.localPosition;
        infoSize = image.rectTransform.sizeDelta;
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
