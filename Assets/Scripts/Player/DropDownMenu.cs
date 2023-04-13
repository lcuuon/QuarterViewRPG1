using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] TMP_Text atk;
    [SerializeField] TMP_Text criProb;
    [SerializeField] TMP_Text criDmg;
    [SerializeField] TMP_Text MoveSpd;
    [SerializeField] TMP_Text CDR;
    [SerializeField] TMP_Text atkSpd;

    PlayerLevelManager playerState;

    private bool isdown = false;
    private float posy;
    RectTransform rect;

    void Start()
    {
        playerState = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        atk.text = ($"+ {playerState.AtkDamage}");
        criProb.text = ($"{playerState.criticalProb}%");
        criDmg.text = ($"+ {playerState.criticalDmg}");
        MoveSpd.text = ($"{playerState.MoveSpeed}");
        CDR.text = ($"{playerState.CDR}%");
        atkSpd.text = ($"{playerState.AtkSpeed}");
    }

    public void DropDownButton()
    {
        StartCoroutine( DropDown());
    }

    IEnumerator DropDown()
    {
        if ( isdown )
        {
            Debug.Log("aa");
            posy = rect.localPosition.y;
            while(true)
            {
                posy += 5f;
                rect.localPosition = new Vector2(rect.localPosition.x, posy);
                yield return new WaitForSeconds(0.0001f);
                if (posy >= 175)
                    break;
            }
            isdown = false;
        }
        else
        {
            Debug.Log("aa");
            posy = rect.localPosition.y;
            while(true)
            {
                posy -= 5f;
                rect.localPosition = new Vector2(rect.localPosition.x, posy);
                yield return new WaitForSeconds(0.0001f);
                if (posy <= 3)
                    break;
            }
            isdown = true;
        }
    }
}
