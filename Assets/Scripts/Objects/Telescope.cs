using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : Gesture
{

    [SerializeField] OVRCameraRig scopeCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        scopeCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
