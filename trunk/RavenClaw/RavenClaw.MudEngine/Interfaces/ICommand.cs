using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Interfaces
{
    interface ICommand
    {
        void ExecuteCommand(MudSession mudSession);
    }
}
