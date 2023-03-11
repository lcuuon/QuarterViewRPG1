using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{

    private Image blackScreen;
    GameObject player;
    PlayerMove playerCs;
    PlayerLevelManager levelManager;
    TMP_Text Hp_Value;
    private int curHp;

    [SerializeField] private float HP;

    private void Awake()
    {
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(blackScreen.gameObject);
        blackScreen.color = new Color(0, 0, 0, 0);
    }

    void Start()
    {
        Item_DataManager.GetInstance().LoadDatas();
        //var data = Item_DataManager.GetInstance().dicItemDatas[1];
        //Debug.LogFormat("{0}, {1}", data.id, data.name);
    }

    void Update()
    {
        if (Hp_Value != null)
        {
            curHp = (int)playerCs.PlayerCurHP;
            Hp_Value.text = ($"{curHp} / {levelManager.MaxHP}");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Loading")
        {
            Hp_Value = GameObject.Find("HPValue").GetComponent<TMP_Text>();
            levelManager.expBar = GameObject.Find("ExpSlider").GetComponent<Slider>();
        }
        if(levelManager.expBar != null)
            levelManager.expBar.maxValue = levelManager.MaxExp;
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerCs = player.GetComponentInChildren<PlayerMove>();
            if (scene.name == "Start_Village")
            {
                Debug.Log("re");
                playerCs.PlayerCurHP = levelManager.MaxHP;
            }
            else
            {
                Invoke("HPupdate", 0.1f);
            }
        }
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        StartCoroutine("FadeIn");
    }

    private void HPupdate()
    {
        playerCs.PlayerCurHP = HP;
        Debug.Log(playerCs.gameObject.name);
        Debug.Log(HP);
    }

    public void GameStart()
    {
        Debug.Log("Click");
        StartCoroutine(FadeOut("Start_Village"));
    }

    public IEnumerator FadeOut(string sceneName)
    {
        blackScreen.gameObject.SetActive(true);
        float fadeCount = 0;
        while(fadeCount <= 1)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackScreen.color = new Color(0, 0, 0, fadeCount);
        }
        if(player != null)
        {
                HP = playerCs.PlayerCurHP;
                //Debug.Log(HP);
        }
        SceneManager.LoadScene(sceneName);
        
        //blackScreen.gameObject.SetActive(false);
    }
    IEnumerator FadeIn()
    {
        blackScreen.gameObject.SetActive(true);
        float fadeCount = 1;
        while (fadeCount >= 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            blackScreen.color = new Color(0, 0, 0, fadeCount);
        }
        blackScreen.gameObject.SetActive(false);
    }
}
