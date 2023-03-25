using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropTable : MonoBehaviour
{
    [SerializeField] GameObject Droptable;
    [SerializeField] public Sprite[] itemImg;
    [SerializeField] Button item1b;
    [SerializeField] Button item2b;
    

    private PlayerMove player;
    private GetItem button1;
    private GetItem button2;

    private bool isopen;


    private void Awake()
    {
        Droptable.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        isopen = false;
    }

    void Start()
    {
        button1 = item1b.gameObject.GetComponent<GetItem>();
        button2 = item2b.gameObject.GetComponent<GetItem>();
        player = GameObject.Find("Character").GetComponent<PlayerMove>();
    }

    void Update()
    {

        if (!isopen)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                player.isUI = true;
                Droptable.SetActive(true);
                isopen = true;
                ItemLoad();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape))
            {
                closeTab();
            }
        }
        
        
    }

    public void closeTab()
    {
        player.isUI = false;
        Droptable.SetActive(false);
        isopen = false;
    }

    private void ItemLoad()
    {
        int item1 = Random.Range(0, 9);
        int item2 = Random.Range(0, 9);


        while (item1 == item2)
        {
            item2 = Random.Range(0, 9);
        }
        button1.itemId = item1;
        button2.itemId = item2;
        button1.ItemReset();
        button2.ItemReset();

        item1b.image.sprite = itemImg[item1];
        item2b.image.sprite = itemImg[item2];
    }
}
