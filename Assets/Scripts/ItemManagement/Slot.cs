using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    PlayerLevelManager levelManager;
    Item_Data data;
    private PlayerInventory inventory;
    [SerializeField] private GameObject Info;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] public TMP_Text infoTxt;
    [SerializeField] public Image image;
    RectTransform rectT;

    public int bildingindex;

    GameObject itemMenu;
    
    public int itemId;
    public int itemRegist;

    //private bool canInfo;

    void Start()
    {
        rectT= GetComponent<RectTransform>();
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        inventory = GameObject.Find("InventoryP").GetComponent<PlayerInventory>();
        data = Item_DataManager.GetInstance().LoadDatas(itemId);
        itemName.text = data.name;
        Info.gameObject.SetActive(false);
        itemRegist = 66;
        itemMenu = inventory.itemMenu;
        rectT.SetAsFirstSibling();
    }

    void Update()
    {
        

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Info.gameObject.SetActive(false);
            //inventory.ItemMenu();
            itemMenu.gameObject.SetActive(true);
            itemMenu.transform.SetAsLastSibling();
            var menu = itemMenu.GetComponent<ItemClickMenu>();
            menu.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(rectT.localPosition.x + 269.8f, rectT.localPosition.y + 286.5f, 0);
            menu.itemId = itemId;
            if (itemRegist == 66)
            {
                for (int i = 0; i < 19; i++)
                {
                    if (menu.itemRegist[i] == 0)
                    {
                        itemRegist = i;
                        menu.itemRegist[i] = 1;
                        menu.curSlot = i;
                        break;
                    }
                }
            }
            else
            {
                menu.curSlot = itemRegist;
            }
            if (menu.itemRegist[itemRegist] == 1)
            {
                menu.text.text = ("> Âø¿ë");
            }
            else if (menu.itemRegist[itemRegist] == 2)
            {
                menu.text.text = ("> Âø¿ë ÇØÁ¦");
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Info.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Info.gameObject.SetActive(false);
    }
}
