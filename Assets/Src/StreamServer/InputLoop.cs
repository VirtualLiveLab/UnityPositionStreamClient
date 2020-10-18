﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace StreamServer
{
    /**
     * UDP packet receiving class.
     * Do not make multiple instance of this class,
     * because that will break synchronization between
     * Socket.Available() and Socket.ReadAsync().
     * This class will be refactored to singleton.
     */
    public class InputLoop
    {
        public CancellationTokenSource cts = new CancellationTokenSource();
        private UdpClient udp;
        private int interval;
        private string name;
        private ModelManager modelManager;

        public InputLoop(UdpClient udpClient, ModelManager modelManager, int interval, string name = "Receiver")
        {
            udp = udpClient;
            this.modelManager = modelManager;
            this.interval = interval;
            this.name = name;
        }
        
        public void Start()
        {
            IPEndPoint localEndPoint = udp.Client.LocalEndPoint as IPEndPoint;
            PrintDbg($"Any -> localhost: [{localEndPoint?.Port}]\n");
            Task.Run(() => Loop(cts.Token), cts.Token);
        }

        private async Task Loop(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    var delay = Task.Delay(interval, token);
                    try
                    {
                        while (udp.Available > 0)
                        {
                            var res = await udp.ReceiveAsync();
                            var buf = res.Buffer;
                            var packets = Utility.BufferToPackets(buf);
                            if (packets != null)
                            {
                                foreach (var packet in packets)
                                {
                                    if (modelManager.Users.TryGetValue(packet.PaketId, out var user))
                                    {
                                        user.Data.CurrentMinimumAvatarPacket = packet;
                                    }
                                }
                            }
                        }
                    }
                    catch (SocketException e)
                    {
                        PrintDbg(e);
                        PrintDbg("Is the server running？");
                    }

                    token.ThrowIfCancellationRequested();
                    await delay;
                }
            }
            catch (OperationCanceledException)
            {
                PrintDbg("Input loop stopped");
                udp = null;
                throw;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
        
        public void Stop()
        {
            cts.Cancel();
        }
        
        private void PrintDbg<T>(T str)
        {
            Debug.Log($"[{name}] {str}");
        }
    }
}
