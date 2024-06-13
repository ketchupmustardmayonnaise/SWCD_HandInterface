using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aiming : Gesture
{
    // Start is called before the first frame update
    [SerializeField] GameObject AimingPoint;
    [SerializeField] Bullet bullet;
    [SerializeField] GameObject leftHand;

    int x;
    int y;

    void Start()
    {
        x = 120;
        y = 120;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            AimingPoint.gameObject.transform.localPosition = new Vector3((float)((120 - x) * 0.0375),
                0, (float)((120 - y) * 0.0375));
        }
    }

    public void SetPoint(int _x, int _y)
    {
        if (gameObject.activeSelf)
        {
            if (_x != 0 && _y != 0)
            {
                x = _x;
                y = _y;
            }
            else
            {
                x = 120;
                y = 120;
            }
        }
    }

    public void Fire()
    {
        Instantiate(bullet, AimingPoint.transform.position, Quaternion.Euler(leftHand.transform.right));
    }
}
