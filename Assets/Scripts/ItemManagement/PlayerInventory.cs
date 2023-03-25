using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject Inventory;
    [SerializeField] Image ItemPrefeb;
    [SerializeField] private DropTable dropTable;
    [SerializeField] public GameObject itemMenu;

    public Image[,] slot = new Image[2, 8];
    private int[] standbyItem = new int[14];
    private int standbySlot = 14;
    public bool isopen;
    private PlayerMove player;

    private string[] itemInfo = new string[14];
    private Vector3[] infoPos = new Vector3[14];
    private Vector2[] infoSize = new Vector2[14];

    private int bildingindex = 20;


    private void Awake()
    {
        Inventory.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        isopen = false;
    }

    void Start()
    {
        player = GameObject.Find("Character").GetComponent<PlayerMove>();
        for (int i = 0; i < 14; i++)
        {
            standbyItem[i] = 666;
        }
    }

    void Update()
    {
        if (!isopen)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                player.isUI = true;
                Inventory.SetActive(true);
                itemMenu.SetActive(false);
                isopen = true;
                ItemInit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
            {
                player.isUI = false;
                Inventory.SetActive(false);
                isopen = false;
            }
        }
    }

    public void ItemMenu()
    {
        itemMenu.SetActive(true);
    }

    public void ItemInfoStnadby(string infotxt, Vector2 infosize, Vector3 infopos)
    {
        for (int i = 0; i < 14; i++)
        {
            if (itemInfo[i] == null)
            {
                itemInfo[i] = infotxt;
                infoPos[i] = infopos;
                infoSize[i] = infosize;
                break;
            }
        }
    }

    public void ItemStandby(int itemid)
    {
        if (standbySlot != 0)
        {
            for (int i = 0; i < 14; i++)
            {
                if (standbyItem[i] == 666)
                {
                    standbyItem[i] = itemid;
                    standbySlot--;
                    break;
                }
            }
        }
        for (int i = 0; i < 14; i++)
        {
            Debug.Log(standbyItem[i]);
        }
    }

    private void ItemInit()
    {
        if (standbySlot != 0)
        {
            for (int i = 0; i < 14; i++)
            {
                if (standbyItem[i] != 666)
                {
                    slotInstance(standbyItem[i]);
                    Debug.Log(standbyItem[i]);
                }
                else
                {
                    break;
                }
            }
        }
        for (int i = 0; i < 14; i++)
        {
            standbyItem[i] = 666;
        }
    }

    public void slotInstance(int itemId)
    {
        float posx = -270.8f;
        float posy = -288.3f;
        bool isIn = false;

        for (int i = 0; i < 2; i++)
        {

            for (int j = 0; j < 7; j++)
            {
                if (slot[i, j] == null)
                {
                    Debug.Log("itemIn");
                    isIn = true;                 
                    slot[i, j] = Instantiate(ItemPrefeb);
                    slot[i, j].gameObject.transform.SetParent(GameObject.Find("InventorySlot").transform, false);                    
                    slot[i, j].gameObject.GetComponent<RectTransform>().localPosition = new Vector3(posx, posy, 0);
                    slot[i, j].sprite = dropTable.itemImg[itemId];
                    slot[i, j].gameObject.GetComponent<Slot>().itemId = itemId;
                    slot[i, j].gameObject.GetComponent<Slot>().bildingindex = bildingindex;
                    bildingindex--;

                    if (i == 0)
                    {
                        var curslot = slot[i, j].gameObject.GetComponent<Slot>();
                        curslot.infoTxt.text = itemInfo[j];
                        curslot.image.rectTransform.localPosition = infoPos[j];
                        curslot.image.rectTransform.sizeDelta = infoSize[j];
                    }
                    else
                    {
                        var curslot = slot[i, j].gameObject.GetComponent<Slot>();
                        curslot.infoTxt.text = itemInfo[7 + j];
                        curslot.image.rectTransform.localPosition = infoPos[7 + j];
                        curslot.image.rectTransform.sizeDelta = infoSize[7 + j];
                    }

                    break;
                }
                posx += 90;
            }
            if (isIn)
            {
                break;
            }
            posx = -270.8f;
            posy -= 93;
        }

        

    }
}
