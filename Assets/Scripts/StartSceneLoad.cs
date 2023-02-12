using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneLoad : MonoBehaviour
{

    private Image blackScreen;
    GameObject canvas;
    GameObject player;
    PlayerMove playerCs;

    [SerializeField] private float HP;

    private void Awake()
    {
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

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerCs = player.GetComponentInChildren<PlayerMove>();
            Invoke("HPupdate", 0.1f);
        }
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        canvas = GameObject.Find("Canvas");
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
        StartCoroutine(FadeOut("Start_Village", new Vector3(11, 0, 187)));
    }

    public IEnumerator FadeOut(string sceneName, Vector3 characterPos)
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
