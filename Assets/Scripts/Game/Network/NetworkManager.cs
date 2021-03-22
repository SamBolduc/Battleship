using UnityEngine;

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

using DotNetty.Buffers;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Text;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    Thread _networkClientThread;

    public static Bootstrap TcpBootstrap;

    static IChannel _tcpChannel;

    static readonly PooledByteBufferAllocator _alloc = PooledByteBufferAllocator.Default;

    static IPEndPoint _serverAddress_TCP;

    public static object Locking = new object();

    public static Queue<PacketData> PacketQueue;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        PacketQueue = new Queue<PacketData>();
    }

    void Start()
    {
        _networkClientThread = new Thread(Unity_DotNettyClient);
        _networkClientThread.Start();
    }

    void OnEnable()
    {
        StartCoroutine(PacketReceiveEvent());
    }

    void OnDisable()
    {
        StopCoroutine(PacketReceiveEvent());
    }

    IEnumerator PacketReceiveEvent()
    {
        Debug.Log("Loading PacketReceiveEvent()");
        WaitForSecondsRealtime receptionRate = new WaitForSecondsRealtime(0.005f);
        PacketData packet = null;

        while (true)
        {
            lock (Locking)
            {
                if (PacketQueue.Count > 0)
                {
                    packet = PacketQueue.Dequeue();
                }
            }

            if (packet != null)
            {
                byte[] data = packet.data;

                Debug.LogWarning("[MESSAGE] " + Encoding.UTF8.GetString(data));

                data = null;
            }
            yield return receptionRate;
        }
    }


    public async static void ConnectGameServer(string TYPE, string IP_OR_DOMAIN_VALUE, int PORT)
    {
        try
        {
            if (TYPE.Equals("ip"))
            {
                _serverAddress_TCP = new IPEndPoint(IPAddress.Parse(IP_OR_DOMAIN_VALUE), PORT);
            }
            else if (TYPE.Equals("domain"))
            {
                _serverAddress_TCP = new IPEndPoint(Dns.GetHostEntry(IP_OR_DOMAIN_VALUE).AddressList[0], PORT);
            }
            else
            {
                Debug.Log("Error: ConnectGameServer()");
                return;
            }

            _tcpChannel = await TcpBootstrap.ConnectAsync(_serverAddress_TCP);
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.ToString());
        }
    }


    // TCP
    public static void SendByte(byte[] send_data)
    {
        if (_tcpChannel == null || _tcpChannel.Active != true || _tcpChannel.IsWritable != true)
        {
            return;
        }

        try
        {
            IByteBuffer data = _alloc.DirectBuffer(send_data.Length);
            
            data.WriteBytes(send_data);

            _tcpChannel.WriteAndFlushAsync(data);
        }
        catch (Exception e)
        {
            Debug.Log("SendByte() Error:" + e.ToString());
        }
    }

    void Unity_DotNettyClient()
    {
        Debug.Log("NetworkManager:Init...");

        IEventLoopGroup TCPWorkGroup;

        TCPWorkGroup = new MultithreadEventLoopGroup(1);

        TcpBootstrap = new Bootstrap();

        try
        {
            TcpBootstrap
            .Group(TCPWorkGroup) 

            .Channel<TcpSocketChannel>()  

            .Option(ChannelOption.Allocator, PooledByteBufferAllocator.Default) 

            .Option(ChannelOption.TcpNodelay, true)

            .Handler(new LoggingHandler())

            .Handler(new ActionChannelInitializer<TcpSocketChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                //pipeline.AddLast("ByteSizeFilterHandler", new ByteSizeFilterHandler());
                pipeline.AddLast("TCP_InBoundHandler", new TCPHandler());
            }));

            Debug.Log("NetworkManager:DotNetty TCP_Init...Complete");


            Debug.Log("NetworkManager:DotNetty Initialize Complete");

            Thread.CurrentThread.Join();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            TCPWorkGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));

            Debug.Log("NetworkManager:DotNetty ShutdownGracefully Complete.");
        }
    } 

}



public class PacketData
{
    public byte[] data;

    public PacketData() { }

    public PacketData(byte[] data)
    {
        this.data = data;
    }
}