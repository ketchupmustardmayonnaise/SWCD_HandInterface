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

    bool isReloading = false;
    bool isReady = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        bulletNum = bulletMax;
        setText();
    }

    public void Fire()
    {
        if (gameObject.activeSelf == true && !isReloading)
        {
            // 총알이 남아 있는가?
            if (bulletNum > 0 && isReady)
            {
                Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
                bulletNum--;
                setText();
                isReady = false;
            }
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        text.text = "Reloading All...";
        yield return new WaitForSeconds(1.0f);
        bulletNum = bulletMax;
        setText();
        isReloading = false;
        yield return null;
    }

    public IEnumerator ReloadOne()
    {
        isReloading = true;
        text.text = "Reloading One...";
        yield return new WaitForSeconds(1.0f);
        setText();
        isReloading = false;
        isReady = true;
        yield return null;
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
