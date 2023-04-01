using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField] string SceneName;
    [SerializeField] Vector3 playerPos;
    [SerializeField] Image image;
    

    GameManager gm;

    private bool isin;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        image.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isin)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isin = false;
                image.gameObject.SetActive(false);
                gm.StartCoroutine(gm.FadeOut(SceneName));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isin = true;
            image.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isin = false;
            image.gameObject.SetActive(false);
        }
    }
}
