using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Collections;

namespace RavenClaw.MudEngine.Data
{
    abstract class DbCore
    {
        private DatabaseEngine _dbengine;

        private CollectionBase TableColumns;
        private String TableName;

        public DbCore(ref DatabaseEngine engine)
        {
            DbCommand command;

            if (engine != null)
                _dbengine = engine;
            else
                throw(new ArgumentNullException("passed null engine param"));

            // Create command object and create table
            command = DatabaseEngine.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS " + TableName;
            // TODO: Pull out remaining fields from the collectionbase, and run the SQL
            command.ExecuteNonQuery();
            command = null;

            // do loop
            command.CommandText = "CREATE INDEX idx_" + idx + " ON " + TableName + " (" + idx + ");";
            //

        }

        

    }
}
