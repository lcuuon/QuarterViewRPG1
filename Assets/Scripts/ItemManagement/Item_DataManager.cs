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

    public Item_Data LoadDatas()
    {
        var json = Resources.Load<TextAsset>("Datas/itemData").text;
        var arrItemDatas = JsonConvert.DeserializeObject<Item_Data[]>(json);
        int randomItem = Random.RandomRange(0, 9);
        var data = arrItemDatas[randomItem];
        Debug.LogFormat("id : {0}, name : {1}, atk : {2}, movespeed : {3}, criticalProb : {4}, criticalDmg : {5}, CDR : {6}, HP : {7}, attackSpeed : {8}",
            data.id, data.name, data.atk, data.movespeed, data.criticalProb, data.criticalDmg, data.CDR, data.HP, data.attackSpeed);
        return data;
        
        //foreach (var data in arrItemDatas)
        //{
        //    Debug.LogFormat("id : {0}, name : {1}, atk : {2}, movespeed : {3}, criticalProb : {4}, criticalDmg : {5}, CDR : {6}, HP : {7}, attackSpeed : {8}",
        //    data.id, data.name, data.atk, data.movespeed, data.criticalProb, data.criticalDmg, data.CDR, data.HP, data.attackSpeed);
        //}
    }




}
