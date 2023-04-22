using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] OverlapCheck overlap;
    private Image blackScreen;
    GameObject player;
    PlayerMove playerCs;
    PlayerLevelManager levelManager;
    TMP_Text Hp_Value;
    UIcanvas canvas;
    private int curHp;
    public bool sceneChange = false;
    private string curMap;

    [SerializeField] private float HP;

    private void Awake()
    {
        canvas = GameObject.Find("uicanvas").GetComponent<UIcanvas>();
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(blackScreen.gameObject);
        blackScreen.color = new Color(0, 0, 0, 0);
    }

    void Start()
    {
        
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
        Debug.Log(scene.name);
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
                playerCs.PlayerCurHP = levelManager.MaxHP;
            }
            else
            {
                sceneChange = true;
                Invoke("HPupdate", 0.1f);
            }
        }
        if (scene.name != "ForestLoading")
        {
            blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
            StartCoroutine("FadeIn");
        }
        else
        {
            Loading loading = GameObject.Find("Loading").GetComponent<Loading>();
            StartCoroutine(loading.LoadAsynScene(curMap));
        }
    }

    private void HPupdate()
    {
        playerCs.PlayerCurHP = HP;
    }

    public void GameStart()
    {
        StartCoroutine(FadeOut(0));
    }

    public IEnumerator FadeOut(int stageid)
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
        }
        var data = MapManager.GetInstance().LoadData(stageid);
        if (stageid <= 1)
        {
            Debug.Log(data.Length);
            SceneManager.LoadScene(data[0].MapName);
        }
        else
        {
            int random = Random.Range(0, data.Length);
            while (data[random] == null)
            {
                random = Random.Range(0, data.Length);
            }
            curMap = data[random].MapName;
            SceneManager.LoadScene("ForestLoading");
        }
        if (player != null)
            playerCs.NavClear();
        
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
        sceneChange = false;
        if (player != null)
            playerCs.NavSet();
        blackScreen.gameObject.SetActive(false);
        canvas.Setup();
        overlap.overlapCheck();
    }
}
