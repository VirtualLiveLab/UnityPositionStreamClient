using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace StreamServer
{
    public class SyncOutputLoop
    {
        private UdpClient udp;
        private string _logName;
        private IPEndPoint _remoteEndPoint;

        public List<Transform> TransformList = new List<Transform>();

        public SyncOutputLoop(UdpClient udpClient, IPEndPoint remoteEndPoint ,string logName)
        {
            udp = udpClient;
            _remoteEndPoint = remoteEndPoint;
            _logName = logName;
        }
        
        public void Start()
        {
            IPEndPoint localEndPoint = udp.Client.LocalEndPoint as IPEndPoint;
            IPEndPoint remoteEndPoint = _remoteEndPoint;
            PrintDbg($"[{localEndPoint?.Address}]: [{localEndPoint?.Port}] -> " +
                     $"[{remoteEndPoint?.Address}]: [{remoteEndPoint?.Port}]\n");
        }

        public void Send(byte[] buff)
        {
            udp.Send(buff, buff.Length, _remoteEndPoint);
        }

        private void PrintDbg(string str)
        {
            Debug.Log($"[{_logName}] {str}");
        }
    }
}
