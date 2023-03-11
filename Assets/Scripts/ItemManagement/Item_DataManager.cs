using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class Item_DataManager
{
    private static Item_DataManager instance;
    //public Dictionary<int, Item_Data> dicItemDatas;
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

    public void LoadDatas()
    {
        var json = Resources.Load<TextAsset>("Datas/itemData").text;
        var arrItemDatas = JsonConvert.DeserializeObject<Items[]>(json);
        foreach (var data in arrItemDatas)
        {
            Debug.LogFormat("{0}, {1}", data.id, data.name);
        }
        //this.dicItemDatas = arrItemDatas.ToDictionary(x => x.id);
    }
    



}
