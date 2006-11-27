using System;
using System.Collections.Generic;
using System.Text;

using RavenClaw.MudEngine.Interfaces;

namespace RavenClaw.MudEngine
{
    class CommandFactory
    {
        private CommandFactory()
        {

        }

        public static ICommand GetInstance(string command, SessionStatus session)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (session == SessionStatus.PreLogin)
            {
                return new Commands.HandleUsername(command);
            }

            if (session == SessionStatus.GivenUsername)
            {
                return new Commands.HandlePassword(command);
            }

            if (session == SessionStatus.LoggedIn)
            {
                // Other rules.

                return new Commands.InvalidCommand();

            }

            return null;
        }
    }
}
