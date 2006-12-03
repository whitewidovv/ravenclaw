using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Interfaces
{
    class IKey
    {
        bool CanOpen(ILockable item);
    }
}
