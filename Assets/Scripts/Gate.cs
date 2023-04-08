using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField] string SceneName;
    [SerializeField] Vector3 playerPos;
    PopUpTxt text;

    GameManager gm;

    private bool isin;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        text = GameObject.Find("PopUptxt").GetComponent<PopUpTxt>();
    }

    void Update()
    {
        if (isin)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isin = false;
                gm.StartCoroutine(gm.FadeOut(2));
                text.TextDown();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isin = true;
            text.TextUp(1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isin = false;
            text.TextDown();
        }
    }
}
