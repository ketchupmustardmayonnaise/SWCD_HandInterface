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

    SerialPort sp;

    bool isTouching = false;
    bool isTouchingPrev = false;

    int gesture;
    int eventFlag;
    int previousEventFlag;

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
        try
        {
            int bytes = sp.BytesToRead;

            if (bytes >= 1)
            {
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
                                    Debug.Log("시리얼 제스처");
                                    gesture = datas[i];
                                    gesManager.SerialGesture(gesture);
                                }
                            }
                            else if (i == 2) x = datas[i];
                            else if (i == 3) y = datas[i];

                            /*// 제스처 인식
                            if (i == 0)
                            {
                                // 직전 제스처랑 지금 제스처랑 다른지 확인
                                if (gesture != datas[i]) gesManager.SerialGesture(datas[i]);

                                // 아무런 제스처도 인식하지 못한 상태라면
                                if (gesture == 0 || gesture == -1)
                                {
                                    gesture = datas[i];
                                }
                            }
                            else if (i == 1) // touchNum 인식
                            {
                                touchNum = datas[i];
                            }*/
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

        isTouchingPrev = isTouching;

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

        // 터치 떼는 순간
        if (isTouchingPrev == true && isTouching == false)
        {
            gesManager.SerialTouchRelease();
        }

        //Debug.Log((isTouching ? "True, " : "False, ") + eventFlag + ", " + gesture + ", " + x + ", " + y);

        // X, Y 값을 GestureManager에게 넘겨 줌
        gesManager.SerialXY(x, y);
    }
}
