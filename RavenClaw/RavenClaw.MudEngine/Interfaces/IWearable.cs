using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Interfaces
{
    interface IWearable
    {
        bool IsWearable(Wearlocation location, Account a);

        void WearItem(WearLocation location, Account a);
    }
}
