using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public int itemId;
    private PlayerInventory inventory;
    [SerializeField] private DropTable dropTable;

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


}
