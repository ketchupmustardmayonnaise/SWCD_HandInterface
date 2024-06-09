using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Gesture
{
    [SerializeField] GameObject reloadPosition;
    [SerializeField] BowArrow arrow;

    BowArrow currentArrow; // ȭ�� ������
    bool isArrowRemained = false; // ���� ȭ���� �ִ°�

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
        if (!isArrowRemained)
        {
            currentArrow = Instantiate(arrow, reloadPosition.transform.position, reloadPosition.transform.rotation, transform);
            isArrowRemained = true;
        }
    }

    public void Fire()
    {
        if (isArrowRemained)
        {
            currentArrow.isReady = true;
            isArrowRemained = false;
            currentArrow = null;
        }
    }
}
