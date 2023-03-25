using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{
    Slider hpbar;
    PlayerLevelManager levelmanager;

    void Start()
    {
        hpbar = gameObject.GetComponent<Slider>();
        levelmanager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
    }

    void Update()
    {
        hpbar.maxValue = levelmanager.MaxHP;
    }
}
