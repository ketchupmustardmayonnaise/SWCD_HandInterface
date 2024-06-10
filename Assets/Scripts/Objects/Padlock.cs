using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padlock : Gesture
{
    [SerializeField] GameObject upperPart;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Lock()
    {
        upperPart.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void Unlock()
    {
        upperPart.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
