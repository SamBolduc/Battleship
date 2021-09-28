using Assets.Scripts.Game.Network.Packets;
using System.Net.Sockets;
using UnityEngine;

public class NetworkManager
{
    public static NetworkManager Instance = new NetworkManager();
    private TcpClient client;
    private bool isHead;
    private int len;
    public static PacketHandler PacketHandler = new PacketHandler();

    private static bool _initialized;
    public void Init()
    {
        client = new TcpClient();
        client.Connect("127.0.0.1", 8989);
        isHead = true;
        if (_initialized) return;
        _initialized = true;
    }

    public void Disconnect()
    {
        client?.Close();
    }

    public void Send(byte[] msg)
    {
        byte[] data = new byte[4 + msg.Length];
        IntToBytes(msg.Length).CopyTo(data, 0);
        msg.CopyTo(data, 4);
        client.GetStream().Write(data, 0, data.Length);
    }
    
    public void ReceiveMsg()
    {
        NetworkStream stream = client.GetStream();
        if (!stream.CanRead)
        {
            return;
        }
        if (isHead)
        {
            if (client.Available < 4)
            {
                return;
            }
            byte[] lenByte = new byte[4];
            stream.Read(lenByte, 0, 4);
            len = BytesToInt(lenByte, 0);
            isHead = false;
        }
        if (!isHead)
        {
            if (client.Available < len)
            {
                return;
            }
            byte[] msgByte = new byte[len];
            stream.Read(msgByte, 0, len);
            isHead = true;
            len = 0;
            if (onReception != null)
            {
                onReception(msgByte);
            }
        }

    }

    public static int BytesToInt(byte[] data, int offset)
    {
        int num = 0;
        for (int i = offset; i < offset + 4; i++)
        {
            num <<= 8;
            num |= (data[i] & 0xff);
        }
        return num;
    }
    
    public static byte[] IntToBytes(int num)
    {
        byte[] bytes = new byte[4];
        for (int i = 0; i < 4; i++)
        {
            bytes[i] = (byte)(num >> (24 - i * 8));
        }
        return bytes;
    }

    public delegate void OnRevMsg(byte[] msg);

    public OnRevMsg onReception;

}