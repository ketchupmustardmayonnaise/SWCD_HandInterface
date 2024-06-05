using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using static Oculus.Interaction.OptionalAttribute;

public class Joystick : Gesture
{
    [SerializeField]
    float threshold;

    [SerializeField]
    Player player;

    Vector3 initRotate;
    public bool isActivate;

    // Start is called before the first frame update
    void Start()
    {
        threshold = 1f;
        gameObject.SetActive(false);
        isActivate = false;
    }

    public void SetInitRotate()
    {
        initRotate = gameObject.transform.rotation.ToEulerAngles();
        isActivate = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isActivate)
        {
            Vector3 rotate = gameObject.transform.rotation.ToEulerAngles();
            Vector3 diffRotate = rotate - initRotate;

            float x = 0f;
            float z = 0f;
            if (diffRotate.x < -threshold)
            {
                x = -1;
            }
            else if (diffRotate.x > threshold)
            {
                x = 1;
            }

            if (diffRotate.z < -threshold)
            {
                z = -1;
            }
            else if (diffRotate.z > threshold)
            {
                z = 1;
            }
            player.CalculateMovement(x, z);
        }
        
    }
}
