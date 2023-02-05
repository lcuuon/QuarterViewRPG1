using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPbar : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHP;

    [SerializeField] Vector3 offset = Vector3.zero;
    public Transform enemyPos;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHP = this.gameObject.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(enemyPos.position);

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        rectHP.localPosition = localPos + new Vector2(offset.x, offset.y);
    }

    void Update()
    {
        
    }
}
