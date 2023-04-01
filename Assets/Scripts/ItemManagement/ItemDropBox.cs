using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropBox : MonoBehaviour
{
    private Material outline;
    private DropTable dt;

    MeshRenderer renderer;
    Material[] materials;


    void Start()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
        renderer = GetComponent<MeshRenderer>();
        dt = GameObject.Find("DropTable").GetComponent<DropTable>();
        materials = renderer.sharedMaterials;
    }

    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("enter");

        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i] == null)
            {
                materials[i] = outline;
                Debug.Log(materials[i].name);
                break;
            }
        }

        renderer.materials = materials;
    }

    private void OnMouseExit() 
    {
        for (int i = materials.Length - 1; i > 0; i--)
        {
            if (materials[i] != null)
            {
                materials[i] = null;
                break;
            }
        }

        renderer.materials = materials;
    }

    private void OnMouseDown()
    {
        dt.OpenTab();

    }


    
}
