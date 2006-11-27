using System;
using System.Collections.Generic;
using System.Text;
using RavenClaw.MudEngine.Interfaces;

namespace RavenClaw.MudEngine.Commands
{
    class HandleUsername : ICommand
    {
        #region ICommand Members

        private string _username;

        public HandleUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            _username = username;
        }

        void ICommand.ExecuteCommand(MudSession mudSession)
        {
            mudSession.Username = _username;

            mudSession.SessionStatus = SessionStatus.GivenUsername;

            mudSession.SendData("Password:");
        }

        #endregion
    }
}
