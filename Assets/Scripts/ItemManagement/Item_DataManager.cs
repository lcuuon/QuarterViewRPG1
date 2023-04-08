using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class Item_DataManager
{
    private static Item_DataManager instance;
    private Item_DataManager()
    {

    }

    public static Item_DataManager GetInstance()
    {
        if (Item_DataManager.instance == null)
        {
            Item_DataManager.instance = new Item_DataManager();
        }
        return Item_DataManager.instance;
    }

    public Item_Data LoadDatas(int reqItem)
    {
        var json = Resources.Load<TextAsset>("Datas/itemData").text;
        var arrItemDatas = JsonConvert.DeserializeObject<Item_Data[]>(json);
        var data = arrItemDatas[reqItem];

        return data;
    }
}
