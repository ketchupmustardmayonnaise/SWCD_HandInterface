using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Gesture
{
    [SerializeField] GameObject reloadPosition;
    [SerializeField] BowArrow arrow;

    bool isArrowRemained = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reload()
    {
        if (isArrowRemained)
        {
            Instantiate(arrow, reloadPosition.transform.position, reloadPosition.transform.rotation);
            isArrowRemained = true;
        }
    }

    public void Shoot()
    {
        if (gameObject.activeSelf)
        {
            arrow.isReady = true;
            isArrowRemained = false;
        }
    }
}
