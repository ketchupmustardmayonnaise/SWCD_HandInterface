using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBulletNum : MonoBehaviour
{
    int bulletNum;

    [SerializeField]
    const int bulletMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        bulletNum = bulletMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BulletFired()
    {
        bulletNum--;
    }

    public bool isFireEnabled()
    {
        if (bulletNum > 0) return true;
        else return false;
    }

    public void Reload()
    {
        bulletNum = bulletMax;
    }
}
