using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatNpc : MonoBehaviour
{
    [SerializeField] private Material outline;

    PopUpTxt text;
    SkinnedMeshRenderer renderers;
    List<Material> materials = new List<Material>();

    void Start()
    {
        renderers = GetComponent<SkinnedMeshRenderer>();
        text = GameObject.Find("PopUptxt").GetComponent<PopUpTxt>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            materials.Clear();
            materials.AddRange(renderers.sharedMaterials);
            materials.Add(outline);

            renderers.materials = materials.ToArray();
            text.TextUp(0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            materials.Clear();
            materials.AddRange(renderers.sharedMaterials);
            materials.Remove(outline);

            renderers.materials = materials.ToArray();
            text.TextDown();
        }
    }
}
