using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.PackageManager;
using System;

public class SocketConnection : MonoBehaviour
{
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Server");

        // Start TcpServer background thread
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequest));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Runs in background TcpServerThread; Handles incomming TcpClient requests
    private void ListenForIncommingRequest()
    {
        try
        {
            // Create listener on 192.168.0.2 port 50001
            tcpListener = new TcpListener(IPAddress.Parse("192.168.0.11"), 50001);
            tcpListener.Start();
            Debug.Log("Server is listening");

            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        // Read incomming stream into byte array.
                        do
                        {
                            byte[] buffer = new byte[1024];
                            int bytes = stream.Read(buffer, 0, buffer.Length);
                            if (bytes <= 0)
                            {
                                continue;
                            }

                            string message = Encoding.UTF8.GetString(buffer, 0, bytes);
                            Debug.Log(message);
                        } while (true);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }
}
