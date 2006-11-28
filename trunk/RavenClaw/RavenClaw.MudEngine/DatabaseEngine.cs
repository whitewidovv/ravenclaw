using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Configuration;

namespace RavenClaw.MudEngine
{
    internal class DatabaseEngine
    {
        private DbProviderFactory _factory;
        private DbConnection _connection;

        private static DatabaseEngine _instance = new DatabaseEngine();

        public static DatabaseEngine Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Creates a DatabaseEngine instance
        /// </summary>
        private DatabaseEngine()
        {
            AppSettingsReader settings = new AppSettingsReader();
        
            String provider = Convert.ToString(settings.GetValue("RavenClaw.DatabaseEngine.Provider", typeof(string)));

            if (String.IsNullOrEmpty(provider))
            {
                _factory = DbProviderFactories.GetFactory(provider);
                _connection = _factory.CreateConnection();
            } else {
                throw (new Exception("Database Provider not set"));
            }
        }

         ~DatabaseEngine()
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
                _connection.Close();

            _connection.Dispose();

        }

        /// <summary>
        /// Creates a DbCommand based object from the DB Provider
        /// </summary>
        public DbCommand CreateCommand()
        {
            return _factory.CreateCommand();
        }

        /// <summary>
        /// Creates a DbCommandBuilder based object from the DB Provider
        /// </summary>
        public DbCommandBuilder CreateCommandBuilder()
        {
            return _factory.CreateCommandBuilder();
        }

    }
}
