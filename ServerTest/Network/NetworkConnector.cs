﻿using System.Net.Sockets;

namespace Server3
{

    /// <summary>
    /// 网络连接对象
    /// 1. 连接对象，通常是客户端对象Socket，
    /// 2. socket连接上服务器
    /// 3. Conn只有一个，就是自己
    /// </summary>
    public class NetworkConnector : Network
    {
        // 被连接的ip和port
        private string _ip;
        private int _port;
        private byte[] _testError = new byte[4];

        public override bool Init()
        {
            return true;
        }

        public override void Tick()
        {
            Select();
            if (!IsConnected())
            {
                if (_exceptFds.Contains(_masterSocket))
                {
                    Log.Error($"connect except. socket: {_masterSocket}, re connect");
                    Dispose();
                    Connect(_ip, _port);
                    return;
                }

                if (_readFds.Contains(_masterSocket) || _writeFds.Contains(_masterSocket))
                {
                    try
                    {
                        _masterSocket!.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error, _testError);
                        CreateConnectObj(_masterSocket);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"connect failed. socket: {_masterSocket}, re connect");

                        // 关闭当前socket，重新connect
                        Dispose();
                        Connect(_ip, _port);
                    }
                }
            }
        }

        public bool Connect(string ip, int port)
        {
            _ip = ip;
            _port = port;

            _masterSocket = CreateSocket();
            if (_masterSocket == null)
                return false;

            try
            {
                _masterSocket.Connect(ip, port);
                CreateConnectObj(_masterSocket);
            }
            catch (Exception e)
            {
                // ignored
            }
            return true;
        }

        public bool IsConnected()
        {
            return _connects.Count > 0;
        }
    }

}