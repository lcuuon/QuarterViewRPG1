using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcanvas : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);   
    }

    public void Setup()
    {
        gameObject.transform.SetAsLastSibling();
    }

    void Update()
    {
        
    }
}
