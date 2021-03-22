using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using UnityEngine;

public class TCPHandler : ByteToMessageDecoder, IChannelHandler
{
    public override void ChannelRegistered(IChannelHandlerContext context) { Debug.Log("ChannelRegistered"); }
    public override void ChannelUnregistered(IChannelHandlerContext context) { Debug.Log("ChannelUnregistered"); }  


    void IChannelHandler.ChannelActive(IChannelHandlerContext context)
    {        
        Debug.Log("NetworkManager:TCP ChannelActive! LocalAddress=" + context.Channel.LocalAddress);
    }

    void IChannelHandler.ChannelInactive(IChannelHandlerContext context)
    {
        Debug.Log("NetworkManager:TCP ChannelInactive!");
    }


    protected internal override void Decode(IChannelHandlerContext ctx, IByteBuffer msg, List<object> list)
    {
	    if (msg.ReadableBytes < 12)
	    {
		    return; 
	    }

	    msg.MarkReaderIndex();
	    
	    int packet_size = msg.ReadIntLE();

	    if (!isAvailablePacketSize(packet_size))
	    {
		    Debug.Log("[TCP_InBoundHandler] Packet손상!! packet_size = " + packet_size + "");

		    ctx.Channel.CloseAsync();
		    return;
	    }

	    if (msg.ReadableBytes < packet_size + 8)
	    {
		    msg.ResetReaderIndex();
		    return;
	    }
	    else
	    {
		    if (!isCompleteSizePacket(packet_size, ctx, msg))
		    {
			    return;
		    }
	    }
    }
	
    
    bool isCompleteSizePacket(int packet_size, IChannelHandlerContext ctx, IByteBuffer msg)
    {
        if (msg.ReadableBytes < packet_size + 8)
        {
            msg.ResetReaderIndex();
            return false;
        }
        else if (msg.ReadableBytes > packet_size + 8)
        {
            byte[] packet_data = new byte[msg.ReadableBytes];
            msg.ReadBytes(packet_data, 0, msg.ReadableBytes);

            if (msg.Capacity - msg.WriterIndex < 10000)
                msg.DiscardReadBytes();

            
            
            
            lock (NetworkManager.Locking)
            {
	            NetworkManager.PacketQueue.Enqueue( new PacketData(packet_data) );
            }



            if (msg.ReadableBytes < 12)
            {
                return false;
            }
            else
            {
                msg.MarkReaderIndex();
                int amountSize = msg.ReadIntLE();

                if (!isAvailablePacketSize(amountSize))
                {
                    ctx.CloseAsync();
                    return false;
                }

                return isCompleteSizePacket(amountSize, ctx, msg);
            }
        }
        else
        {
	        byte[] packet_data = new byte[msg.ReadableBytes];
	        msg.ReadBytes(packet_data, 0, msg.ReadableBytes);

            msg.Clear();


            lock (NetworkManager.Locking)
            {
	            NetworkManager.PacketQueue.Enqueue( new PacketData(packet_data) );
            }

            return false;
        }
    }
    
    bool isAvailablePacketSize(int packet_size)
    {
	    if (0 >= packet_size || packet_size > 65535)
		    return false;

	    else
		    return true;
    }
    
    
    public override void ExceptionCaught(IChannelHandlerContext context, Exception e)
    {
        Debug.Log(e.Message+e.StackTrace);
        context.Channel.CloseAsync();
    }

}
