// <copyright file="SqlServerAccess.cs" company="Etersoul">
// This code is part of Trilogic Data Project.
// </copyright>
// <author>William</author>
namespace Trilogic.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    using Trilogic.Utility;

    /// <summary>
    /// SQL Server access.
    /// </summary>
    public class SqlServerAccess : DataAccessBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.Data.SqlServerAccess"/> class.
        /// </summary>
        public SqlServerAccess() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trilogic.Data.SqlServerAccess"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="database">The database.</param>
        public SqlServerAccess(string host, string username, string password, string database) : base()
        {
            this.Host = host;
            this.Username = username;
            this.Password = password;
            this.Database = database;
        }
        
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        public string Database { get; set; }

        /// <summary>
        /// Gets the tables.
        /// </summary>
        /// <returns>The tables.</returns>
        public List<string> GetTables()
        {
            List<string> list = new List<string>();

            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for table list");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT h.name + '.' + s.name AS name FROM sys.tables s INNER JOIN sys.schemas h ON h.schema_id = s.schema_id WHERE s.type = 'U' ORDER BY s.name";
                command.CommandType = CommandType.Text;

                this.Logger.Write("Sending command");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["name"].ToString());
                    }
                }

                this.Logger.Write("Process finished");
            }

            return list;
        }

        /// <summary>
        /// Gets the stored procedure.
        /// </summary>
        /// <returns>The stored procedure.</returns>
        public List<string> GetStoredProcedure()
        {
            List<string> list = new List<string>();

            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for stored procedure list");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT h.name + '.' + s.name AS name FROM sys.procedures s INNER JOIN sys.schemas h ON h.schema_id = s.schema_id WHERE s.type = 'P' ORDER BY s.name";
                command.CommandType = CommandType.Text;

                this.Logger.Write("Sending command");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["name"].ToString());
                    }
                }

                this.Logger.Write("Process finished");
            }

            return list;
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        protected SqlConnection CreateConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = this.Host;
            builder.UserID = this.Username;
            builder.Password = this.Password;
            builder.ConnectTimeout = 5;

            SqlConnection connection = new SqlConnection(builder.ToString());
            connection.Open();

            return connection;
        }
    }
}