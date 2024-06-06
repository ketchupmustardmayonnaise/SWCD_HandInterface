using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    public bool isReady = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isReady) transform.Translate(Vector3.forward * 0.5f);
    }
}
