using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Interfaces
{
    public interface IPlane
    {
        string Name
        {
            get;
            set;
        }

        int Elevation
        {
            get;
            set;
        }
    }
}
