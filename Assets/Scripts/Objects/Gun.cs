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
            // 총알이 남아 있는가?
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
        text.text = "Remained bullet: " + bulletNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
