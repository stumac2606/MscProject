using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TCP_Basic : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;



    private void Start()
    {



        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(localAddr, 8888);

        try
        {
            server.Start();
        }
        catch { Debug.Log("Start server error!"); }

        try
        {
            AcceptClient();
        }
        catch { Debug.Log("Accept client error!"); }

        receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
    }

    private void OnDestroy()
    {
        server?.Stop();
        client?.Close();

        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
    }

    private void AcceptClient()
    {
        server.BeginAcceptTcpClient(HandleClientConnection, null);
    }

    private void HandleClientConnection(IAsyncResult ar)
    {
        client = server.EndAcceptTcpClient(ar);
        stream = client.GetStream();

        AcceptClient();
    }

    private void ReceiveMessages()
    {
        byte[] bytes = new byte[32];
        int count;

        while (true)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    while (stream.DataAvailable)
                    {
                        count = stream.Read(bytes, 0, bytes.Length);
                        string data = Encoding.UTF8.GetString(bytes, 0, count);
                        Debug.Log("Received: " + data);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Receive error: " + e.Message);
                }
            }

            Thread.Sleep(10); // Add a small delay to prevent busy-waiting
        }
    }

    public void SendString(string message)
    {
        if (client != null && client.Connected)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            stream.Write(sendBytes, 0, sendBytes.Length);
            Debug.Log("Sent: " + message);
        }
        else
        {
            Debug.LogWarning("No client connection available to send the message.");
        }
    }
}
