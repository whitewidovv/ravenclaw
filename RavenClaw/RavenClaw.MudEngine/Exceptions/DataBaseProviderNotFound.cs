using System;
using System.Collections.Generic;
using System.Text;

namespace RavenClaw.MudEngine.Exceptions
{
    public class DatabaseProviderNotFound : Exception
    {
        public DatabaseProviderNotFound() : base(Strings.DatabaseProviderNotFound)
        {

        }

        public DatabaseProviderNotFound(string message)
            : base(message)
        {

        }

        public DatabaseProviderNotFound(string message, Exception innerException)
        {

        }
    }
}
