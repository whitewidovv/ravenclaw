using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Configuration;

namespace RavenClaw.MudEngine
{
    class DatabaseEngine
    {
        private DbProviderFactory _Factory;
        private DbConnection _Connection;

        public DatabaseEngine()
        {
            AppSettingsReader Settings = new AppSettingsReader();
            String Provider;

            Provider = Settings.GetValue("RavenClaw.DatabaseEngine.Provider",typeof(string)).ToString();
            if (Provider != null)
            {
                _Factory = DbProviderFactories.GetFactory(Provider);
                _Connection = _Factory.CreateConnection();
            } else {
                throw (new Exception("Database Provider not set"));
            }
        }

         ~DatabaseEngine()
        {
            if (_Connection.State != System.Data.ConnectionState.Closed)
                _Connection.Close();

            _Connection.Dispose();

        }

    }
}
