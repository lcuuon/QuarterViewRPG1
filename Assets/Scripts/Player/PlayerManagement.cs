using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    GameManager gm;
    PlayerMove player;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GetComponentInChildren<PlayerMove>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (gm.sceneChange)
        {
            transform.localPosition = new Vector3(-74.3799973f, 0, 72.25f);
            player.transform.localPosition = Vector3.zero;
        }

    }
}
