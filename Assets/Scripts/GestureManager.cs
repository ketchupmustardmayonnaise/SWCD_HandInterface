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
    Gesture idle;

    [SerializeField]
    Telescope telescope;

    [SerializeField]
    Poker poker;

    [SerializeField]
    Aiming aiming;

    [SerializeField]
    Padlock padlock;

    public bool isTouching = false;

    // [SerializeField]
    // Idle idle;
    // Hammer hammer;
    // Radios radios;
    // Bumb bumb;

    Gesture currentGesture;

    List<Gesture> gestures;

    float serialGestureTime;

    int x; int y;
    int init_x; int init_y;


    // Start is called before the first frame update
    void Start()
    {
        currentGesture = idle;
        serialGestureTime = 0;
        x = 0; y = 0;
        init_x = 0; init_y = 0;
    }

    public void SetCurrentGesture(Gesture ges)
    {
        
        // 이전 제스처가 idle && 지금 다른 제스처를 취한다면 -> 해당 제스처 활성화
        if (currentGesture == idle)
        {
            currentGesture = ges;
            EnableGesture(ges, true);
            if (ges == joystick) joystick.SetInitRotate();
        }


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
        if (isTouching)
        {
            if (currentGesture == bow) bow.Fire();
            if (currentGesture == aiming) aiming.Fire();
            if (currentGesture == bomb) bomb.Throw();
            init_x = 0; init_y = 0;
            isTouching = false;
        }
    }

    public void SerialXY(int _x, int _y)
    {
        x = _x; y = _y;
        if (currentGesture == aiming) aiming.SetPoint(_x, _y);
        //Debug.Log(isTouching + "," + (init_x - x));
        if (currentGesture == gun)
        {
            if ((init_x - x) > 100 && isTouching)
            {
                StartCoroutine(gun.ReloadOne());
                isTouching = false;
            }
        }
    }

    public void SerialXYInit(int _x, int _y)
    {
        isTouching = true;
        init_x = _x; init_y = _y;
    }

    public void SerialGesture(int ges)
    {
        if (ges == 170) // PALM
        {
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
            if (currentGesture == poker)
            {
                StartCoroutine(poker.Throw());
            }
            if (currentGesture == padlock)
            {
                padlock.Lock();
            }
            if (currentGesture == telescope)
            {
                telescope.Zoom(false);
            }
        }
        else if (ges == 2) // DOWN
        {
            if (currentGesture == bomb)
            {
                bomb.setBombTime(false);
                serialGestureTime = 0;
            }
            if (currentGesture == padlock)
            {
                padlock.Unlock();
            }
            if (currentGesture == telescope)
            {
                telescope.Zoom(true);
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
        else if (ges == 12)
        {
            if (currentGesture == gun)
            {
                StartCoroutine(gun.Reload());
            }
        }
    }
}
