using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            var data = Item_DataManager.GetInstance().LoadDatas();

        }
    }
}
