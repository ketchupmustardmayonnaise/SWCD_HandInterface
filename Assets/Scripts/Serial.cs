using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Serial : MonoBehaviour
{
    [SerializeField] GestureManager gesManager;
    [SerializeField] string port = "COM4";
    [SerializeField] int baud = 300;
    [SerializeField] int dataNum = 4;

    SerialPort sp;
    bool hexTF = false; //hex ������ �������� true, ascii ������ �������� false;
    
    int gesture;
    int touchNum;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        Serial_Go(port, baud);

        time = 0;
        touchNum = 0;
        gesture = -1;
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
                byte[] buffer = new byte[bytes];
                sp.Read(buffer, 0, bytes);

                string str;
                str = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                //Debug.Log("str = " + str);

                string[] tempStr;
                tempStr = str.Split("\r\n");

                if (tempStr.Length > 1)
                {
                    time = 0;

                    int index = 0;
                    while (index < tempStr.Length)
                    {
                        int[] datas = new int[dataNum];
                        string nonSplitStr = tempStr[index].ToString();
                        string[] splitStr = nonSplitStr.Split(',');
                        for (int i = 0; i < dataNum; i++)
                        {
                            datas[i] = Int32.Parse(splitStr[i]);

                            // ����ó �ν�
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
                            }
                        }

                        //Debug.Log(datas[0] + "," + datas[1] + "," + datas[2] + "," + datas[3]);

                        datas.Free();
                    }
                }
            }
        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
        }

        time += Time.deltaTime;
        Debug.Log(gesture + ", " + touchNum);
        if (time > 1.0f)
        {
            gesture = -1;
            touchNum = 0;
        }
    }
}
