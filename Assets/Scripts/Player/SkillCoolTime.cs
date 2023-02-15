using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    [SerializeField] TMP_Text textCoolT;
    [SerializeField] public Image fillImg;
    [SerializeField] float cooltime;
    [SerializeField] PlayerMove player;
    PlayerLevelManager levelManager;
    private float curtime;
    private float timeStart;
    private bool isEnded = true;
    private float[] tempCooltime = new float[4];

    void Start()
    {
        tempCooltime[0] = cooltime;
        levelManager = GameObject.Find("PlayerCurState").GetComponent<PlayerLevelManager>();
        CoolTimeReset();
        Debug.Log(cooltime);
        Init_UI();
        fillImg.fillAmount = 0;
    }

    void Update()
    {
        if (isEnded)
            return;
        Check_CoolTime();
    }

    public void CoolTimeReset()
    {
        cooltime = tempCooltime[0];
        cooltime -= tempCooltime[0] * (levelManager.CDR / 100);
    }

    private void Init_UI()
    {
        fillImg.type = Image.Type.Filled;
        fillImg.fillMethod = Image.FillMethod.Radial360;
        fillImg.fillOrigin = (int)Image.Origin360.Top;
        fillImg.fillClockwise = false;
    }

    private void Check_CoolTime()
    {
        curtime = Time.time - timeStart;
        if (curtime < cooltime)
        {
            Set_FillAmount(cooltime - curtime);
        }
        else if (!isEnded)
        {
            EndCoolTime();
        }
    }

    private void EndCoolTime()
    {
        Set_FillAmount(0);
        isEnded = true;
        textCoolT.gameObject.SetActive(false);
        player.canDash = true;
    }

    public void Trigger_Skill()
    {
        Reset_CollTime();
        player.canDash = false;
    }

    private void Reset_CollTime()
    {
        textCoolT.gameObject.SetActive(true);
        curtime = cooltime;
        timeStart = Time.time;
        Set_FillAmount(cooltime);
        isEnded = false;
    }

    private void Set_FillAmount(float value)
    {
        fillImg.fillAmount = value/cooltime;
        string txt = value.ToString("0.0");
        textCoolT.text = txt;
    }
}
