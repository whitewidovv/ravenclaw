using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace RavenClaw.MudEngine
{
    public class MudEngine
    {

        private bool _acceptNewConnections = true;
        public MudEngine()
        {

        }

        public void Start()
        {
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000));
            listener.Start();

            while (_acceptNewConnections)
            {
                TcpClient client = listener.AcceptTcpClient();

                MudSession session = new MudSession(client);   
            }
        }

        public void Stop()
        {
            _acceptNewConnections = false;
        }
    }
}
