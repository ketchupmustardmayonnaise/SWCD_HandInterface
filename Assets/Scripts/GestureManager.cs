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
    Radios radios;

    // [SerializeField]
    // Idle idle;
    // Hammer hammer;
    // Radios radios;
    // Bumb bumb;

    string currentGesture;

    List<Gesture> gestures;

    bool isGesture;
    bool setTimer;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        isGesture = false;
        gestures = new List<Gesture>();
        currentGesture = "idle";
        time = 0;

        gestures.Add(bow);
        gestures.Add(gun);
        gestures.Add(joystick);
    }

    public void SetCurrentGesture(string str)
    {
        Debug.Log(str);
        if (currentGesture == "idle")
        {
            currentGesture = str;
            if (currentGesture == "bow") bow.SetVisible(true);
            else if (currentGesture == "gun") gun.SetVisible(true);
            else if (currentGesture == "joystick")
            {
                joystick.SetVisible(true);
                joystick.SetInitRotate();
            }
            else if (currentGesture == "radios") radios.SetVisible(true);
            else if (currentGesture == "bomb") bomb.SetVisible(true);
        }

        if (str == "idle")
        {
            if (currentGesture == "bow") bow.SetVisible(false);
            else if (currentGesture == "gun") gun.SetVisible(false);
            else if (currentGesture == "joystick")
            {
                joystick.SetVisible(false);
                joystick.isActivate = false;
            }
            else if (currentGesture == "radios") radios.SetVisible(false);
            else if (currentGesture == "bomb") bomb.SetVisible(false);
            currentGesture = "idle";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
