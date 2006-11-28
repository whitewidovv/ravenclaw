using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
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
                throw (new Exceptions.DatabaseProviderNotFound());
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
        internal DbCommand CreateCommand()
        {
            return _factory.CreateCommand();
        }

        /// <summary>
        /// Creates a DbCommandBuilder based object from the DB Provider
        /// </summary>
        internal DbCommandBuilder CreateCommandBuilder()
        {
            return _factory.CreateCommandBuilder();
        }

        /// <summary>
        /// Creates a transaction for the the connection based on the DB Provider.
        /// </summary>
        /// <returns>An instance of DBTransaction</returns>
        internal DbTransaction CreateTransaction()
        {
            return _connection.BeginTransaction();
        }

        static internal void AddLongParameter(DbCommand command, string parameter, long parameterValue)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException("parameter");

            DbParameter intParameter = command.CreateParameter();
            intParameter.Direction = ParameterDirection.Input;
            intParameter.Value = parameterValue;
            intParameter.ParameterName = parameter;
            intParameter.DbType = DbType.Int64;
            
            command.Parameters.Add(intParameter);

        }

        static internal void AddStringParameter(DbCommand command, string parameter, string parameterValue)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentNullException("parameter");

            if (string.IsNullOrEmpty(parameterValue))
                throw new ArgumentNullException("parameterValue");

            DbParameter intParameter = command.CreateParameter();
            intParameter.Direction = ParameterDirection.Input;
            intParameter.Value = parameterValue;
            intParameter.ParameterName = parameter;
            intParameter.DbType = DbType.String;

            command.Parameters.Add(intParameter);
        }

    }
}
