using System;
using System.Collections.Generic;
using System.Text;

using RavenClaw.MudEngine.Interfaces;
namespace RavenClaw.MudEngine.Commands
{
    class InvalidCommand : ICommand
    {
        #region ICommand Members

        void ICommand.ExecuteCommand(MudSession mudSession)
        {
            mudSession.WriteText("I'm sorry master, I didn't understand your instruction." + Environment.NewLine);
        }

        #endregion
    }
}
