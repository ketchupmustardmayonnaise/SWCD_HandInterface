using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using static Oculus.Interaction.OptionalAttribute;

public class Joystick : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }


    public void SetVisible(bool flag)
    {
        gameObject.SetActive(flag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
