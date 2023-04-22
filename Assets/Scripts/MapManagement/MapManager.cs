using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    private MapManager()
    {

    }

    public static MapManager GetInstance()
    {
        if (MapManager.Instance == null)
        {
            MapManager.Instance = new MapManager();
        }
        return MapManager.Instance;
    }

    public MapData[] LoadData(int stageid)
    {
        var json = Resources.Load<TextAsset>("Datas/MapData").text;
        var array = JsonConvert.DeserializeObject<MapData[]>(json);
        MapData[] data = new MapData[array.Length];
        int j = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Stageid == stageid)
            {
                data[j] = array[i];
                Debug.Log(array[i].MapName);
                j++;
            }
        }
        return data;
    }
}
