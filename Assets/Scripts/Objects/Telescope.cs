using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : Gesture
{
    List<int> scale = new List<int>() { 1, 3, 6 };
    int index;

    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        index = 2;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Zoom(bool Zoomin)
    {
        if (Zoomin && index > 0) index--;
        else if (!Zoomin && index < 2) index++;
        cam.fieldOfView = scale[index];
    }
}
