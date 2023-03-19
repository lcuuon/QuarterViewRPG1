using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int itemId;
    private PlayerInventory inventory;
    [SerializeField] private DropTable dropTable;
    [SerializeField] private GameObject Info;

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

    public void OnPointerExit(PointerEventData eventData)
    {
        Info.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Info.SetActive(true);
    }
}
