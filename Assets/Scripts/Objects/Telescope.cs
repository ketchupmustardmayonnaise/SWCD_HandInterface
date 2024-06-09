using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : Gesture
{
    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CameraMove(bool isUp)
    {
        player.isTransformed = true;
        // 다시 여기 코딩
    }
}
