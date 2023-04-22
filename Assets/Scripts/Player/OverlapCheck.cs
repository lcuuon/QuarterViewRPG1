using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{
    public void overlapCheck()
    {
        GameObject overlap = GameObject.Find("Player");
        if (overlap != this.gameObject)
        {
            Destroy(overlap);
        }
    }
}
