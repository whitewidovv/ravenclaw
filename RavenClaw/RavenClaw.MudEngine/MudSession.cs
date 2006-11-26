using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;


using RavenClaw.MudEngine.Exceptions;
using RavenClaw.MudEngine.Interfaces;

namespace RavenClaw.MudEngine
{
    class MudSession
    {
        private TcpClient _tcpConnection;
        private Thread _sessionThread;
        private NetworkStream _ns;

        private SessionStatus _sessionStatus = SessionStatus.PreLogin;
        
        public SessionStatus SessionStatus
        {
            internal set
            {
                _sessionStatus = value;
            }

            get
            {
                return _sessionStatus;
            }
        }

        private string _username = string.Empty;
        public string Username
        {
            internal set
            {
                _username = value;
            }

            get
            {
                return _username;
            }
        }
        public MudSession(TcpClient client)
        {
            if (client == null)
                throw new ArgumentNullException("client");

            _tcpConnection = client;
            _ns = client.GetStream();

            _sessionThread = new Thread(new ThreadStart(HandleSession));
            _sessionThread.Start();
        }

        private void HandleSession()
        {
            try
            {
                WritePreamble();

                while (_tcpConnection.Connected)
                {
                    string commandString = GetCommandText();

                    ICommand command = CommandFactory.GetInstance(commandString, this.SessionStatus);

                    command.ExecuteCommand(this);
                }
            }
            catch (Exception)
            {
                // Do nothing
            }
        }

        private void WritePreamble()
        {
            SendData("Welcome to the Raven Claw Mud Engine" + Environment.NewLine);
            SendData("------------------------------------" + Environment.NewLine);

            SendData("Username:");
        }
        private string GetCommandText()
        {
            StreamReader reader = new StreamReader(_ns);

            return reader.ReadLine();
        }

        public void SendData(string messageToSend)
        {
            if (!_tcpConnection.Connected)
            {
                throw new MudSessionClosedException();
            }

            if (String.IsNullOrEmpty(messageToSend))
            {
                throw new ArgumentNullException("messageToSend");
            }

            byte[] byteData = System.Text.ASCIIEncoding.ASCII.GetBytes(messageToSend);

            NetworkStream connectionStream = _tcpConnection.GetStream();
          
            connectionStream.Write(byteData, 0, byteData.Length);
           
        }
    }
}
