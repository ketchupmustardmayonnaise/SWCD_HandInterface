using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Serial2 : MonoBehaviour
{

    SerialPort sp;

    bool hexTF = false; //hex ������ �������� true, ascii ������ �������� false;

    // Start is called before the first frame update
    void Start()
    {
        Serial_Go("COM4", 300);
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

                if (hexTF)
                {
                    List<int> messIntList = new List<int>();

                    foreach (byte item in buffer)
                    {
                        messIntList.Add(item);
                        //Debug.Log("item=" + item);
                    }

                    //0XF2 0X41 0X01 0XF3 ���·� ������ ����
                    if (messIntList[0] == 242 && messIntList[1] == 49 && messIntList[2] == 1)
                    {
                        //���ϴ� �����Ͱ� ������ ����
                    }
                }
                else
                {
                    string str;
                    str = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.Log("str = " + str);

                    if (str == "cp1")
                    {
                        //���ϴ� �����Ͱ� ������ ����
                    }
                }
            }
        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
        }
    }
}
