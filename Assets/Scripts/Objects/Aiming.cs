using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Aiming : Gesture
{
    // Start is called before the first frame update
    [SerializeField] GameObject AimingPoint;

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
            AimingPoint.gameObject.transform.localPosition = new Vector3((float)((x - 120) * 0.0375),
                0, (float)((y-120) * 0.0375));
        }
    }

    public void SetPoint(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
