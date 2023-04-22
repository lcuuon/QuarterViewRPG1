using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DamageTxt : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectText;
    private Vector3 _enemyPos;
    private float offset;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        Debug.Log(canvas.name);
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectText = this.gameObject.GetComponent<RectTransform>();
    }

    public void parentSet(Transform parent)
    {
        Debug.Log(parent.name);
        gameObject.transform.SetParent(parent);
    }

    public void Damaged(Vector3 enemyPos)
    {
        _enemyPos = enemyPos;
        offset = 0;
        Invoke("DestroyObject", 0.5f);
    }

    private void LateUpdate()
    {
        offset += Time.deltaTime * 100;

        var screenPos = Camera.main.WorldToScreenPoint(_enemyPos);

        var localPos = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        rectText.localPosition = localPos + new Vector2(0, 100 + offset);
    }

    private void DestroyObject()
    {
        GetComponent<TMP_Text>().color = Color.white;
        DamagedTextPool.ReturnDamaged(this);
    }
}
