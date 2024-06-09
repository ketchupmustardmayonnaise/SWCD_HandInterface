using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    [SerializeField]
    Bow bow;

    [SerializeField]
    Gun gun;

    [SerializeField]
    Joystick joystick;

    [SerializeField]
    Bomb bomb;

    [SerializeField]
    Hammer hammer;

    [SerializeField]
    Gesture idle;

    [SerializeField]
    Telescope telescope;

    [SerializeField]
    Poker poker;

    [SerializeField]
    Aiming aiming;

    public bool isTouching = false;

    // [SerializeField]
    // Idle idle;
    // Hammer hammer;
    // Radios radios;
    // Bumb bumb;

    Gesture currentGesture;

    List<Gesture> gestures;

    float serialGestureTime;


    // Start is called before the first frame update
    void Start()
    {
        currentGesture = idle;
        serialGestureTime = 0;
    }

    public void SetCurrentGesture(Gesture ges)
    {
        
        // 이전 제스처가 idle && 지금 다른 제스처를 취한다면 -> 해당 제스처 활성화
        if (currentGesture == idle)
        {
            currentGesture = ges;
            EnableGesture(ges, true);
        }

        // 두 손 제스처
        // 

        // 취한 제스처가 idle일 때 -> 모두 비활성화
        if (ges == idle)
        {
            EnableGesture(currentGesture, false);
            currentGesture = idle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentGesture);
        serialGestureTime += Time.deltaTime;
    }

    public void EnableGesture(Gesture ges, bool isEnabled)
    {
        ges.SetVisible(isEnabled);
    }

    public void SerialTouchRelease()
    {
        if (currentGesture == bow) bow.Fire();
        if (currentGesture == aiming) aiming.Fire();
    }

    public void SerialXY(int x, int y)
    {
        if (currentGesture == aiming) aiming.SetPoint(x, y);
    }

    public void SerialGesture(int ges)
    {
        if (ges == 170) // PALM
        {
            if (currentGesture == gun)
            {
                StartCoroutine(gun.Reload());
            }

            if (currentGesture == bomb)
            {
                bomb.isJumpReady = true;
            }
        }
        else if (ges == 1) // UP
        {
            if (currentGesture == bomb)
            {
                bomb.setBombTime(true);
                serialGestureTime = 0;
            }
        }
        else if (ges == 2) // DOWN
        {
            if (currentGesture == bomb)
            {
                bomb.setBombTime(false);
                serialGestureTime = 0;
            }
        }
        else if (ges == 3) // LEFT
        {
            if (currentGesture == poker)
            {
                poker.Select(true);
            }
            if (currentGesture = bow)
            {
                bow.Reload();
            }
        }
        else if (ges == 4) // RIGHT
        {
            if (currentGesture == poker)
            {
                poker.Select(false);
            }
        }
    }
}
