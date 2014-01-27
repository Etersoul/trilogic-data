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
        public SchemaCollection GetTables()
        {
            SchemaCollection list = new SchemaCollection();

            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for table list");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.name + '.' + s.name AS name, s.object_id
                FROM sys.tables s
                INNER JOIN sys.schemas h
                    ON h.schema_id = s.schema_id
                WHERE s.type = 'U'
                ORDER BY s.name";

                command.CommandType = CommandType.Text;

                this.Logger.Write("Sending command");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SchemaData { Name = (string)reader["name"], ObjectID = (int)reader["object_id"], Type = SchemaDataType.Table });
                    }
                }

                this.Logger.Write("Process finished");
            }

            return list;
        }

        /// <summary>
        /// Gets the stored procedure.
        /// </summary>
        /// <returns>The stored procedure collection.</returns>
        public SchemaCollection GetStoredProcedure()
        {
            SchemaCollection list = new SchemaCollection();

            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for stored procedure list");

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.name + '.' + s.name AS name, s.object_id
                FROM sys.procedures s
                INNER JOIN sys.schemas h
                ON h.schema_id = s.schema_id
                WHERE s.type = 'P'
                ORDER BY s.name";

                command.CommandType = CommandType.Text;

                this.Logger.Write("Sending command");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SchemaData { Name = (string)reader["name"], ObjectID = (int)reader["object_id"], Type = SchemaDataType.StoredProcedure });
                    }
                }

                this.Logger.Write("Process finished");
            }

            return list;
        }

        /// <summary>
        /// Gets the table schema.
        /// </summary>
        /// <returns>The table schema definition.</returns>
        /// <param name="objectID">Table I.</param>
        public string GetTableSchema(int objectID)
        {
            List<string> list = new List<string>();
            string tableName = string.Empty;
            string schemaName = string.Empty;
            string definition = "CREATE TABLE [{0}].[{1}] (\n\t{2}\n)\nGO\n";
            string columnDef = "[{0}] [{1}]{2} {3}";

            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for table: " + objectID);

                // Table
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT s.name AS table_name, h.name AS schema_name, s.object_id
                FROM sys.tables s
                INNER JOIN sys.schemas h
                    ON h.schema_id = s.schema_id
                WHERE s.type = 'U'
                    AND s.object_id = @objectid
                ORDER BY s.name";
                command.Parameters.Add(new SqlParameter("objectid", objectID));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tableName = reader["table_name"].ToString();
                        schemaName = reader["schema_name"].ToString();
                    }
                }

                // Columns
                command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.name AS column_name, t.name AS type_name, h.is_nullable, h.max_length, t.max_length AS max_length_default,
                    h.is_identity
                FROM sys.columns h
                INNER JOIN sys.types t
                ON t.system_type_id = h.system_type_id
                WHERE h.object_id = @objectid
                ORDER BY h.column_id";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("objectid", objectID));

                this.Logger.Write("Sending command");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(string.Format(
                            columnDef,
                            reader["column_name"].ToString(),
                            reader["type_name"].ToString(),
                            reader["max_length"].ToString() != reader["max_length_default"].ToString() ? "(" + reader["max_length"] + ")" : string.Empty,
                            reader["is_nullable"].ToString() == "False" ? "NOT NULL" : "NULL"));
                    }
                }

                // Constraint & unique key index part
                command = connection.CreateCommand();
                command.CommandText = @"
                SELECT c.name AS column_name, h.is_primary_key, h.name, h.index_id, t.is_descending_key, h.type_desc, h.is_unique
                FROM sys.indexes h
                INNER JOIN sys.index_columns t
                    ON t.object_id = h.object_id
                    AND t.index_id = h.index_id
                INNER JOIN sys.columns c
                    ON c.object_id = h.object_id
                    AND c.column_id = t.column_id
                WHERE h.object_id = @objectid
                    AND h.is_unique = 1
                ORDER BY h.index_id, t.column_id";
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("objectid", objectID));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    string lastName = string.Empty;
                    string lastIsPrimary = string.Empty;
                    string lastIsUnique = string.Empty;
                    string lastTypeDesc = string.Empty;
                    List<string> memberList = new List<string>();
                    while (reader.Read())
                    {
                        if (lastName != reader["name"].ToString())
                        {
                            if (lastName != string.Empty)
                            {
                                list.Add(string.Format(
                                    "CONSTRAINT [{0}] {1}{2}\n\t(\n\t\t{3}\n\t)",
                                    lastName,
                                    lastIsPrimary == "True" ? "PRIMARY KEY " : (lastIsUnique == "True" ? "UNIQUE " : string.Empty),
                                    lastTypeDesc.ToString(),
                                    string.Join(",\n\t\t", memberList.ToArray())));
                            }

                            lastName = reader["name"].ToString();
                            lastIsPrimary = reader["is_primary_key"].ToString();
                            lastIsUnique = reader["is_unique"].ToString();
                            lastTypeDesc = reader["type_desc"].ToString();

                            memberList = new List<string>();
                        }

                        memberList.Add(string.Format(
                            "[{0}] {1}",
                            reader["column_name"].ToString(),
                            reader["is_descending_key"].ToString() == "False" ? "ASC" : "DESC"));

                    }

                    if (lastName != string.Empty)
                    {
                        list.Add(string.Format(
                            "CONSTRAINT [{0}] {1}{2}\n\t(\n\t\t{3}\n\t)",
                            lastName,
                            lastIsPrimary == "True" ? "PRIMARY KEY " : (lastIsUnique == "True" ? "UNIQUE " : string.Empty),
                            lastTypeDesc.ToString(),
                            string.Join(",\n\t\t", memberList.ToArray())));
                    }
                }
            }

            return string.Format(definition, schemaName, tableName, string.Join(",\n\t", list));
        }

        /// <summary>
        /// Gets the stored procedure definition.
        /// </summary>
        /// <returns>The stored procedure definition.</returns>
        /// <param name="objectID">Object ID.</param>
        public string GetStoredProcedureDefinition(int objectID)
        {
            using (SqlConnection connection = this.CreateConnection())
            {
                this.Logger.Write("Opening connection for procedure: " + objectID);

                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT h.object_id, h.definition
                FROM sys.sql_modules h
                WHERE h.object_id = @objectid";

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("objectid", objectID));

                this.Logger.Write("Sending command");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["definition"].ToString() + "GO\n";
                    }
                }

                return null;
            }
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