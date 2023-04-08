using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTxt : MonoBehaviour
{
    [SerializeField] public Sprite[] img;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public void TextUp(int imgnum)
    {
        image.sprite = img[imgnum];
        image.enabled = true;

    }
    public void TextDown()
    {
        image.enabled = false;
    }
}
