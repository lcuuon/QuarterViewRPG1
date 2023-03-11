using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Items
{
    public List<Item_Data> Info;
}


[System.Serializable]
public class Item_Data
{
    public int id;
    public string name;
}
