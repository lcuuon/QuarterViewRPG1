using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(_camera.fieldOfView > 15)
            {
                _camera.fieldOfView--;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_camera.fieldOfView < 60)
            {
                _camera.fieldOfView++;
            }
        }



    }
}
