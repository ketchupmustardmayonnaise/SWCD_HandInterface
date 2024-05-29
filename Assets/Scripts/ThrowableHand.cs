using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableHand : MonoBehaviour
{
    [SerializeField]
    OVRHand hand;

    private Vector3 newPosRight;
    private Vector3 prevPosRight;
    private Vector3 rightHandVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetVelocity()
    {
        return rightHandVelocity;
    }
}
