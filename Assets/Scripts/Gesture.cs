using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gesture : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public string name;
    public UnityEvent onRecognized;

    [SerializeField]
    GameObject gestureObject;

    void Start()
    {
        gestureObject.SetActive(false);
    }

    public void SetVisible(bool flag)
    {
        gestureObject.SetActive(flag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
