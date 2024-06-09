using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] OVRCameraRig baseCamera;
    [SerializeField] OVRCameraRig secondCamera;

    [SerializeField] List<GameObject> cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        baseCamera.enabled = true;
        secondCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSecondCameraPosition(bool isUp)
    {
        if (secondCamera.isActiveAndEnabled)
        {
            if (isUp)
            {

            }
        }
    }
}
