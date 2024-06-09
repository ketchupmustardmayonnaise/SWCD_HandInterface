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

    int gesture;
    int eventFlag;
    int previousEventFlag;

    int x;
    int y;

    // Start is called before the first frame update
    void Start()
    {
        Serial_Go(port, baud);

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

                            /*// ����ó �ν�
                            if (i == 0)
                            {
                                // ���� ����ó�� ���� ����ó�� �ٸ��� Ȯ��
                                if (gesture != datas[i]) gesManager.SerialGesture(datas[i]);

                                // �ƹ��� ����ó�� �ν����� ���� ���¶��
                                if (gesture == 0 || gesture == -1)
                                {
                                    gesture = datas[i];
                                }
                            }
                            else if (i == 1) // touchNum �ν�
                            {
                                touchNum = datas[i];
                            }*/
                        }

                        //Debug.Log(datas[2] + "," + datas[3]);

                        aiming.SetPoint(datas[2], datas[3]);
                        datas.Free();
                    }
                }
            }
        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
        }

        // ��ġ ��
        if (eventFlag == 1 && eventFlag != previousEventFlag)
        {
            gesManager.SerialTouchRelease();
            isTouching = false;
            eventFlag = 0;
            previousEventFlag = 0;
            gesture = 0;
            x = 0; y = 0;
        }

        //Debug.Log((isTouching ? "True, " : "False, ") + eventFlag + ", " + gesture + ", " + x + ", " + y);

        // X, Y ���� GestureManager���� �Ѱ� ��
        gesManager.SerialXY(x, y);
    }
}
