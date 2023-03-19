using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject Inventory;
    [SerializeField] Image ItemPrefeb;
    [SerializeField] private DropTable dropTable;

    public Image[,] slot = new Image[2, 8];
    private int[] standbyItem = new int[16];
    private int standbySlot = 16;
    private bool isopen;
    private PlayerMove player;


    private void Awake()
    {
        Inventory.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        isopen = false;
    }

    void Start()
    {
        player = GameObject.Find("Character").GetComponent<PlayerMove>();
        for (int i = 0; i < 16; i++)
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

    public void ItemStandby(int itemid)
    {
        if (standbySlot != 0)
        {
            for (int i = 0; i < 16; i++)
            {
                if (standbyItem[i] == 666)
                {
                    standbyItem[i] = itemid;
                    standbySlot--;
                    break;
                }
            }
        }
        for (int i = 0; i < 16; i++)
        {
            Debug.Log(standbyItem[i]);
        }
    }

    private void ItemInit()
    {
        if (standbySlot != 16)
        {
            for (int i = 0; i < 16; i++)
            {
                if (standbyItem[i] != 666)
                {
                    slotInstance(standbyItem[i]);
                    Debug.Log(standbyItem[i]);
                }
            }
        }
        for (int i = 0; i < 16; i++)
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
                    isIn = true;                 
                    slot[i, j] = Instantiate(ItemPrefeb);
                    slot[i, j].gameObject.transform.SetParent(GameObject.Find("InventorySlot").transform, false);                    
                    slot[i, j].gameObject.GetComponent<RectTransform>().localPosition = new Vector3(posx, posy, 0);
                    slot[i, j].sprite = dropTable.itemImg[itemId];
                    slot[i, j].gameObject.GetComponent<Slot>().itemId = itemId;
                    Debug.Log(posx);
                    Debug.Log(posy);
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
