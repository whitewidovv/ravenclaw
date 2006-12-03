using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Interfaces
{
    interface ILockable
    {
        int LockId
        {
            get;
        }
        bool IsLocked
        {
            get;
        }

        bool Lock(IKey key);
    }
}
