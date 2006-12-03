using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Interfaces
{
    public interface IRace
    {
        int RaceId
        {
            get;
        }

        /// <summary>
        /// When the player gets "killed" can they respawn? Most of the time this will be true.
        /// </summary>
        bool Immortal
        {
            get;
        }

        string Name
        {
            get;
        }

        public decimal ExperienceMultiplier
        {
            get;
        }
    }
}
