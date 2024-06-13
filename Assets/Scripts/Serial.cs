using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Serial : MonoBehaviour
{
    [SerializeField] GestureManager gesManager;
    [SerializeField] Aiming aiming;
    [SerializeField] string port = "COM4";
    [SerializeField] int baud = 300;
    [SerializeField] int dataNum = 4;

    [SerializeField] bool isDebugging = false;

    SerialPort sp;

    bool isTouching = false;
    bool isTouchingPrev = false;

    int gesture;
    int eventFlag;
    int previousEventFlag;


    int init_x; int init_y;
    int x;
    int y;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Serial_Go(port, baud);

        isTouchingPrev = false;
        previousEventFlag = 0;
        eventFlag = 0;
        gesture = 0;
    }

    public void Serial_Go(string st0, int num0)
    {
        sp = new SerialPort(st0, num0);
        sp.DtrEnable = true;
        sp.RtsEnable = true;
        sp.Open();
        sp.ReadTimeout = 50;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingPrev = isTouching;
        try
        {
            int bytes = sp.BytesToRead;

            if (bytes >= 1)
            {
                isTouchingPrev = isTouching;
                isTouching = true;
                count = 0;

                byte[] buffer = new byte[bytes];
                sp.Read(buffer, 0, bytes);

                string str;
                str = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                //Debug.Log("str = " + str);

                string[] tempStr;
                tempStr = str.Split("\r\n");

                if (tempStr.Length > 1)
                {
                    int index = 0;
                    while (index < tempStr.Length)
                    {
                        int[] datas = new int[dataNum];
                        string nonSplitStr = tempStr[index].ToString();
                        string[] splitStr = nonSplitStr.Split(',');
                        for (int i = 0; i < dataNum; i++)
                        {
                            datas[i] = Int32.Parse(splitStr[i]);

                            if (i == 0)
                            {
                                previousEventFlag = eventFlag;
                                eventFlag = datas[i];
                            }
                            else if (i == 1)
                            {
                                if (datas[i] != 0 && gesture == 0)
                                {
                                    gesture = datas[i];
                                    gesManager.SerialGesture(gesture);
                                }
                            }
                            else if (i == 2) x = datas[i];
                            else if (i == 3) y = datas[i];
                        }

                        //Debug.Log(datas[0] + "," + datas[1] + "," + datas[2] + "," + datas[3]);

                        aiming.SetPoint(datas[2], datas[3]);
                        datas.Free();
                    }
                }
            }
            else
            {
                count++;
                if (count >= 10) isTouching = false;
            }
        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
        }

        //isTouchingPrev = isTouching;

        // 터치 떼는 순간 감지
        if ((eventFlag == 1 && eventFlag != previousEventFlag))
        { 
            isTouching = false;
        }

        if (!isTouching)
        {
            eventFlag = 0;
            previousEventFlag = 0;
            gesture = 0;
            x = 0; y = 0;
        }

        // 터치 누르기 시작하는 순간
        if (isTouchingPrev == false && isTouching == true)
        {
            gesManager.SerialXYInit(x, y);
        }

        // 터치 떼는 순간
        if (isTouchingPrev == true && isTouching == false)
        {
            gesManager.SerialTouchRelease();
        }

        if(isDebugging)  Debug.Log((isTouching ? "True, " : "False, ") + eventFlag + ", " + gesture + ", " + x + ", " + y);

        // X, Y 값을 GestureManager에게 넘겨 줌
        gesManager.SerialXY(x, y);
    }
}
