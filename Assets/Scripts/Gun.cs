using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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