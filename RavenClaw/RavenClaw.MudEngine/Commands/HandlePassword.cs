using System;
using System.Collections.Generic;
using System.Text;

using RavenClaw.MudEngine.Interfaces;

namespace RavenClaw.MudEngine.Commands
{
    class HandlePassword : ICommand
    {
        private string _password = string.Empty;
        
        public HandlePassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            _password = password;
        }



        #region ICommand Members

        void ICommand.ExecuteCommand(MudSession mudSession)
        {
            if (mudSession.Username == "ckwop" && _password == "test")
            {
                mudSession.WriteText("Password Correct." + Environment.NewLine);
                mudSession.SessionStatus = SessionStatus.LoggedIn;
            }
            else
            {
                mudSession.WriteText("Password Incorrect." + Environment.NewLine);
                mudSession.WriteText("Password:");
            }
        }

        #endregion
    }
}
