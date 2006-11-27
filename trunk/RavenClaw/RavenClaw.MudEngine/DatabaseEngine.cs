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

        /// <summary>
        /// Creates a DatabaseEngine instance
        /// </summary>
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

        /// <summary>
        /// Creates a DatabaseEngine instance
        /// </summary>
        public DatabaseEngine(String Provider)
        {
            if (Provider != null)
            {
                _Factory = DbProviderFactories.GetFactory(Provider);
                _Connection = _Factory.CreateConnection();
            }
            else
            {
                throw (new ArgumentNullException("Provider not supplied"));
            }
        }

         ~DatabaseEngine()
        {
            if (_Connection.State != System.Data.ConnectionState.Closed)
                _Connection.Close();

            _Connection.Dispose();

        }

        /// <summary>
        /// Creates a DbCommand based object from the DB Provider
        /// </summary>
        public DbCommand CreateCommand()
        {
            return _Factory.CreateCommand();
        }

        /// <summary>
        /// Creates a DbCommandBuilder based object from the DB Provider
        /// </summary>
        public DbCommandBuilder CreateCommandBuilder()
        {
            return _Factory.CreateCommandBuilder();
        }

    }
}
