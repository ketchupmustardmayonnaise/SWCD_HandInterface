using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Gun : Gesture
{
    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    Transform FirePos;

    [SerializeField]
    const int bulletMax = 5;

    [SerializeField]
    TMP_Text text;

    int bulletNum;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        bulletNum = bulletMax;
        setText();
    }

    public void Fire()
    {
        if (gameObject.activeSelf == true)
        {
            // ÃÑ¾ËÀÌ ³²¾Æ ÀÖ´Â°¡?
            if (bulletNum > 0)
            {
                Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
                bulletNum--;
                setText();
            }
        }
    }

    public void Reload()
    {
        bulletNum = bulletMax;
        setText();
    }

    void setText()
    {
        text.text = "ÀÜ¿© ÃÑ¾Ë °³¼ö: " + bulletNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
