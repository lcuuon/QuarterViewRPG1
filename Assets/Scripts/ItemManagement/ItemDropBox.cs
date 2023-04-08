using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropBox : MonoBehaviour
{
    [SerializeField] private Material outline;
    private DropTable dt;

    PopUpTxt text;
    MeshRenderer renderers;
    List<Material> materials = new List<Material>();

    void Start()
    {
        renderers = GetComponent<MeshRenderer>();
        dt = GameObject.Find("DropTable").GetComponent<DropTable>();
        text = GameObject.Find("PopUptxt").GetComponent<PopUpTxt>();
    }

    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        materials.Clear();
        materials.AddRange(renderers.sharedMaterials);
        materials.Add(outline);
        text.TextUp(2);

        renderers.materials = materials.ToArray();
    }


    private void OnMouseExit() 
    {
        materials.Clear();
        materials.AddRange(renderers.sharedMaterials);
        materials.Remove(outline);
        text.TextDown();

        renderers.materials = materials.ToArray();
    }

    private void OnMouseDown()
    {
        dt.OpenTab();

    }


    
}
